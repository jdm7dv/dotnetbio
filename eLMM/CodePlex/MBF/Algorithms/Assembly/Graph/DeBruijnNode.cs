//*********************************************************
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//
//
//
//
//*********************************************************



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bio.Algorithms.Kmer;

namespace Bio.Algorithms.Assembly.Graph
{
    /// <summary>
    /// Represents a node in the De Bruijn graph
    /// A node is associated with a k-mer. 
    /// Also holds adjacency information with other nodes.
    /// </summary>
    public class DeBruijnNode
    {
        #region Fields
        /// <summary>
        /// Holds sequence index and starting position within sequence
        /// for kmer represented by this node.
        /// </summary>
        private int _sequenceIndex, _kmerPosition;

        /// <summary>
        /// Length of k-mer associated with the node
        /// </summary>
        private int _kmerLength;

        /// <summary>
        /// Number of times this k-mer occurs in input sequences
        /// </summary>
        private int _countNormalOrientation;

        /// <summary>
        /// Number of times reverse-complement of k-mer occurs in input sequences.
        /// Default value for reverseComplementCount is -1.
        /// If RC is stored seperately (as in Velvet algorithm), this field should remain -1.
        /// Used only when kmer, RC(kmer) are stored in the same node (for eg. in algorithms like ABySS).
        /// </summary>
        private int _countReverseComplement = -1;

        /// <summary>
        /// Right Extension edges. Edge contains connecting node, and orientation of edge. 
        /// A right-end extension edge will be added from node A to node B, if there is an 
        /// overlap of length (k-1) between right end of sequence A and left end of sequences B. 
        /// Orientation is same, if overlapping sequences in adjacent nodes 
        /// are normal orientation. Orientation is opposite, if one of the 
        /// sequences is reverse complement.
        /// </summary>
        private Dictionary<DeBruijnNode, DeBruijnEdge> _rightEndExtensionNodes;

        /// <summary>
        /// Left Extension edges. Edge contains connecting node, and orientation of edge. 
        /// A left-end extension edge will be added from node A to node B, if there is an 
        /// overlap of length (k-1) between left end of sequence A and right end of sequences B. 
        /// Orientation is same, if overlapping sequences in adjacent nodes 
        /// are normal orientation. Orientation is opposite, if one of the 
        /// sequences is reverse complement.
        /// </summary>
        private Dictionary<DeBruijnNode, DeBruijnEdge> _leftEndExtensionNodes;

        /// <summary>
        /// Value to be assigned yto field kmer count to indicate
        /// that node has been visited. This value is chosen such 
        /// that no legitimate node will have this value for kmer count.
        /// </summary>
        private const int VISITED_NODE_KMERCOUNT_VALUE = -100;
        #endregion

        #region Constructors and Properties
        /// <summary>
        /// Initializes a new instance of the DeBruijnNode class.
        /// Creates graph node with sequence index.
        /// </summary>
        /// <param name="length">Length of k-mer</param>
        /// <param name="sequenceIndex">Sequence Index for k-mer</param>
        public DeBruijnNode(int length, int sequenceIndex)
            : this(length, sequenceIndex, 0) { }

        /// <summary>
        /// Initializes a new instance of the DeBruijnNode class.
        /// Allocates left and right extension data structure.
        /// </summary>
        /// <param name="length">Length of k-mer</param>
        /// <param name="sequenceIndex">Index of parent sequence</param>
        /// <param name="kmerPosition">Start position of kmer within parent sequence</param>
        public DeBruijnNode(int length, int sequenceIndex, int kmerPosition)
        {
            if (length <= 0)
            {
                throw new ArgumentException(Properties.Resource.KmerLengthShouldBePositive);
            }

            _sequenceIndex = sequenceIndex;
            _kmerPosition = kmerPosition;
            _kmerLength = length;
            _rightEndExtensionNodes = new Dictionary<DeBruijnNode, DeBruijnEdge>();
            _leftEndExtensionNodes = new Dictionary<DeBruijnNode, DeBruijnEdge>();
            _countNormalOrientation = 1;
            _countReverseComplement = 0;
        }

        /// <summary>
        /// Gets the right extension edges
        /// </summary>
        public Dictionary<DeBruijnNode, DeBruijnEdge> RightExtensionNodes
        {
            get { return _rightEndExtensionNodes; }
        }

        /// <summary>
        /// Gets the left extension edges
        /// </summary>
        public Dictionary<DeBruijnNode, DeBruijnEdge> LeftExtensionNodes
        {
            get { return _leftEndExtensionNodes; }
        }

        /// <summary>
        /// Gets the length of associated k-mer
        /// </summary>
        public int KmerLength
        {
            get { return _kmerLength; }
        }

        /// <summary>
        /// Gets index of source sequence for kmer
        /// </summary>
        public int SequenceIndex
        {
            get { return _sequenceIndex; }
        }

        /// <summary>
        /// Gets start position of kmer in source sequence
        /// </summary>
        public int KmerPosition
        {
            get { return _kmerPosition; }
        }

        /// <summary>
        /// Gets the total count of number of occurrances for the k-mer.
        /// If _countReverseComplement is -1, it indicates it is an unused field.
        /// </summary>
        public int KmerCount
        {
            get
            {
                return (_countReverseComplement == -1) ?
                    _countNormalOrientation :
                    _countNormalOrientation + _countReverseComplement;
            }
        }

        /// <summary>
        /// Gets the total number of extension edges for the node.
        /// </summary>
        public int ExtensionsCount
        {
            get { return RightExtensionNodes.Count + LeftExtensionNodes.Count; }
        }
        #endregion

        /// <summary>
        /// Update count and location information for k-mer based on values in input Kmer.
        /// Thread-safe method
        /// </summary>
        /// <param name="isReverseComplement">Boolean indicating if kmer is reverse complement</param>
        public void AddKmerDataThreadSafe(bool isReverseComplement)
        {
            if (isReverseComplement)
            {
                IncrementReverseComplementCount();
            }
            else
            {
                IncrementCount();
            }
        }

        /// <summary>
        /// Add node with given orientation to left extension edges.
        /// Not thread-safe. Use lock at caller if required.
        /// </summary>
        /// <param name="node">Node to add left-extension to</param>
        /// <param name="isSameOrientation">Orientation of connecting edge</param>
        public void AddLeftEndExtension(DeBruijnNode node, bool isSameOrientation)
        {
            ValidateNode(node);
            DeBruijnEdge edge;
            if (_leftEndExtensionNodes.TryGetValue(node, out edge))
            {
                _leftEndExtensionNodes[node].IsSameOrientation ^= isSameOrientation;
            }
            else
            {
                _leftEndExtensionNodes[node] = new DeBruijnEdge(isSameOrientation);
            }
        }

        /// <summary>
        /// Add node with given orientation to right extension edges.
        /// Not thread-safe. Use lock at caller if required.
        /// </summary>
        /// <param name="node">Node to add right-extension to</param>
        /// <param name="isSameOrientation">Orientation of connecting edge</param>
        public void AddRightEndExtension(DeBruijnNode node, bool isSameOrientation)
        {
            ValidateNode(node);
            DeBruijnEdge edge;
            if (_rightEndExtensionNodes.TryGetValue(node, out edge))
            {
                _rightEndExtensionNodes[node].IsSameOrientation ^= isSameOrientation;
            }
            else
            {
                _rightEndExtensionNodes[node] = new DeBruijnEdge(isSameOrientation);
            }
        }

        /// <summary>
        /// Removes edge corresponding to the node from appropriate data structure,
        /// after checking whether given node is part of left or right extensions.
        /// Thread-safe method
        /// </summary>
        /// <param name="node">Node for which extension is to be removed</param>
        public void RemoveExtensionThreadSafe(DeBruijnNode node)
        {
            ValidateNode(node);
            bool removed;
            lock (_rightEndExtensionNodes)
            {
                removed = _rightEndExtensionNodes.Remove(node);
            }

            if (!removed)
            {
                lock (_leftEndExtensionNodes)
                {
                    _leftEndExtensionNodes.Remove(node);
                }
            }
        }

        /// <summary>
        /// Makes extension edge corresponding to the node invalid,
        /// after checking whether given node is part of left or right extensions.
        /// Not Thread-safe. Use lock at caller if required.
        /// </summary>
        /// <param name="node">Node for which extension is to be made invalid</param>
        public void MarkExtensionInvalid(DeBruijnNode node)
        {
            ValidateNode(node);
            if (_rightEndExtensionNodes.ContainsKey(node))
            {
                _rightEndExtensionNodes[node].IsValid = false;
            }
            else if (_leftEndExtensionNodes.ContainsKey(node))
            {
                _leftEndExtensionNodes[node].IsValid = false;
            }
        }

        /// <summary>
        /// Computes the valid extensions for the node
        /// and stores them in appropriate fields.
        /// </summary>
        public void PurgeInvalidExtensions()
        {
            List<DeBruijnNode> extensionNodes = _leftEndExtensionNodes.Keys.ToList();
            foreach (DeBruijnNode node in extensionNodes)
            {
                if (!_leftEndExtensionNodes[node].IsValid)
                    _leftEndExtensionNodes.Remove(node);
            }

            extensionNodes = _rightEndExtensionNodes.Keys.ToList();
            foreach (DeBruijnNode node in extensionNodes)
            {
                if (!_rightEndExtensionNodes[node].IsValid)
                    _rightEndExtensionNodes.Remove(node);
            }
        }

        /// <summary>
        /// Mark nodes as visited.
        /// WARNING: DO NOT USE this if you need kmer count information.
        /// kmer count field is being re-used for this purpose.
        /// Old value of kmer count will be over-written.
        /// </summary>
        public void MarkNodeAsVisited()
        {
            _countNormalOrientation = VISITED_NODE_KMERCOUNT_VALUE;
        }

        /// <summary>
        /// Check if node is marked as visited
        /// Checks if the kmer count field is set to a specific value
        /// </summary>
        /// <returns>True if marked; otherwise false</returns>
        public bool IsNodeVisited()
        {
            return _countNormalOrientation == VISITED_NODE_KMERCOUNT_VALUE;
        }

        /// <summary>
        /// Check if input node is null
        /// </summary>
        /// <param name="node">Input node</param>
        private static void ValidateNode(DeBruijnNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
        }

        /// <summary>
        /// Update normal orientation count and location 
        /// information for k-mer based on values in input Kmer.
        /// </summary>
        private void IncrementCount()
        {
            // Update for kmer
            Interlocked.Increment(ref _countNormalOrientation);
        }

        /// <summary>
        /// Update reverse-complement orientation count and location 
        /// information for k-mer based on values in input Kmer.
        /// </summary>
        private void IncrementReverseComplementCount()
        {
            Interlocked.Increment(ref _countReverseComplement);
        }
    }
}
