/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.TestExitAttribute
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
    /// Exit aspect for wrapping tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public class TestExitAttribute : Attribute, IExitAspect
    {
        public int Order
        {
            get { return 1; }
        }

        public void Execute(MemberInfo memberInfo, IDictionary<string, object> parameters, 
            ref object returnValue, TimeSpan duration)
        {
            switch (memberInfo.Name)
            {
                case TestConstants.Methods.VoidMethodWithNoParameters:
                    TestResultBag.VoidMethodWithNoParametersExitCalled = true;
                    break;

                case TestConstants.Methods.VoidMethodWithParameters:
                    TestResultBag.VoidMethodWithParametersExitCalled = true;
                    break;

                case TestConstants.Methods.ReturnMethodWithNoParameters:
                    TestResultBag.ReturnMethodWithNoParametersExitCalled = true;
                    TestResultBag.ReturnMethodWithNoParametersReturnValue = (string) returnValue;
                    break;

                case TestConstants.Methods.ReturnMethodWithParameters:
                    TestResultBag.ReturnMethodWithParametersExitCalled = true;
                    TestResultBag.ReturnMethodWithParametersReturnValue = (int) returnValue;
                    break;

                case TestConstants.Methods.MethodWithRefAndOutParameters:
                    TestResultBag.MethodWithRefAndOutParametersExitCalled = true;
                    TestResultBag.MethodWithRefAndOutParametersOutParameter1Value = (string) parameters["outParameter1"];
                    TestResultBag.MethodWithRefAndOutParametersRefParameter2FinalValue =
                        (IList<int>) parameters["refParameter2"];
                    break;

                case TestConstants.Methods.GenericMethod:
                    TestResultBag.GenericMethodExitCalled = true;
                    TestResultBag.GenericMethodReturnValue = returnValue;
                    break;

                case TestConstants.Methods.MethodThatThrows:
                    TestResultBag.MethodThatThrowsExitCalled = true;
                    break;

                case TestConstants.Properties.NormalProperty:
                    TestResultBag.NormalPropertyExitCalled = true;
                    if (returnValue != null) TestResultBag.NormalPropertyGetValue = (string) returnValue;
                    break;

                case TestConstants.Properties.ReadOnlyProperty:
                    TestResultBag.ReadOnlyPropertyExitCalled = true;
                    TestResultBag.ReadOnlyPropertyGetValue = (int) returnValue;
                    break;

                case TestConstants.Properties.WriteOnlyProperty:
                    TestResultBag.WriteOnlyPropertyExitCalled = true;
                    break;

                case TestConstants.Properties.Item:
                    TestResultBag.IndexerExitCalled = true;
                    TestResultBag.IndexerParameterValue = (string) parameters["indexParameter"];
                    if (returnValue != null) TestResultBag.IndexerGetValue = (string) returnValue;
                    break;
            }
        }
    }
}