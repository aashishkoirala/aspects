/*******************************************************************************************************************************
 * AK.Aspects.Tests.Infrastructure.ICreationPolicySharedTestContract
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

namespace AK.Aspects.Tests.Infrastructure
{
    /// <summary>
    /// Contract for the shared creation policy test.
    /// </summary>
    /// <author>Aashish Koirala</author>
    public interface ICreationPolicySharedTestContract
    {
        string Value { get; set; }
    }
}