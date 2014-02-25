/*******************************************************************************************************************************
 * AK.Aspects.AspectExecutor
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
using System.Linq;
using System.Reflection;

#endregion

namespace AK.Aspects
{
    /// <summary>
    /// This is called by the generated aspect code to execute the applied aspects. This is not meant to be
    /// called directly. 
    /// </summary>
    /// <author>Aashish Koirala</author>
    public static class AspectExecutor
    {
        /// <summary>
        /// Executes entry aspects.
        /// </summary>
        /// <param name="target">Inner target object.</param>
        /// <param name="memberInfo">MemberInfo representing current method.</param>
        /// <param name="parameters">Parameter values keyed by name.</param>
        /// <returns>Continue rest of the method/property operation?</returns>
        public static bool ExecuteEntryAspects(
            object target,
            MemberInfo memberInfo,
            IDictionary<string, object> parameters)
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            // ReSharper disable LoopCanBeConvertedToQuery
            // ReSharper disable ConditionIsAlwaysTrueOrFalse

            var classAspects = Enumerable.Empty<IEntryAspect>();
            if (target != null)
            {
                classAspects = target.GetType().GetCustomAttributes()
                    .Where(x => x as IEntryAspect != null)
                    .Cast<IEntryAspect>();
            }

            var memberAspects = memberInfo.GetCustomAttributes()
                .Where(x => x as IEntryAspect != null)
                .Cast<IEntryAspect>();

            var aspects = classAspects.Union(memberAspects).OrderBy(x => x.Order);


            foreach (var aspect in aspects)
            {
                var continueOperation = aspect.Execute(memberInfo, parameters);
                if (!continueOperation) return false;
            }

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            // ReSharper restore SuspiciousTypeConversion.Global
            // ReSharper restore LoopCanBeConvertedToQuery

            return true;
        }

        /// <summary>
        /// Executes exit aspects.
        /// </summary>
        /// <param name="target">Inner target object.</param>
        /// <param name="memberInfo">MemberInfo representing current method.</param>
        /// <param name="parameters">Parameter values keyed by name.</param>
        /// <param name="returnValue">Return value (NULL for void methods).</param>
        /// <param name="duration">Time taken to execute the operation.</param>
        public static void ExecuteExitAspects(
            object target,
            MemberInfo memberInfo,
            IDictionary<string, object> parameters,
            object returnValue,
            TimeSpan duration)
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            // ReSharper disable ConditionIsAlwaysTrueOrFalse

            var classAspects = Enumerable.Empty<IExitAspect>();
            if (target != null)
            {
                classAspects = target.GetType().GetCustomAttributes()
                    .Where(x => x as IExitAspect != null)
                    .Cast<IExitAspect>();
            }

            var memberAspects = memberInfo.GetCustomAttributes()
                .Where(x => x as IExitAspect != null)
                .Cast<IExitAspect>();

            var aspects = classAspects.Union(memberAspects).OrderBy(x => x.Order);

            foreach (var aspect in aspects)
                aspect.Execute(memberInfo, parameters, returnValue, duration);

            // ReSharper restore SuspiciousTypeConversion.Global
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        /// <summary>
        /// Executes error aspects.
        /// </summary>
        /// <param name="target">Inner target object.</param>
        /// <param name="memberInfo">MemberInfo representing current method.</param>
        /// <param name="parameters">Parameter values keyed by name.</param>
        /// <param name="ex">Exception that was caught.</param>
        /// <returns>Whether to rethrow the exception.</returns>
        public static bool ExecuteErrorAspects(
            object target,
            MemberInfo memberInfo,
            IDictionary<string, object> parameters,
            Exception ex)
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            // ReSharper disable ConditionIsAlwaysTrueOrFalse

            var classAspects = Enumerable.Empty<IErrorAspect>();
            if (target != null)
            {
                classAspects = target.GetType().GetCustomAttributes()
                    .Where(x => x as IErrorAspect != null)
                    .Cast<IErrorAspect>();
            }

            var memberAspects = memberInfo.GetCustomAttributes()
                .Where(x => x as IErrorAspect != null)
                .Cast<IErrorAspect>();

            var aspects = classAspects.Union(memberAspects).OrderBy(x => x.Order);

            var rethrow = true;
            foreach (var aspect in aspects)
                rethrow = aspect.Execute(memberInfo, parameters, ex);

            return rethrow;

            // ReSharper restore SuspiciousTypeConversion.Global
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }
    }
}