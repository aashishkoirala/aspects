/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.TestImplementation
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

using System.Collections.Generic;
using System.ComponentModel.Composition;

#endregion

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Implementation for wrapping tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [TestEntry, TestExit, TestError, Export(typeof(ITestContract))]
    public class TestImplementation : ITestContract
    {
        public string NormalProperty
        {
            get
            {
                TestResultBag.NormalPropertyGetterCalled = true;
                return TestConstants.NormalPropertyGetValue;
            }
            set { TestResultBag.NormalPropertySetterCalled = true; }
        }

        public int ReadOnlyProperty
        {
            get
            {
                TestResultBag.ReadOnlyPropertyGetterCalled = true;
                return TestConstants.ReadOnlyPropertyGetValue;
            }
        }

        public decimal WriteOnlyProperty
        {
            set { TestResultBag.WriteOnlyPropertySetterCalled = true; }
        }

        public string this[string indexParameter]
        {
            get
            {
                TestResultBag.IndexerGetterCalled = true;
                return TestConstants.IndexerGetValue;
            }
            set { TestResultBag.IndexerSetterCalled = true; }
        }

        public void VoidMethodWithNoParameters()
        {
            TestResultBag.VoidMethodWithNoParametersCalled = true;
        }

        public void VoidMethodWithParameters(int parameter1, string parameter2)
        {
            TestResultBag.VoidMethodWithParametersCalled = true;
        }

        public string ReturnMethodWithNoParameters()
        {
            TestResultBag.ReturnMethodWithNoParametersCalled = true;

            return TestConstants.ReturnMethodWithNoParametersReturnValue;
        }

        public int ReturnMethodWithParameters(int parameter1, string parameter2)
        {
            TestResultBag.ReturnMethodWithParametersCalled = true;

            return TestConstants.ReturnMethodWithParametersReturnValue;
        }

        public void MethodWithRefAndOutParameters(out string outParameter1, ref IList<int> refParameter2)
        {
            TestResultBag.MethodWithRefAndOutParametersCalled = true;

            outParameter1 = TestConstants.MethodWithRefAndOutParametersOutParameter1Value;
            refParameter2 = TestConstants.MethodWithRefAndOutParametersRefParameter2FinalValue;
        }

        public T GenericMethod<T>(T parameter1, string parameter2)
        {
            TestResultBag.GenericMethodCalled = true;
            TestResultBag.GenericMethodTypeParameter = typeof (T);
            return (T) TestConstants.GenericMethodReturnValue;
        }

        public string MethodThatThrows()
        {
            TestResultBag.MethodThatThrowsCalled = true;
            throw TestConstants.MethodThatThrowsException;
        }
    }
}