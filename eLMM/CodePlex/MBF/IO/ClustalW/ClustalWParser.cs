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
using System.Globalization;
using System.IO;
using System.Text;
using Bio.Algorithms.Alignment;
using Bio.Encoding;
using Bio.Properties;
using Bio.Util;
using Bio.Util.Logging;

namespace Bio.IO.ClustalW
{
    /// <summary>
    /// A ClustalWParser reads from a source of text that is formatted according to the ClustalW flat
    /// file specification, and converts the data to in-memory ISequenceAlignment objects.  For advanced
    /// users, the ability to select an encoding for the internal memory representation is
    /// provided. There is also a default encoding for each alphabet that may be encountered.
    /// </summary>
    public class ClustalWParser : ISequenceAlignmentParser
    {
        #region "Private Field(s)"
        /// <summary>
        /// Basic Sequence Alignment Parser that contains all the common methods required.
        /// </summary>
        private CommonSequenceParser _basicParser;
        #endregion

        #region "Constructor(s)"
        /// <summary>
        /// Initializes a new instance of the ClustalWParser class.
        /// Default constructor chooses default encoding based on alphabet.
        /// </summary>
        public ClustalWParser()
        {
            _basicParser = new CommonSequenceParser();
        }

        /// <summary>
        /// Initializes a new instance of the ClustalWParser class.
        /// Constructor for setting the encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use for parsed ISequence objects.</param>
        public ClustalWParser(IEncoding encoding)
            : this()
        {
            Encoding = encoding;
        }
        #endregion

        #region "Public Property(ies)"
        /// <summary>
        /// Gets the name of the sequence alignment parser being
        /// implemented. This is intended to give the
        /// developer some information of the parser type.
        /// </summary>
        public string Name
        {
            get { return Resource.CLUSTALW_NAME; }
        }

        /// <summary>
        /// Gets the description of the sequence alignment parser being
        /// implemented. This is intended to give the
        /// developer some information of the parser.
        /// </summary>
        public string Description
        {
            get { return Resource.CLUSTALWPARSER_DESCRIPTION; }
        }

        /// <summary>
        /// Gets or sets alphabet to use for sequences in parsed ISequenceAlignment objects.
        /// </summary>
        public IAlphabet Alphabet { get; set; }

        /// <summary>
        /// Gets or sets encoding to use for sequences in parsed ISequenceAlignment objects.
        /// </summary>
        public IEncoding Encoding { get; set; }

        /// <summary>
        /// Gets the file extensions that the parser implementation
        /// will support.
        /// </summary>
        public string FileTypes
        {
            get { return Resource.CLUSTALW_FILEEXTENSION; }
        }
        #endregion

        #region "Public Method(s)"
        /// <summary>
        /// Parses a list of biological sequence alignment texts from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence alignment text.</param>
        /// <returns>The list of parsed ISequenceAlignment objects.</returns>
        public IList<ISequenceAlignment> Parse(TextReader reader)
        {
            return Parse(reader, true);
        }

        /// <summary>
        /// Parses a list of biological sequence alignment texts from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence alignment text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequenceAlignment objects.</returns>
        public IList<ISequenceAlignment> Parse(TextReader reader, bool isReadOnly)
        {
            using (BioTextReader bioReader = new BioTextReader(reader))
            {
                return Parse(bioReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a list of biological sequence alignment texts from a file.
        /// </summary>
        /// <param name="fileName">The name of a biological sequence alignment file.</param>
        /// <returns>The list of parsed ISequenceAlignment objects.</returns>
        public IList<ISequenceAlignment> Parse(string fileName)
        {
            return Parse(fileName, true);
        }

        /// <summary>
        /// Parses a list of biological sequence alignment texts from a file.
        /// </summary>
        /// <param name="fileName">The name of a biological sequence alignment file.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequenceAlignment objects.</returns>
        public IList<ISequenceAlignment> Parse(string fileName, bool isReadOnly)
        {
            using (BioTextReader bioReader = new BioTextReader(fileName))
            {
                return Parse(bioReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a single biological sequence alignment text from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence alignment text.</param>
        /// <returns>The parsed ISequenceAlignment object.</returns>
        public ISequenceAlignment ParseOne(TextReader reader)
        {
            return ParseOne(reader, true);
        }

        /// <summary>
        /// Parses a single biological sequence alignment text from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence alignment text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence alignment should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The parsed ISequenceAlignment object.</returns>
        public ISequenceAlignment ParseOne(TextReader reader, bool isReadOnly)
        {
            using (BioTextReader bioReader = new BioTextReader(reader))
            {
                return ParseOne(bioReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a single biological sequence alignment text from a file.
        /// </summary>
        /// <param name="fileName">The name of a biological sequence alignment file.</param>
        /// <returns>The parsed ISequenceAlignment object.</returns>
        public ISequenceAlignment ParseOne(string fileName)
        {
            return ParseOne(fileName, true);
        }

        /// <summary>
        /// Parses a single biological sequence alignment text from a file.
        /// </summary>
        /// <param name="fileName">The name of a biological sequence alignment file.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence alignment should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The parsed ISequenceAlignment object.</returns>
        public ISequenceAlignment ParseOne(string fileName, bool isReadOnly)
        {
            using (BioTextReader bioReader = new BioTextReader(fileName))
            {
                return ParseOne(bioReader, isReadOnly);
            }
        }
        #endregion

        #region "Protected Method(s)"
        /// <summary>
        /// Parses a list of sequences using a BioTextReader.
        /// </summary>
        /// <remarks>
        /// This method should be overridden by any parsers that need to process file-scope
        /// metadata that applies to all of the sequences in the file.
        /// </remarks>
        /// <param name="bioReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequence objects.</returns>
        protected virtual IList<ISequenceAlignment> Parse(BioTextReader bioReader, bool isReadOnly)
        {
            if (bioReader == null)
                throw new ArgumentNullException("bioReader");

            // no empty files allowed
            if (!bioReader.HasLines)
            {
                string message = Properties.Resource.IONoTextToParse;
                throw new InvalidDataException(message);
            }

            List<ISequenceAlignment> alignments = new List<ISequenceAlignment>();

            while (bioReader.HasLines)
            {
                if (string.IsNullOrEmpty(bioReader.Line.Trim()))
                {
                    bioReader.GoToNextLine();
                    continue;
                }

                alignments.Add(ParseOneWithSpecificFormat(bioReader, isReadOnly));
            }

            return alignments;
        }

        /// <summary>
        /// Parses a single ClustalW text from a reader into a sequence.
        /// </summary>
        /// <param name="bioReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence alignment should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.</param>
        /// <returns>A new Sequence Alignment instance containing parsed data.</returns>
        protected ISequenceAlignment ParseOneWithSpecificFormat(BioTextReader bioReader, bool isReadOnly)
        {
            if (bioReader == null)
                throw new ArgumentNullException("bioReader");

            string message = string.Empty;

            if (!bioReader.Line.StartsWith("CLUSTAL", StringComparison.OrdinalIgnoreCase))
            {
                message = string.Format(CultureInfo.CurrentCulture, Resource.INVAILD_INPUT_FILE, this.Name);
                throw new InvalidDataException(message);
            }

            bioReader.GoToNextLine();  // Skip blank lines until we get to the first block.

            // Now that we're at the first block, one or more blank lines are the block separators, which we'll need.
            bioReader.SkipBlankLines = false;

            Dictionary<string, ISequence> mapIdToSequence = new Dictionary<string, ISequence>();
            IAlphabet alignmentAlphabet = null; 
            bool isFirstBlock = true;
            bool inBlock = false;

            while (bioReader.HasLines)
            {
                // Blank line or consensus line signals end of block.
                if (String.IsNullOrEmpty(bioReader.Line) || 
                    Helper.ContainsOnly(bioReader.Line, '*', ' ', '.', '+', ':'))
                {
                    if (inBlock)
                    {
                        // Blank line signifies end of block
                        inBlock = false;
                        isFirstBlock = false;
                    }
                }
                else // It's not a blank or consensus line.
                {
                    // It's a data line in a block.
                    // Lines begin with sequence id, then the sequence segment, and optionally a number, which we will ignore
                    string[] tokens = bioReader.Line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries); // (char[])null uses whitespace delimiters
                    string id = tokens[0];
                    string data = tokens[1].ToUpper(CultureInfo.InvariantCulture);
                    Sequence sequence = null;
                    IAlphabet alphabet = Alphabet;

                    inBlock = true;
                    if (isFirstBlock)
                    {
                        if (null == alphabet)
                        {
                            alphabet = _basicParser.IdentifyAlphabet(alphabet, data);

                            if (null == alphabet)
                            {
                                message = string.Format(
                                        CultureInfo.InvariantCulture,
                                        Resource.InvalidSymbolInString,
                                        data);
                                throw new InvalidDataException(message);
                            }
                            else
                            {
                                if (null == alignmentAlphabet)
                                {
                                    alignmentAlphabet = alphabet;
                                }
                                else
                                {
                                    if (alignmentAlphabet != alphabet)
                                    {
                                        message = string.Format(
                                                CultureInfo.CurrentCulture,
                                                Properties.Resource.SequenceAlphabetMismatch);
                                        throw new InvalidDataException(message);
                                    }
                                }
                            }
                        }

                        if (Encoding == null)
                        {
                            sequence = new Sequence(alphabet, data);
                        }
                        else
                        {
                            sequence = new Sequence(alphabet, Encoding, data);
                        }

                        sequence.ID = id;
                        sequence.IsReadOnly = false;

                        mapIdToSequence.Add(id, sequence);
                    }
                    else
                    {
                        if (!mapIdToSequence.ContainsKey(id))
                        {
                            message = string.Format(CultureInfo.CurrentCulture, Properties.Resource.ClustalUnknownSequence, id);
                            throw new InvalidDataException(message);
                        }

                        sequence = (Sequence)mapIdToSequence[id];
                        sequence.InsertRange(sequence.Count, data);
                    }
                }

                bioReader.GoToNextLine();
            }

            SequenceAlignment sequenceAlignment = new SequenceAlignment();
            sequenceAlignment.AlignedSequences.Add(new AlignedSequence());
            foreach (Sequence alignmentSequence in mapIdToSequence.Values)
            {
                alignmentSequence.IsReadOnly = isReadOnly;
                sequenceAlignment.AlignedSequences[0].Sequences.Add(alignmentSequence);
            }

            return sequenceAlignment;
        }
        #endregion

        #region "Private Method(s)"
        /// <summary>
        /// Parses a single sequences using a BioTextReader.
        /// </summary>
        /// <param name="bioReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence alignment should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.</param>
        /// <returns>A new Sequence Alignment instance containing parsed data.</returns>
        private ISequenceAlignment ParseOne(BioTextReader bioReader, bool isReadOnly)
        {
            // no empty files allowed
            if (!bioReader.HasLines)
            {
                string message = Properties.Resource.IONoTextToParse;
                throw new InvalidDataException(message);
            }

            // do the actual parsing
            return ParseOneWithSpecificFormat(bioReader, isReadOnly);
        }
        #endregion
    }
}
