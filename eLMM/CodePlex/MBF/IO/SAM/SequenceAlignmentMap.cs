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
using System.Runtime.Serialization;
using Bio.Algorithms.Alignment;
using Bio.Util;

namespace Bio.IO.SAM
{
    /// <summary>
    /// Class to hold sequence alignment map (SAM) structure.
    /// </summary>
    [Serializable]
    public class SequenceAlignmentMap : ISequenceAlignment
    {
        /// <summary>
        /// Holds SAM header.
        /// </summary>
       private SAMAlignmentHeader _header;

        /// <summary>
        /// holds the metadta.
        /// </summary>
        private Dictionary<string, object> _metadata;

        /// <summary>
        /// Holds list of query sequences present in this SAM object.
        /// </summary>
        private List<SAMAlignedSequence> _querySequences;

        /// <summary>
        /// Gets the SAM header.
        /// </summary>
        public SAMAlignmentHeader Header
        {
            get
            {
                return _header;
            }
        }

        /// <summary>
        /// Returns list of reference sequences present in this header. 
        /// </summary>
        public IList<string> GetRefSequences()
        {
            return _header.GetReferenceSequences();
        }

        /// <summary>
        /// Returns list of SequenceRanges objects which represents reference sequences present in the header. 
        /// </summary>
        public IList<SequenceRange> GetReferenceSequenceRanges()
        {
            return _header.GetReferenceSequenceRanges();
        }

        /// <summary>
        /// Gets the query sequences present in this alignment.
        /// </summary>
        public IList<SAMAlignedSequence> QuerySequences
        {
            get
            {
                return _querySequences;
            }
        }

        /// <summary>
        /// Default constructor.
        /// Creates SequenceAlignmentMap instance.
        /// </summary>
        public SequenceAlignmentMap() : this(new SAMAlignmentHeader()) { }

        /// <summary>
        /// Creates SequenceAlignmentMap instance.
        /// </summary>
        /// <param name="header">SAM header.</param>
        public SequenceAlignmentMap(SAMAlignmentHeader header)
        {
            if (header == null)
            {
                throw new ArgumentNullException("header");
            }

            _header = header;
            _metadata = new Dictionary<string, object>();
            _metadata.Add(Helper.SAMAlignmentHeaderKey, _header);
            _querySequences = new List<SAMAlignedSequence>();
        }

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming context.</param>
        protected SequenceAlignmentMap(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            _header = (SAMAlignmentHeader)info.GetValue("header", typeof(SAMAlignmentHeader));
            _metadata = new Dictionary<string, object>();
            _metadata.Add(Helper.SAMAlignmentHeaderKey, _header);
            _querySequences = (List<SAMAlignedSequence>)info.GetValue("sequences", typeof(List<SAMAlignedSequence>));

            if (_querySequences == null)
            {
                _querySequences = new List<SAMAlignedSequence>();
            }
        }

        /// <summary>
        /// Gets list of aligned sequences present in this alignment.
        /// </summary>
        public IList<IAlignedSequence> AlignedSequences
        {
            get { return _querySequences.ConvertAll(Q => Q as IAlignedSequence).ToList().AsReadOnly(); }
        }

        /// <summary>
        /// Gets list of source sequences present in this alignment.
        /// Note that this method always returns an empty readonly list.
        /// </summary>
        public IList<ISequence> Sequences
        {
            get { return new List<ISequence>().AsReadOnly(); }
        }

        /// <summary>
        /// Gets the metadata of this alignment.
        /// </summary>
        public Dictionary<string, object> Metadata
        {
            get { return _metadata; }
        }

        /// <summary>
        /// Gets documentation object.
        /// </summary>
        public object Documentation
        {
            get;
            set;
        }

        /// <summary>
        /// Method for serializing the SAM object.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("header", _header);
            info.AddValue("sequences", _querySequences);
        }
    }
}
