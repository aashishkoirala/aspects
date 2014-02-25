/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.ITestContract
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

#endregion

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Contract for wrapping tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public interface ITestContract
    {
        string NormalProperty { get; set; }
        int ReadOnlyProperty { get; }
        decimal WriteOnlyProperty { set; }
        string this[string indexParameter] { get; set; }

        void VoidMethodWithNoParameters();
        void VoidMethodWithParameters(int parameter1, string parameter2);
        string ReturnMethodWithNoParameters();
        int ReturnMethodWithParameters(int parameter1, string parameter2);
        void MethodWithRefAndOutParameters(out string outParameter1, ref IList<int> refParameter2);
        T GenericMethod<T>(T parameter1, string parameter2);
        string MethodThatThrows();
    }
}