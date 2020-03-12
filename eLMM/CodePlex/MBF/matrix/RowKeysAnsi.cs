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
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bio.Matrix
{
    /// <summary>
    /// Be sure to use this is a "Using" to that it gets disposed correctly.
    /// </summary>
    public class RowKeysAnsi : RowKeysStructMatrix<char>
    {
        #pragma warning disable 1591
        protected override char ByteArrayToValueOrMissing(byte[] byteArray)
        #pragma warning restore 1591
        {
            return (char)byteArray[0];
        }

#pragma warning disable 1591
        protected override byte[] ValueOrMissingToByteArray(char value)
#pragma warning restore 1591
        {
            byte[] byteArray = new byte[] { (byte)value };
            return byteArray;
        }


        #pragma warning disable 1591
        protected override int BytesPerValue
        #pragma warning restore 1591
        {
            get
            {
                return 1;
            }
        }

        #pragma warning disable 1591
        public override char MissingValue
        #pragma warning restore 1591
        {
            get
            {
                return '?';
            }
        }

        /// <summary>
        /// Create an instance of RowKeysAnsi from a file in DenseAnsi format.
        /// </summary>
        /// <param name="denseAnsiFileName">The DenseAnsi file</param>
        /// <param name="parallelOptions">A ParallelOptions instance that configures the multithreaded behavior of this operation.</param>
        /// <param name="fileAccess">A FileAccess value that specifies the operations that can be performed on the file. Defaults to 'Read'</param>
        /// <param name="fileShare">A FileShare value specifying the type of access other threads have to the file. Defaults to 'Read'</param>
        /// <returns>A RowKeysAnsi object</returns>
        public static RowKeysAnsi GetInstanceFromDenseAnsi(string denseAnsiFileName, ParallelOptions parallelOptions, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read)
        {
            RowKeysAnsi rowKeysAnsi = new RowKeysAnsi();
            rowKeysAnsi.GetInstanceFromDenseStructFileNameInternal(denseAnsiFileName, parallelOptions, fileAccess, fileShare);
            return rowKeysAnsi;
        }



        /// <summary>
        /// Create an instance of RowKeysAnsi from a file in RowKeysAnsi format.
        /// </summary>
        /// <param name="parallelOptions">A ParallelOptions instance that configures the multithreaded behavior of this operation.</param>
        /// <param name="rowKeysAnsiFileName">The rowKeys ansi file</param>
        /// <param name="fileAccess">A FileAccess value that specifies the operations that can be performed on the file. Defaults to 'Read'</param>
        /// <param name="fileShare">A FileShare value specifying the type of access other threads have to the file. Defaults to 'Read'</param>
        /// <returns></returns>
        public static RowKeysAnsi GetInstanceFromRowKeysAnsi(string rowKeysAnsiFileName, ParallelOptions parallelOptions, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read)
        {
            RowKeysAnsi rowKeysAnsi = new RowKeysAnsi();
            rowKeysAnsi.GetInstanceFromRowKeysStructFileNameInternal(rowKeysAnsiFileName, parallelOptions, fileAccess, fileShare);
            return rowKeysAnsi;
        }
    }
}
