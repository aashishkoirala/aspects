/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.ModifierImplementation
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

using System;

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Implementation for modifier tests.
    /// </summary>
    /// <author>Aashish Koirala</author>
    [Modifier]
    public class ModifierImplementation : IModifierContract
    {
        public int DoThing(bool returnPredefinedNumber1, bool returnPredefinedNumber2, bool throwException)
        {
            if (throwException) throw new ApplicationException("Inner.");

            return returnPredefinedNumber1 ? 20 : 40;
        }

        public void DoError()
        {
            throw new ApplicationException("Inner.");
        }
    }
}