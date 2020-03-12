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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bio.Tests
{
    /// <summary>
    /// Tests for derived sequences.
    /// </summary>
    [TestClass]
    public class DeriveSequenceTests
    {
        /// <summary>
        /// Get Reversed Sequence.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestGetReversedSequence()
        {
            const string sequence = "ATGCC";
            const string expectedSequence = "CCGTA";
            ISequence orignalSequence = new Sequence(Alphabets.DNA, sequence);
            DerivedSequence deriveSequence = new DerivedSequence(orignalSequence, false, false);
            string actualSequence = new string(deriveSequence.GetReversedSequence().Select(a => (char)a).ToArray());
            Assert.AreEqual(expectedSequence, actualSequence);
        }

        /// <summary>
        /// Get Complemented Sequence.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestGetComplementedSequence()
        {
            const string sequence = "ATGCC";
            const string expectedSequence = "TACGG";
            ISequence orignalSequence = new Sequence(Alphabets.DNA, sequence);
            DerivedSequence deriveSequence = new DerivedSequence(orignalSequence, false, false);
            string actualSequence = new string(deriveSequence.GetComplementedSequence().Select(a => (char)a).ToArray());
            Assert.AreEqual(expectedSequence, actualSequence);
        }

        /// <summary>
        /// Get Reverse Complemented Sequence.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestGetReverseComplementedSequence()
        {
            const string sequence = "ATGCC";
            const string expectedSequence = "GGCAT";
            ISequence orignalSequence = new Sequence(Alphabets.DNA, sequence);
            DerivedSequence deriveSequence = new DerivedSequence(orignalSequence, false, false);
            string actualSequence = new string(deriveSequence.GetReverseComplementedSequence().Select(a => (char)a).ToArray());
            Assert.AreEqual(expectedSequence, actualSequence);
        }
    }
}
