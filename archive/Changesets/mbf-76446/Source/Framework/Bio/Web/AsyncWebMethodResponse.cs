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
using System.IO;

namespace Bio.Web
{
    /// <summary>
    /// This class represent the response of asynchronous web method class.
    /// </summary>
    public class AsyncWebMethodResponse
    {
        /// <summary>
        /// State of the Async web method
        /// </summary>
        private object state = null;

        /// <summary>
        /// Default constructor: Initializes an instance of class AsyncWebMethodResponse
        /// </summary>
        /// <param name="state">State of the Async web method</param>
        public AsyncWebMethodResponse(object state)
        {
            this.state = state;
        }

        /// <summary>
        /// Gets or sets current state of the Async web method.
        /// </summary>
        public AsyncMethodState Status { get; set; }

        /// <summary>
        /// Gets Exception (if any) of the Async web method.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Gets or sets the description of Async web method status.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the output of the Async web method.
        /// </summary>
        public Stream Result { get; set; }

        /// <summary>
        /// Gets the state of async web method
        /// </summary>
        public object State
        {
            get { return state; }
        }
    }
}
