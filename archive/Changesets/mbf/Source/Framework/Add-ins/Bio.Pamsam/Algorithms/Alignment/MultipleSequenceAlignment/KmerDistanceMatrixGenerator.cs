﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bio.Algorithms.Alignment.MultipleSequenceAlignment
{
    /// <summary>
    /// Implemetation of Distance Matrix Generator class via Kmer Counting.
    /// 
    /// Generates a distance matrix from a set of *unaligned* sequences by
    /// kmer distance method. Although the method applies to aligned sequences,
    /// KimuraDistanceMatrixGenerator should be the choice for aligned sequences.
    /// 
    /// A kmer is a contiguous subsequence of length k, also known as a word or k-tuple.
    /// Related sequences tend to have more kmers in common than by chance. A distance
    /// score is assigned to every pair of sequences by comparing the commonness of kmers.
    /// The frequency of kmers within one sequence is calculated by kmer counting, and 
    /// the kmers and their frequencies are represented by dictionary due to the sparsity.
    /// Then the kmer distance between two sequences is defined as the distance of two vectors
    /// of kmer frequencies in Euclidean space. A variety of distance functions are avaialble. 
    /// Kmer counting and distance functions are defined in KmerDistanceScoreCalculator class.
    /// 
    /// A kmer distance matrix generator should use one kmer length and one selected distance
    /// function for the whole set of unaligned sequences. This is done by passing kmer length
    /// and distance function name to KmerDistanceScoreCalculator class, where these two are 
    /// read-only variables.
    /// 
    /// The generator is an O(N^2) algorithm. It first generates kmer counting for all the 
    /// sequences in linear time, and calculates pairwise distance score in the double loop.
    /// The distances will be stored in a symmetric square distance matrix, where rows and 
    /// cols are sequences, and elements are distance scores. The distance matrix is then used
    /// to generate binary guide tree by hierarchical clustering method.
    /// </summary>
    public class KmerDistanceMatrixGenerator : IDistanceMatrixGenerator
    {
        #region Fields

        // Distance matrix
        private IDistanceMatrix _distanceMatrix;

        // Stores k-mer counts for all sequence
        private Dictionary<String, float>[] _allCountsDictionary;

        // The k_mer distance score calculator
        private KmerDistanceScoreCalculator _kmerScoreCalculator;

        // The length of kmer in this class
        private int _kmerLength;

        #endregion

        #region Properties
        /// <summary>
        /// The distance matrix generated by this class
        /// </summary>
        public IDistanceMatrix DistanceMatrix 
        { 
            get { return _distanceMatrix; } 
        }

        /// <summary>
        /// The method name of this class
        /// </summary>
        public string Name 
        { 
            get { return Resource.KmerDistanceMatrixGeneratorName; } 
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default distance function is Euclidean Distance
        /// </summary>
        /// <param name="sequences">a list of unaligned sequences</param>
        /// <param name="kmerLength">positive integer length of kmer</param>
        /// <param name="alphabetType">alphabet type: DNA, RNA, or Protein</param>
        public KmerDistanceMatrixGenerator(List<ISequence> sequences, int kmerLength, IAlphabet alphabetType)
            : this(sequences, kmerLength, alphabetType, DistanceFunctionTypes.EuclideanDistance)
        {
        }

        /// <summary>
        /// Construct DistanceMatrix via k-mer counting algorithm
        /// </summary>
        /// <param name="sequences">a list of unaligned sequences</param>
        /// <param name="kmerLength">positive integer length of kmer</param>
        /// <param name="alphabetType">moleculeType: DNA, RNA or Protein</param>
        /// <param name="distanceFunctionName">distance function name</param>
        public KmerDistanceMatrixGenerator(IList<ISequence> sequences, int kmerLength, IAlphabet alphabetType, DistanceFunctionTypes distanceFunctionName)
        {
            if (sequences.Count == 0)
            {
                throw new ArgumentException("Empty input sequence list");
            }

            _kmerLength = kmerLength;

            _kmerScoreCalculator = new KmerDistanceScoreCalculator(kmerLength, alphabetType, distanceFunctionName);

            GenerateDistanceMatrix(sequences);
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Generate a symmetric distance matrix from a set of unaligned sequences.
        /// </summary>
        /// <param name="sequences">a set of unaligned sequences</param>
        public void GenerateDistanceMatrix(IList<ISequence> sequences)
        {
            // Generate k-mer counting dictionary for each sequence
            try
            {
                _allCountsDictionary = new Dictionary<string,float>[sequences.Count];

                Parallel.For(0, sequences.Count, i =>
                {
                    Dictionary<string, float> currentDictionary = KmerDistanceScoreCalculator.CalculateKmerCounting(sequences[i], _kmerLength);
                    MsaUtils.Normalize(currentDictionary);
                    _allCountsDictionary[i] = currentDictionary;
                });
            }
            catch (OutOfMemoryException ex)
            {
                throw new Exception("Out of memory when generating kmer counting", ex.InnerException);
            }

            // Construct a SymmetricDistanceMatrix
            // with dimension equals to the number of sequences
            _distanceMatrix = new SymmetricDistanceMatrix(sequences.Count);

            // Fill in DistanceMatrix
            Parallel.For(1, sequences.Count, PAMSAMMultipleSequenceAligner.parallelOption, row =>
            {
                for (int col = 0; col < row; ++col)
                {
                    float distanceScore = _kmerScoreCalculator.CalculateDistanceScore
                                (_allCountsDictionary[row], _allCountsDictionary[col]);
                    _distanceMatrix[row, col] = distanceScore;
                    _distanceMatrix[col, row] = distanceScore;
                }
            });
        }
        #endregion
    }
}
