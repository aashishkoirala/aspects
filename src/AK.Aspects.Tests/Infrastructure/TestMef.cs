/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.TestMef
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

using System.ComponentModel.Composition;

#endregion

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Export wrapper for MEF tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [Export]
    public class TestMef
    {
        private readonly ITestContract unwrapped;
        private readonly ITestContract wrapped;

        [ImportingConstructor]
        public TestMef(
            [Import] ITestContract unwrapped,
            [Import(typeof (IAspected<ITestContract>))] ITestContract wrapped)
        {
            this.unwrapped = unwrapped;
            this.wrapped = wrapped;
        }

        public void ExecuteUnwrapped()
        {
            this.unwrapped.VoidMethodWithNoParameters();
        }

        public void ExecuteWrapped()
        {
            this.wrapped.VoidMethodWithNoParameters();
        }
    }
}