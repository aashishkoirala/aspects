/*******************************************************************************************************************************
 * AK.Aspects.IEntryAspect
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
using System.Reflection;

#endregion

namespace AK.Aspects
{
    /// <summary>
    /// Interface that an attribute must implement to be an entry aspect.
    /// An entry aspect is executed as soon as the operation is entered into.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public interface IEntryAspect : IAspect
    {
        /// <summary>
        /// Executes the entry aspect.
        /// </summary>
        /// <param name="memberInfo">
        /// MemberInfo representing the current method or property. In the case of a property,
        /// this will represent the inner get/set method.
        /// </param>
        /// <param name="parameters">
        /// Dictionary of parameter values passed to this operation, keyed by parameter names.
        /// </param>
        /// <returns>
        /// Return TRUE to continue execution. Return FALSE to stop execution of the
        /// method/property and return the default value.
        /// </returns>
        bool Execute(MemberInfo memberInfo, IDictionary<string, object> parameters);
    }
}