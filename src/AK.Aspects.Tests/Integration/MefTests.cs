/*******************************************************************************************************************************
 * AK.Aspects.Tests.Integration.MefTests
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
using System.ComponentModel.Composition.Hosting;

#endregion

namespace AK.Aspects.Tests.Integration
{
    /// <summary>
    /// MEF related integration tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [TestClass]
    public class MefTests
    {
        private CompositionContainer container;
        private AggregateCatalog catalog;
        private TestMef target;

        [TestInitialize]
        public void Initialize()
        {
            var thisAssembly = this.GetType().Assembly;
            var targetAssembly = typeof (AspectHelper).Assembly;

            this.catalog = new AggregateCatalog(new AssemblyCatalog(thisAssembly), new AssemblyCatalog(targetAssembly));
            this.container = new CompositionContainer(this.catalog);

            AspectHelper.RegisterForComposition(this.container, thisAssembly);

            this.target = this.container.GetExportedValue<TestMef>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.container.Dispose();
            this.catalog.Dispose();
            this.target = null;
        }

        [TestMethod, TestCategory("Integration")]
        public void Mef_Integration_Works()
        {
            TestResultBag.Nullify();

            this.target.ExecuteUnwrapped();

            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersCalled);
            Assert.IsFalse(TestResultBag.VoidMethodWithNoParametersEntryCalled);
            Assert.IsFalse(TestResultBag.VoidMethodWithNoParametersExitCalled);

            TestResultBag.Nullify();

            this.target.ExecuteWrapped();

            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersEntryCalled);
            Assert.IsTrue(TestResultBag.VoidMethodWithNoParametersExitCalled);
        }

        [TestMethod, TestCategory("Integration")]
        public void Mef_Creation_Policy_Shared_Works()
        {
            // ReSharper disable SuspiciousTypeConversion.Global

            var shared1 =
                (ICreationPolicySharedTestContract)
                this.container.GetExportedValue<IAspected<ICreationPolicySharedTestContract>>();

            const string value1 = "Some value";
            shared1.Value = value1;

            var shared2 =
                (ICreationPolicySharedTestContract)
                this.container.GetExportedValue<IAspected<ICreationPolicySharedTestContract>>();

            var value2 = shared2.Value;

            Assert.AreEqual(value1, value2);

            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod, TestCategory("Integration")]
        public void Mef_Creation_Policy_Non_Shared_Works()
        {
            // ReSharper disable SuspiciousTypeConversion.Global

            var shared1 =
                (ICreationPolicyNonSharedTestContract)
                this.container.GetExportedValue<IAspected<ICreationPolicyNonSharedTestContract>>();

            shared1.Value = "Some value";

            var shared2 =
                (ICreationPolicyNonSharedTestContract)
                this.container.GetExportedValue<IAspected<ICreationPolicyNonSharedTestContract>>();

            Assert.IsNull(shared2.Value);

            // ReSharper restore SuspiciousTypeConversion.Global
        }
    }
}