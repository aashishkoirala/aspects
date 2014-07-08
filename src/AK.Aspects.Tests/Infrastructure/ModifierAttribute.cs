/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.ModifierAttribute
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
    /// Aspect for modifier tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public class ModifierAttribute : Attribute, IEntryAspect, IExitAspect, IErrorAspect
    {
        public int Order
        {
            get { return 1; }
        }

        public bool Execute(MemberInfo memberInfo, IDictionary<string, object> parameters, ref object returnValue)
        {
            object parameter;
            if (!parameters.TryGetValue("returnPredefinedNumber1", out parameter)) return true;
            if (parameter.GetType() != typeof (bool)) return true;

            var returnPredefinedNumber = (bool) parameter;
            if (!returnPredefinedNumber) return true;
            if (memberInfo is MethodInfo && (memberInfo as MethodInfo).ReturnType != typeof (int)) return true;

            returnValue = 23;
            return false;
        }

        public void Execute(MemberInfo memberInfo, IDictionary<string, object> parameters, ref object returnValue,
                    TimeSpan duration)
        {
            object parameter;
            if (!parameters.TryGetValue("returnPredefinedNumber2", out parameter)) return;
            if (parameter.GetType() != typeof (bool)) return;

            var returnPredefinedNumber = (bool)parameter;
            if (!returnPredefinedNumber) return;
            if (memberInfo is MethodInfo && (memberInfo as MethodInfo).ReturnType != typeof (int)) return;

            returnValue = 46;
        }

        public bool Execute(MemberInfo memberInfo, IDictionary<string, object> parameters, ref Exception ex,
                            ref object returnValue)
        {
            if (memberInfo is MethodInfo && (memberInfo as MethodInfo).ReturnType == typeof (int))
            {
                returnValue = -1;
                return false;
            }

            ex = new ApplicationException("Wrapped!", ex);
            return true;
        }
    }
}