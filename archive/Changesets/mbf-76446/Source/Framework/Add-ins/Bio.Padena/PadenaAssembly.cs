﻿// *********************************************************
// 
//     Copyright (c) Microsoft. All rights reserved.
//     This code is licensed under the Apache License, Version 2.0.
//     THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//     ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//     IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//     PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// 
// *********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Bio.Algorithms.Assembly.Padena
{
    /// <summary>
    /// PadenaAssembly is the result of running Padena on a set input sequences. 
    /// As part of assembled output, it gives contig and scaffold sequences.
    /// </summary>
    public class PadenaAssembly : IDeBruijnDeNovoAssembly
    {
        #region Fields
        /// <summary>
        /// Holds list of contigs created after Assembly.
        /// </summary>
        private List<ISequence> contigSequences;

        /// <summary>
        /// Holds list of scaffolds created after Assembly.
        /// </summary>
        private List<ISequence> scaffolds;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the PadenaAssembly class.
        /// Default constructor.
        /// </summary>
        public PadenaAssembly()
        {
            this.contigSequences = new List<ISequence>();
            this.scaffolds = new List<ISequence>();
        }

        #endregion Constructors

        #region IDeNovoAssembly Members
        /// <summary>
        /// Gets the list of assembled sequences.
        /// </summary>
        public IList<ISequence> AssembledSequences
        {
            get
            {
                if (this.scaffolds != null && this.scaffolds.Count() > 0)
                {
                    return this.scaffolds;
                }
                else
                {
                    return this.contigSequences;
                }
            }
        }

        /// <summary>
        /// Gets or sets the associated documentation.
        /// The Documentation object is intended for tracking the history, provenance,
        /// and experimental context of a PadenaAssembly. The user can adopt any desired
        /// convention for use of this object.
        /// </summary>
        public object Documentation { get; set; }
        #endregion

        /// <summary>
        /// Gets list of contig sequences created by assembler.
        /// </summary>
        public IList<ISequence> ContigSequences
        {
            get
            {
                return this.contigSequences;
            }
        }

        /// <summary>
        /// Gets the list of assembler scaffolds.
        /// </summary>
        public IList<ISequence> Scaffolds
        {
            get { return this.scaffolds; }
        }

        /// <summary>
        /// Add list of contigs.
        /// </summary>
        /// <param name="contigs">List of contig sequences.</param>
        public void AddContigs(IEnumerable<ISequence> contigs)
        {
            if (contigs != null)
            {
                this.contigSequences.AddRange(contigs);
            }
        }

        /// <summary>
        /// Add list of scaffolds.
        /// </summary>
        /// <param name="scaffoldsLists">List of scaffold sequences.</param>
        public void AddScaffolds(IEnumerable<ISequence> scaffoldsLists)
        {
            if (scaffoldsLists != null)
            {
                this.scaffolds.AddRange(scaffoldsLists);
            }
        }

        #region ISerializable Members
        /// <summary>
        /// Method for serializing the PadenaAssembly.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("PadenaAssembly:ContigSequences", this.contigSequences);
            info.AddValue("PadenaAssembly:Scaffolds", this.scaffolds);
            if (this.Documentation != null && ((this.Documentation.GetType().Attributes &
               System.Reflection.TypeAttributes.Serializable) == System.Reflection.TypeAttributes.Serializable))
            {
                info.AddValue("PadenaAssembly:Documentation", this.Documentation);
            }
            else
            {
                info.AddValue("PadenaAssembly:Documentation", null);
            }
        }
        #endregion
    }
}
