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
using System.Globalization;

namespace DeveloperPreRequisiteCheck
{
    /// <summary>
    /// This class runs through all the compenents and validate there version compatibility.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// File to be which script to reset environment variable has to be written
        /// </summary>
        private const string ResetEnvVariableBatchFile = "ResetEnvironmentVariable.bat";

        /// <summary>
        /// Flag to check whether the validation should run in silent mode.
        /// </summary>
        private bool _IsValidationSilent = false;

        /// <summary>
        /// Default constructor: Initialized an instance of Validator class.
        /// </summary>
        /// <param name="isValidationSilent">Is validation silent.</param>
        public Validator(bool isValidationSilent)
        {
            _IsValidationSilent = isValidationSilent;
        }

        /// <summary>
        /// Initialize validators and validate all the components
        /// </summary>
        public int Validate()
        {
            bool validationSuccessful = true;

            // Initialize the components.
            IList<IComponentValidator> components = Initialize();
            string lineBreak = "";

            lineBreak = lineBreak.PadLeft(80, '*');
            // Validate the components.
            foreach (IComponentValidator component in components)
            {
                ValidationResult result = component.Validate();

                if (result.Result == false)
                {
                    validationSuccessful = false;
                }

                // Write an empty line.
                Console.WriteLine(String.Empty);
                Console.WriteLine(result.Message);

                Console.WriteLine(String.Empty);
                Console.WriteLine(lineBreak);
            }

            if (!_IsValidationSilent && !validationSuccessful)
            {
                Console.WriteLine(Properties.Resources.PRESS_KEY);
                Console.ReadKey();
            }

            if (validationSuccessful)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// Initialize the validators
        /// </summary>
        /// <returns></returns>
        private IList<IComponentValidator> Initialize()
        {
            IList<IComponentValidator> components = new List<IComponentValidator>();
            Utility.DeleteFile(ResetEnvVariableBatchFile);

            components.Add(new ExcelValidator());
		   
		    components.Add(new VSTOValidator());
			  
            return components;
        }
    }
}
