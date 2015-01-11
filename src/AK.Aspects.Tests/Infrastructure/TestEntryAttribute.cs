/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.TestEntryAttribute
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
using System.Reflection;

#endregion

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Entry aspect for wrapping tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public class TestEntryAttribute : Attribute, IEntryAspect
    {
        public int Order
        {
            get { return 1; }
        }

        public bool Execute(MemberInfo memberInfo, IDictionary<string, object> parameters, ref object returnValue)
        {
            switch (memberInfo.Name)
            {
                case TestConstants.Methods.VoidMethodWithNoParameters:
                    TestResultBag.VoidMethodWithNoParametersEntryCalled = true;
                    break;

                case TestConstants.Methods.VoidMethodWithParameters:
                    TestResultBag.VoidMethodWithParametersEntryCalled = true;
                    TestResultBag.VoidMethodWithParametersParameter1Value = (int) parameters["parameter1"];
                    TestResultBag.VoidMethodWithParametersParameter2Value = (string) parameters["parameter2"];
                    break;

                case TestConstants.Methods.ReturnMethodWithNoParameters:
                    TestResultBag.ReturnMethodWithNoParametersEntryCalled = true;
                    break;

                case TestConstants.Methods.ReturnMethodWithParameters:
                    TestResultBag.ReturnMethodWithParametersEntryCalled = true;
                    TestResultBag.ReturnMethodWithParametersParameter1Value = (int) parameters["parameter1"];
                    TestResultBag.ReturnMethodWithParametersParameter2Value = (string) parameters["parameter2"];
                    break;

                case TestConstants.Methods.MethodWithRefAndOutParameters:
                    TestResultBag.MethodWithRefAndOutParametersEntryCalled = true;
                    TestResultBag.MethodWithRefAndOutParametersRefParameter2InitialValue =
                        (IList<int>) parameters["refParameter2"];
                    break;

                case TestConstants.Methods.GenericMethod:
                    TestResultBag.GenericMethodEntryCalled = true;
                    TestResultBag.GenericMethodTypeParameter = (Type) parameters["T1"];
                    TestResultBag.GenericMethodParameter1Value = parameters["parameter1"];
                    TestResultBag.GenericMethodParameter2Value = (string) parameters["parameter2"];
                    break;

                case TestConstants.Methods.MethodThatThrows:
                    TestResultBag.MethodThatThrowsEntryCalled = true;
                    break;

                case TestConstants.Properties.NormalProperty:
                    TestResultBag.NormalPropertyEntryCalled = true;
                    if (parameters.ContainsKey("value"))
                        TestResultBag.NormalPropertySetValue = (string) parameters["value"];
                    break;

                case TestConstants.Properties.ReadOnlyProperty:
                    TestResultBag.ReadOnlyPropertyEntryCalled = true;
                    break;

                case TestConstants.Properties.WriteOnlyProperty:
                    TestResultBag.WriteOnlyPropertyEntryCalled = true;
                    if (parameters.ContainsKey("value"))
                        TestResultBag.WriteOnlyPropertySetValue = (decimal) parameters["value"];
                    break;

                case TestConstants.Properties.Item:
                    TestResultBag.IndexerEntryCalled = true;
                    TestResultBag.IndexerParameterValue = (string) parameters["indexParameter"];
                    if (parameters.ContainsKey("value")) TestResultBag.IndexerSetValue = (string) parameters["value"];
                    break;
            }
            return true;
        }
    }
}