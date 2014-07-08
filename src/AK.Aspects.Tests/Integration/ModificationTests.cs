/*******************************************************************************************************************************
 * AK.Aspects.Tests.Integration.ModificationTests
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
    /// Return value/exception-modification related integration tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [TestClass]
    public class ModificationTests
    {
        private IModifierContract target;

        [TestInitialize]
        public void Initialize()
        {
            AspectHelper.CodeGenerated += (sender, args) => Console.WriteLine(args.GeneratedCode);
            this.target = AspectHelper.Wrap<IModifierContract>(new ModifierImplementation());
        }

        [TestMethod, TestCategory("Integration")]
        public void Entry_Modification_Of_Return_Value_Works()
        {
            var value = this.target.DoThing(false, false, false);
            Assert.AreEqual(value, 40);

            value = this.target.DoThing(true, false, false);
            Assert.AreEqual(value, 23);
        }

        [TestMethod, TestCategory("Integration")]
        public void Exit_Modification_Of_Return_Value_Works()
        {
            var value = this.target.DoThing(false, false, false);
            Assert.AreEqual(value, 40);

            value = this.target.DoThing(false, true, false);
            Assert.AreEqual(value, 46);
        }

        [TestMethod, TestCategory("Integration")]
        public void Error_Modification_Of_Return_Value_Works()
        {
            var value = this.target.DoThing(false, false, false);
            Assert.AreEqual(value, 40);

            value = this.target.DoThing(false, false, true);
            Assert.AreEqual(value, -1);
        }

        [TestMethod, TestCategory("Integration")]
        public void Error_Modification_Of_Exception_Works()
        {
            try
            {
                this.target.DoError();
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(ex.Message, "Wrapped!");
                Assert.AreEqual(ex.InnerException.Message, "Inner.");
            }
        }
    }
}