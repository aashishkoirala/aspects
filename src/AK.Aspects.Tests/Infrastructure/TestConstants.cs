/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.TestConstants
 * Copyright © 2014 Aashish Koirala <http://aashishkoirala.github.io>
 * 
 * This file is part of Aspects for .NET.
 *  
 * Aspects for .NET is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Aspects for .NET is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Aspects for .NET.  If not, see <http://www.gnu.org/licenses/>.
 * 
 *******************************************************************************************************************************/

#region Namespace Imports

using System;
using System.Collections.Generic;

#endregion

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Values to use for various parameters across the tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public static class TestConstants
    {
        public const string NormalPropertyGetValue = "A";
        public const string NormalPropertySetValue = "B";
        public const int ReadOnlyPropertyGetValue = 1;
        public const decimal WriteOnlyPropertySetValue = 2.3M;
        public const string IndexerParameterValue = "C";
        public const string IndexerGetValue = "D";
        public const string IndexerSetValue = "E";
        public const int VoidMethodWithParametersParameter1Value = 4;
        public const string VoidMethodWithParametersParameter2Value = "F";
        public const string ReturnMethodWithNoParametersReturnValue = "G";
        public const int ReturnMethodWithParametersParameter1Value = 5;
        public const string ReturnMethodWithParametersParameter2Value = "H";
        public const int ReturnMethodWithParametersReturnValue = 6;
        public const string MethodWithRefAndOutParametersOutParameter1Value = "I";
        public static readonly IList<int> MethodWithRefAndOutParametersRefParameter2InitialValue = new List<int> {7, 8};
        public static readonly IList<int> MethodWithRefAndOutParametersRefParameter2FinalValue = new List<int> {9, 10};
        public static readonly Type GenericMethodTypeParameter = typeof (DateTime);
        public static readonly object GenericMethodParameter1Value = DateTime.Today;
        public const string GenericMethodParameter2Value = "L";
        public static readonly object GenericMethodReturnValue = DateTime.Today.AddDays(-1);
        public static readonly Exception MethodThatThrowsException = new ApplicationException("Test Exception");

        public static class Properties
        {
            public const string NormalProperty = "NormalProperty";
            public const string ReadOnlyProperty = "ReadOnlyProperty";
            public const string WriteOnlyProperty = "WriteOnlyProperty";
            public const string Item = "Item";
        }

        public static class Methods
        {
            public const string VoidMethodWithNoParameters = "VoidMethodWithNoParameters";
            public const string VoidMethodWithParameters = "VoidMethodWithParameters";
            public const string ReturnMethodWithNoParameters = "ReturnMethodWithNoParameters";
            public const string ReturnMethodWithParameters = "ReturnMethodWithParameters";
            public const string MethodWithRefAndOutParameters = "MethodWithRefAndOutParameters";
            public const string GenericMethod = "GenericMethod";
            public const string MethodThatThrows = "MethodThatThrows";
        }
    }
}