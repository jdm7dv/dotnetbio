﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBF.Web
{
    /// <summary>
    /// This Enumeration represents state of asynchronous web method call process.
    /// </summary>
    public enum AsyncMethodState
    {
        /// <summary>
        /// Not started
        /// </summary>
        NotStarted,
        /// <summary>
        /// Started
        /// </summary>
        Started,
        /// <summary>
        /// Passed
        /// </summary>
        Passed,
        /// <summary>
        /// Failed
        /// </summary>
        Failed,
    }
}
