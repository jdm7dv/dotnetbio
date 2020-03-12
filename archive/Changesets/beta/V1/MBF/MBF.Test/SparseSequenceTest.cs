﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

namespace MBF.Test
{
    using System;
    using NUnit.Framework;
    using MBF.Util;
    using System.Collections.Generic;

    /// <summary>
    /// Class to test SparseSequence.
    /// </summary>
    [TestFixture]
    public class SparseSequenceTest
    {
        /// <summary>
        /// Test sparse sequence class.
        /// </summary>
        [Test]
        public void TestSparseSequence()
        {
            SparseSequence tempSeq = null;

            #region Test constructors by passing invalid parameters.
            try
            {
                tempSeq = new SparseSequence(Alphabets.DNA, 0, Alphabets.RNA.U);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(Alphabets.RNA, 0, Alphabets.DNA.T);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                ISequence seq = new Sequence(Alphabets.RNA, "AUGC");
                tempSeq = new SparseSequence(Alphabets.DNA, 0, seq);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(Alphabets.DNA, -1, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(Alphabets.DNA, int.MaxValue, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(Alphabets.DNA, 0, null as ISequenceItem);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(Alphabets.DNA, 0, null as ISequence);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(
                    Alphabets.DNA,
                    -1,
                    new List<ISequenceItem>() { 
                        Alphabets.DNA.A, 
                        Alphabets.DNA.C });

                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsNull(tempSeq);
            }

            try
            {
                tempSeq = new SparseSequence(
                   Alphabets.DNA,
                   int.MaxValue,
                   new List<ISequenceItem>() { 
                        Alphabets.DNA.A, 
                        Alphabets.DNA.C });
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsNull(tempSeq);
            }

            #endregion Test constructors by passing invalid parameters.

            // Test constructor which takes only Alphabet for the IsReadOnly property.
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);
            Assert.IsFalse(sparseSeq.IsReadOnly);
            sparseSeq.Insert(0, 'C');
            Assert.AreEqual(sparseSeq.Count, 1);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);

            sparseSeq = new SparseSequence(Alphabets.DNA, 0, Alphabets.DNA.A);

            string id = Guid.NewGuid().ToString(string.Empty);
            sparseSeq.ID = id;
            sparseSeq.DisplayID = "SparseSeq1";

            #region Test parameters

            Assert.AreSame(Alphabets.DNA, sparseSeq.Alphabet);
            Assert.IsTrue(sparseSeq.IsReadOnly);
            Assert.AreSame(sparseSeq.Complement[0], Alphabets.DNA.T);
            Assert.AreEqual(sparseSeq.Count, 1);
            Assert.AreEqual(sparseSeq.DisplayID, "SparseSeq1");
            Assert.AreEqual(sparseSeq.ID, id);

            #endregion Test parameters

            #region Test editing with IsReadOnly set to true.

            try
            {
                sparseSeq.Add(Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.Clear();
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.Insert(0, 'C');
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.Insert(0, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.InsertRange(0, "CGA");
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.Remove(Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.RemoveAt(0);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.RemoveRange(0, 2);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
            }

            try
            {
                sparseSeq.Replace(0, 'C');
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            }

            try
            {
                sparseSeq.Replace(0, Alphabets.DNA.C);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            }

            try
            {
                sparseSeq.ReplaceRange(0, "G");
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.AreEqual(sparseSeq.Count, 1);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            }

            #endregion Test editing with IsReadOnly set to true.

            #region Test not supported members
            string sequencedata = string.Empty;

            try
            {
                sequencedata = sparseSeq.ToString();
                Assert.Fail();
            }
            catch (NotSupportedException)
            {
                Assert.IsEmpty(sequencedata);
            }

            #endregion Test not supported members

            Assert.AreEqual(sparseSeq.IndexOf(Alphabets.DNA.A), 0);
            Assert.AreSame(sparseSeq.Range(0, 1)[0], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq.Reverse[0], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq.ReverseComplement[0], Alphabets.DNA.T);
            Assert.IsTrue(sparseSeq.Contains(Alphabets.DNA.A));

            ISequenceItem[] seqItemArray = new Nucleotide[10];
            seqItemArray[0] = Alphabets.DNA.G;
            sparseSeq.CopyTo(seqItemArray, 1);
            Assert.AreSame(seqItemArray[0], Alphabets.DNA.G);
            Assert.AreSame(seqItemArray[1], Alphabets.DNA.A);

            try
            {
                sparseSeq.CopyTo(null, 1);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }

            Assert.IsTrue(sparseSeq.IsReadOnly);

            sparseSeq.IsReadOnly = false;

            #region Test editing with IsReadOnly set to false.

            #region Test - void Add(ISequenceItem)

            sparseSeq.Add(Alphabets.DNA.G);
            Assert.AreEqual(sparseSeq.Count, 2);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);

            try
            {
                sparseSeq.Add(Alphabets.RNA.U);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 2);
            }

            #endregion Test - void Add(ISequenceItem)

            #region Test - void Clear()

            sparseSeq.Clear();
            Assert.AreEqual(sparseSeq.Count, 0);

            #endregion Test - void Clear()

            #region Test - void Insert(int position, char character)
            sparseSeq.Insert(0, 'C');
            Assert.AreEqual(sparseSeq.Count, 1);

            sparseSeq.Insert(sparseSeq.Count, 'A');
            Assert.AreEqual(sparseSeq.Count, 2);

            try
            {
                sparseSeq.Insert(-1, 'A');
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 2);
            }

            try
            {
                sparseSeq.Insert(sparseSeq.Count + 1, 'A');
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 2);
            }

            try
            {
                sparseSeq.Insert(0, 'U');
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 2);
            }

            #endregion Test - void Insert(int position, char character)

            #region Test - void Insert(int index, ISequenceItem item)

            sparseSeq.Insert(0, Alphabets.DNA.G);
            Assert.AreEqual(sparseSeq.Count, 3);

            sparseSeq.Insert(sparseSeq.Count, Alphabets.DNA.G);
            Assert.AreEqual(sparseSeq.Count, 4);

            try
            {
                sparseSeq.Insert(-1, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 4);
            }

            try
            {
                sparseSeq.Insert(sparseSeq.Count + 1, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 4);
            }

            try
            {
                sparseSeq.Insert(0, Alphabets.RNA.U);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 4);
            }

            #endregion Test - void Insert(int index, ISequenceItem item)

            #region Test - Count
            sparseSeq.Clear();
            Assert.AreEqual(sparseSeq.Count, 0);
            sparseSeq.Count = 5;

            Assert.IsNull(sparseSeq[0]);
            Assert.IsNull(sparseSeq[1]);
            Assert.IsNull(sparseSeq[2]);
            Assert.IsNull(sparseSeq[3]);
            Assert.IsNull(sparseSeq[4]);

            sparseSeq[2] = Alphabets.DNA.G;
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.IsNull(sparseSeq[0]);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.G);
            Assert.IsNull(sparseSeq[3]);
            Assert.IsNull(sparseSeq[4]);

            sparseSeq.Insert(2, 'A');
            Assert.AreEqual(sparseSeq.Count, 6);
            Assert.IsNull(sparseSeq[0]);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.IsNull(sparseSeq[4]);
            Assert.IsNull(sparseSeq[5]);

            sparseSeq.Insert(4, Alphabets.DNA.C);
            Assert.AreEqual(sparseSeq.Count, 7);
            Assert.IsNull(sparseSeq[0]);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[5]);
            Assert.IsNull(sparseSeq[6]);
            #endregion Test - Count

            #region Test - void InsertRange(int position, string sequence)

            sparseSeq.InsertRange(0, "CGA");
            Assert.AreEqual(sparseSeq.Count, 10);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[3]);
            Assert.IsNull(sparseSeq[4]);
            Assert.AreSame(sparseSeq[5], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[6], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[7], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[8]);
            Assert.IsNull(sparseSeq[9]);

            try
            {
                sparseSeq.InsertRange(-1, "CGA");
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 10);
            }

            try
            {
                sparseSeq.InsertRange(sparseSeq.Count + 1, "CGA");
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 10);
            }

            try
            {
                sparseSeq.InsertRange(0, string.Empty);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(sparseSeq.Count, 10);
            }

            try
            {
                sparseSeq.InsertRange(0, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(sparseSeq.Count, 10);
            }

            #endregion Test - void InsertRange(int position, string sequence)

            #region Test - bool Remove(ISequenceItem item)

            Assert.IsTrue(sparseSeq.Remove(Alphabets.DNA.A));
            Assert.AreEqual(sparseSeq.Count, 9);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.IsNull(sparseSeq[2]);
            Assert.IsNull(sparseSeq[3]);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[5], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[6], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[7]);
            Assert.IsNull(sparseSeq[8]);

            Assert.IsFalse(sparseSeq.Remove(Alphabets.DNA.T));

            Assert.IsFalse(sparseSeq.Remove(Alphabets.RNA.U));
            Assert.AreEqual(sparseSeq.Count, 9);

            #endregion Test - bool Remove(ISequenceItem item)

            #region Test - void RemoveAt(int index)
            sparseSeq.RemoveAt(2);
            Assert.AreEqual(sparseSeq.Count, 8);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.IsNull(sparseSeq[2]);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[5], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[6]);
            Assert.IsNull(sparseSeq[7]);

            try
            {
                sparseSeq.RemoveAt(-1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 8);
            }

            try
            {
                sparseSeq.RemoveAt(sparseSeq.Count);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 8);
            }

            #endregion Test - void RemoveAt(int index)

            #region Test - void RemoveRange(int position, int length)
            sparseSeq.RemoveRange(1, 3);
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[3]);
            Assert.IsNull(sparseSeq[4]);

            try
            {
                sparseSeq.RemoveRange(-1, 1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
            }

            try
            {
                sparseSeq.RemoveRange(sparseSeq.Count, 1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
            }

            try
            {
                sparseSeq.RemoveRange(0, -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
            }

            try
            {
                sparseSeq.RemoveRange(2, 4);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
            }

            #endregion Test - void RemoveRange(int position, int length)

            #region Test - void Replace(int position, char character)
            sparseSeq.Replace(3, 'A');
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[4]);

            sparseSeq.Replace(2, 'T');
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[4]);


            try
            {
                sparseSeq.Replace(-1, 'C');
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.Replace(sparseSeq.Count, 'C');
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.Replace(2, 'U');
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.G);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
                Assert.IsNull(sparseSeq[4]);
            }

            #endregion Test - void Replace(int position, char character)

            #region Test - void Replace(int position, ISequenceItem item)
            sparseSeq.Replace(1, null);
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[4]);

            sparseSeq.Replace(3, Alphabets.DNA.T);
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
            Assert.IsNull(sparseSeq[4]);

            sparseSeq.Replace(1, Alphabets.DNA.C);
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
            Assert.IsNull(sparseSeq[4]);

            // try replacing null with null.
            sparseSeq[4] = null;

            try
            {
                sparseSeq.Replace(-1, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.Replace(sparseSeq.Count, Alphabets.DNA.A);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.Replace(2, Alphabets.RNA.U);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }
            #endregion Test - void Replace(int position, ISequenceItem item)

            #region Test - void ReplaceRange(int position, string sequence)

            try
            {
                sparseSeq.ReplaceRange(-1, "GC");
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.ReplaceRange(sparseSeq.Count, "GC");
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.ReplaceRange(0, string.Empty);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            try
            {
                sparseSeq.ReplaceRange(0, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(sparseSeq.Count, 5);
                Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
                Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
                Assert.AreSame(sparseSeq[3], Alphabets.DNA.T);
                Assert.IsNull(sparseSeq[4]);
            }

            sparseSeq.ReplaceRange(3, "GC");
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[1], Alphabets.DNA.C);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.C);

            #endregion Test - void ReplaceRange(int position, string sequence)

            #region Test - ISequenceItem this[int index]

            sparseSeq[1] = null;
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.C);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.C);

            sparseSeq[0] = Alphabets.DNA.A;
            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.C);

            try
            {
                sparseSeq[-1] = Alphabets.DNA.A;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            try
            {
                sparseSeq[sparseSeq.Count] = Alphabets.DNA.A;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            Assert.AreEqual(sparseSeq.Count, 5);
            Assert.AreSame(sparseSeq[0], Alphabets.DNA.A);
            Assert.IsNull(sparseSeq[1]);
            Assert.AreSame(sparseSeq[2], Alphabets.DNA.T);
            Assert.AreSame(sparseSeq[3], Alphabets.DNA.G);
            Assert.AreSame(sparseSeq[4], Alphabets.DNA.C);

            #endregion Test - ISequenceItem this[int index]

            #region Test - IList<IndexedSequenceItem> GetKnownSequenceItems

            IList<IndexedItem<ISequenceItem>> knownItems = sparseSeq.GetKnownSequenceItems();
            Assert.AreEqual(knownItems.Count, 4);
            Assert.AreEqual(knownItems[0].Index, 0);
            Assert.AreSame(knownItems[0].Item, Alphabets.DNA.A);
            Assert.AreEqual(knownItems[1].Index, 2);
            Assert.AreSame(knownItems[1].Item, Alphabets.DNA.T);
            Assert.AreEqual(knownItems[2].Index, 3);
            Assert.AreSame(knownItems[2].Item, Alphabets.DNA.G);
            Assert.AreEqual(knownItems[3].Index, 4);
            Assert.AreSame(knownItems[3].Item, Alphabets.DNA.C);

            #endregion Test - IList<IndexedSequenceItem> GetKnownSequenceItems

            #region Test - IEnumerator<ISequenceItem> GetEnumerator()
            int i = 0;
            foreach (ISequenceItem item in sparseSeq)
            {
                Assert.AreSame(sparseSeq[i++], item);
            }

            #endregion Test - IEnumerator<ISequenceItem> GetEnumerator()

            #endregion Test editing with IsReadOnly set to false.

            #region Test - Constructor by passing a sparse sequence.

            // Create new sparse sequence from a sparse sequence by adding sequence item from index 1.
            tempSeq = new SparseSequence(sparseSeq.Alphabet, 1, sparseSeq);

            Assert.AreEqual(tempSeq.Count, 6);
            Assert.IsNull(tempSeq[0]);
            Assert.AreSame(tempSeq[1], Alphabets.DNA.A);
            Assert.IsNull(tempSeq[2]);
            Assert.AreSame(tempSeq[3], Alphabets.DNA.T);
            Assert.AreSame(tempSeq[4], Alphabets.DNA.G);
            Assert.AreSame(tempSeq[5], Alphabets.DNA.C);
            #endregion Test - Constructor by passing a sparse sequence.

            #region Test - Complement
            ISequence compSeq = sparseSeq.Complement;

            Assert.AreEqual(compSeq.Count, 5);
            Assert.AreSame(compSeq[0], Alphabets.DNA.T);
            Assert.IsNull(compSeq[1]);
            Assert.AreSame(compSeq[2], Alphabets.DNA.A);
            Assert.AreSame(compSeq[3], Alphabets.DNA.C);
            Assert.AreSame(compSeq[4], Alphabets.DNA.G);
            #endregion Test - Complement

            #region Test - Reverse
            compSeq = sparseSeq.Reverse;
            Assert.AreEqual(compSeq.Count, 5);
            Assert.AreSame(compSeq[4], Alphabets.DNA.A);
            Assert.IsNull(compSeq[3]);
            Assert.AreSame(compSeq[2], Alphabets.DNA.T);
            Assert.AreSame(compSeq[1], Alphabets.DNA.G);
            Assert.AreSame(compSeq[0], Alphabets.DNA.C);
            #endregion Test - Reverse

            #region Test - ReverseComplement

            compSeq = sparseSeq.ReverseComplement;
            Assert.AreEqual(compSeq.Count, 5);
            Assert.AreSame(compSeq[4], Alphabets.DNA.T);
            Assert.IsNull(compSeq[3]);
            Assert.AreSame(compSeq[2], Alphabets.DNA.A);
            Assert.AreSame(compSeq[1], Alphabets.DNA.C);
            Assert.AreSame(compSeq[0], Alphabets.DNA.G);
            #endregion Test - ReverseComplement

            #region Test - IndexOfNotNull and LastIndexOfNotNull
            sparseSeq = new SparseSequence(Alphabets.DNA);
            sparseSeq.Count = 1000;
            sparseSeq[9] = null;
            sparseSeq[10] = Alphabets.DNA.Gap;
            sparseSeq[20] = Alphabets.DNA.GA;
            sparseSeq[501] = Alphabets.DNA.A;
            sparseSeq[905] = Alphabets.DNA.Gap;
            sparseSeq[906] = null;
            Assert.AreEqual(10, sparseSeq.IndexOfNotNull());
            Assert.AreEqual(20, sparseSeq.IndexOfNotNull(10));
            Assert.AreEqual(10, sparseSeq.IndexOfNotNull(9));
            Assert.AreEqual(10, sparseSeq.IndexOfNotNull(0));
            Assert.AreEqual(905, sparseSeq.IndexOfNotNull(501));
            Assert.AreEqual(-1, sparseSeq.IndexOfNotNull(sparseSeq.Count));

            Assert.AreEqual(905, sparseSeq.LastIndexOfNotNull());
            Assert.AreEqual(501, sparseSeq.LastIndexOfNotNull(905));
            Assert.AreEqual(905, sparseSeq.LastIndexOfNotNull(906));
            Assert.AreEqual(905, sparseSeq.LastIndexOfNotNull(sparseSeq.Count));
            Assert.AreEqual(10, sparseSeq.LastIndexOfNotNull(20));
            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull(10));
            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull(0));
          
            sparseSeq = new SparseSequence(Alphabets.DNA);
            sparseSeq.Count = 1000;
            Assert.AreEqual(-1, sparseSeq.IndexOfNotNull());
            Assert.AreEqual(-1, sparseSeq.IndexOfNotNull(10));
            Assert.AreEqual(-1, sparseSeq.IndexOfNotNull(sparseSeq.Count));

            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull());
            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull(300));
            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull(sparseSeq.Count));
            Assert.AreEqual(-1, sparseSeq.LastIndexOfNotNull(0));

            #endregion Test - IndexOfNotNull and LastIndexOfNotNull

            #region Test - IndexOfNonGap and LastIndexOfNonGap

            sparseSeq = new SparseSequence(Alphabets.DNA);
            sparseSeq.Count = 1000;
            sparseSeq[9] = null;
            sparseSeq[10] = Alphabets.DNA.Gap;
            sparseSeq[20] = Alphabets.DNA.GA;
            sparseSeq[501] = Alphabets.DNA.A;
            sparseSeq[905] = Alphabets.DNA.Gap;
            sparseSeq[906] = null;
            Assert.AreEqual(20, sparseSeq.IndexOfNonGap());
            Assert.AreEqual(20, sparseSeq.IndexOfNonGap(10));
            Assert.AreEqual(20, sparseSeq.IndexOfNonGap(20));
            Assert.AreEqual(501, sparseSeq.LastIndexOfNonGap());
            Assert.AreEqual(501, sparseSeq.LastIndexOfNonGap(910));
            Assert.AreEqual(501, sparseSeq.LastIndexOfNonGap(905));
            Assert.AreEqual(501, sparseSeq.LastIndexOfNonGap(501));

            #endregion Test - IndexOfNonGap and LastIndexOfNonGap


        }
    }
}
