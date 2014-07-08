/*******************************************************************************************************************************
 * AK.Aspects.Tests.Integration.WrappingTests
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

using AK.Aspects.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace AK.Aspects.Tests.Integration
{
    /// <summary>
    /// Wrapping related integration tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [TestClass]
    public class WrappingTests
    {
        private ITestContract target;

        [TestInitialize]
        public void Initialize()
        {
            AspectHelper.CodeGenerated += (sender, args) => Console.WriteLine(args.GeneratedCode);
            this.target = AspectHelper.Wrap<ITestContract>(new TestImplementation());

            TestResultBag.Nullify();
        }

        [TestMethod, TestCategory("Integration")]
        public void Normal_Property_Get_And_Set_Work()
        {
            var value = this.target.NormalProperty;

            Assert.IsTrue(TestResultBag.NormalPropertyGetterCalled);
            Assert.AreEqual(value, TestConstants.NormalPropertyGetValue);
            Assert.AreEqual(TestResultBag.NormalPropertyGetValue, TestConstants.NormalPropertyGetValue);
            Assert.IsTrue(TestResultBag.NormalPropertyEntryCalled);
            Assert.IsTrue(TestResultBag.NormalPropertyExitCalled);

            TestResultBag.Nullify();
            this.target.NormalProperty = TestConstants.NormalPropertySetValue;

            Assert.IsTrue(TestResultBag.NormalPropertySetterCalled);
            Assert.IsTrue(TestResultBag.NormalPropertyEntryCalled);
            Assert.IsTrue(TestResultBag.NormalPropertyExitCalled);
            Assert.AreEqual(TestResultBag.NormalPropertySetValue, TestConstants.NormalPropertySetValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Read_Only_Property_Get_Works()
        {
            var value = this.target.ReadOnlyProperty;

            Assert.IsTrue(TestResultBag.ReadOnlyPropertyGetterCalled);
            Assert.AreEqual(value, TestConstants.ReadOnlyPropertyGetValue);
            Assert.AreEqual(TestResultBag.ReadOnlyPropertyGetValue, TestConstants.ReadOnlyPropertyGetValue);
            Assert.IsTrue(TestResultBag.ReadOnlyPropertyEntryCalled);
            Assert.IsTrue(TestResultBag.ReadOnlyPropertyExitCalled);
        }

        [TestMethod, TestCategory("Integration")]
        public void Write_Only_Property_Set_Works()
        {
            this.target.WriteOnlyProperty = TestConstants.WriteOnlyPropertySetValue;

            Assert.IsTrue(TestResultBag.WriteOnlyPropertySetterCalled);
            Assert.IsTrue(TestResultBag.WriteOnlyPropertyEntryCalled);
            Assert.IsTrue(TestResultBag.WriteOnlyPropertyExitCalled);
            Assert.AreEqual(TestResultBag.WriteOnlyPropertySetValue, TestConstants.WriteOnlyPropertySetValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Indexer_Get_And_Set_Work()
        {
            var value = this.target[TestConstants.IndexerParameterValue];

            Assert.IsTrue(TestResultBag.IndexerGetterCalled);
            Assert.AreEqual(value, TestConstants.IndexerGetValue);
            Assert.AreEqual(TestResultBag.IndexerGetValue, TestConstants.IndexerGetValue);
            Assert.IsTrue(TestResultBag.IndexerEntryCalled);
            Assert.IsTrue(TestResultBag.IndexerExitCalled);
            Assert.AreEqual(TestResultBag.IndexerParameterValue, TestConstants.IndexerParameterValue);

            TestResultBag.Nullify();
            this.target[TestConstants.IndexerParameterValue] = TestConstants.IndexerSetValue;

            Assert.IsTrue(TestResultBag.IndexerSetterCalled);
            Assert.IsTrue(TestResultBag.IndexerEntryCalled);
            Assert.IsTrue(TestResultBag.IndexerExitCalled);
            Assert.AreEqual(TestResultBag.IndexerSetValue, TestConstants.IndexerSetValue);
            Assert.AreEqual(TestResultBag.IndexerParameterValue, TestConstants.IndexerParameterValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Void_Method_With_No_Parameters_Works()
        {
            this.target.VoidMethodWithNoParameters();

            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersEntryCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersExitCalled);
        }

        [TestMethod, TestCategory("Integration")]
        public void Void_Method_With_Parameters_Works()
        {
            this.target.VoidMethodWithParameters(TestConstants.VoidMethodWithParametersParameter1Value,
                                                 TestConstants.VoidMethodWithParametersParameter2Value);

            Assert.IsTrue(TestResultBag.VoidMethodWithParametersCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithParametersEntryCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithParametersExitCalled);
            Assert.AreEqual(TestResultBag.VoidMethodWithParametersParameter1Value,
                            TestConstants.VoidMethodWithParametersParameter1Value);
            Assert.AreEqual(TestResultBag.VoidMethodWithParametersParameter2Value,
                            TestConstants.VoidMethodWithParametersParameter2Value);
        }

        [TestMethod, TestCategory("Integration")]
        public void Return_Method_With_No_Parameters_Works()
        {
            var value = this.target.ReturnMethodWithNoParameters();

            Assert.IsTrue(TestResultBag.ReturnMethodWithNoParametersCalled);
            Assert.IsTrue(TestResultBag.ReturnMethodWithNoParametersEntryCalled);
            Assert.IsTrue(TestResultBag.ReturnMethodWithNoParametersExitCalled);
            Assert.AreEqual(value, TestConstants.ReturnMethodWithNoParametersReturnValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Return_Method_With_Parameters_Works()
        {
            var value = this.target.ReturnMethodWithParameters(TestConstants.ReturnMethodWithParametersParameter1Value,
                                                               TestConstants.ReturnMethodWithParametersParameter2Value);

            Assert.IsTrue(TestResultBag.ReturnMethodWithParametersCalled);
            Assert.IsTrue(TestResultBag.ReturnMethodWithParametersEntryCalled);
            Assert.IsTrue(TestResultBag.ReturnMethodWithParametersExitCalled);
            Assert.AreEqual(TestResultBag.ReturnMethodWithParametersParameter1Value,
                            TestConstants.ReturnMethodWithParametersParameter1Value);
            Assert.AreEqual(TestResultBag.ReturnMethodWithParametersParameter2Value,
                            TestConstants.ReturnMethodWithParametersParameter2Value);
            Assert.AreEqual(value, TestConstants.ReturnMethodWithParametersReturnValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Method_With_Ref_And_Out_Parameters_Works()
        {
            string outParameter1;
            var refParameter2 = TestConstants.MethodWithRefAndOutParametersRefParameter2InitialValue;

            this.target.MethodWithRefAndOutParameters(out outParameter1, ref refParameter2);

            Assert.IsTrue(TestResultBag.MethodWithRefAndOutParametersCalled);
            Assert.IsTrue(TestResultBag.MethodWithRefAndOutParametersEntryCalled);
            Assert.IsTrue(TestResultBag.MethodWithRefAndOutParametersExitCalled);
            Assert.AreEqual(outParameter1, TestConstants.MethodWithRefAndOutParametersOutParameter1Value);
            Assert.AreEqual(outParameter1, TestResultBag.MethodWithRefAndOutParametersOutParameter1Value);
            Assert.AreEqual(refParameter2, TestResultBag.MethodWithRefAndOutParametersRefParameter2FinalValue);
            Assert.AreEqual(refParameter2, TestConstants.MethodWithRefAndOutParametersRefParameter2FinalValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Generic_Method_Works()
        {
            var value = this.target.GenericMethod((DateTime) TestConstants.GenericMethodParameter1Value,
                                                  TestConstants.GenericMethodParameter2Value);

            Assert.IsTrue(TestResultBag.GenericMethodCalled);
            Assert.IsTrue(TestResultBag.GenericMethodEntryCalled);
            Assert.IsTrue(TestResultBag.GenericMethodExitCalled);
            Assert.AreEqual(TestResultBag.GenericMethodTypeParameter, TestConstants.GenericMethodTypeParameter);
            Assert.AreEqual(TestResultBag.GenericMethodParameter1Value, TestConstants.GenericMethodParameter1Value);
            Assert.AreEqual(TestResultBag.GenericMethodParameter2Value, TestConstants.GenericMethodParameter2Value);
            Assert.AreEqual(value, TestConstants.GenericMethodReturnValue);
            Assert.AreEqual(value, TestResultBag.GenericMethodReturnValue);
        }

        [TestMethod, TestCategory("Integration")]
        public void Method_That_Throws_Works()
        {
            try
            {
                this.target.MethodThatThrows();
                Assert.Fail("Expected exception to be thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(TestResultBag.MethodThatThrowsCalled);
                Assert.IsTrue(TestResultBag.MethodThatThrowsEntryCalled);
                Assert.IsTrue(TestResultBag.MethodThatThrowsExitCalled);
                Assert.AreEqual(TestResultBag.MethodThatThrowsException, TestConstants.MethodThatThrowsException);
                Assert.AreEqual(ex, TestConstants.MethodThatThrowsException);
            }
        }
    }
}