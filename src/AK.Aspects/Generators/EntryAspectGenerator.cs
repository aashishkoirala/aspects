/*******************************************************************************************************************************
 * AK.Aspects.Generators.EntryAspectGenerator
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
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace AK.Aspects.Generators
{
    /// <summary>
    /// Generates entry aspect code.
    /// </summary>
    /// <author>Aashish Koirala</author>
    internal class EntryAspectGenerator : AspectGenerator
    {
        public EntryAspectGenerator(MemberInfo memberInfo) : base(memberInfo) {}

        public IEnumerable<CodeStatement> GenerateForMethod()
        {
            var methodInfo = this.MemberInfo as MethodInfo;
            if (methodInfo == null) throw new InvalidOperationException();

            yield return GenerateMethodStartStatement();

            var returnsValue = methodInfo.ReturnType != typeof (void);
            var aspectInvocationExpression = this.GenerateAspectInvocationForMethod(returnsValue);

            var returnValueStatement = returnsValue
                                           ? Constructs.ReturnValueStatement
                                           : new CodeMethodReturnStatement();

            yield return new CodeConditionStatement(aspectInvocationExpression,
                                                    new CodeStatement[0],
                                                    returnValueStatement.AsArray());
        }

        public IEnumerable<CodeStatement> GenerateForPropertyGet()
        {
            yield return GenerateMethodStartStatement();

            var aspectInvocationExpression = this.GenerateAspectInvocationForProperty(true);

            var returnValueStatement = Constructs.ReturnValueStatement;

            yield return new CodeConditionStatement(aspectInvocationExpression,
                                                    new CodeStatement[0],
                                                    returnValueStatement.AsArray());
        }

        public IEnumerable<CodeStatement> GenerateForPropertySet()
        {
            yield return GenerateMethodStartStatement();

            var aspectInvocationExpression = this.GenerateAspectInvocationForProperty(false);

            var returnValueStatement = (CodeStatement) new CodeMethodReturnStatement();

            yield return new CodeConditionStatement(aspectInvocationExpression,
                                                    new CodeStatement[0],
                                                    returnValueStatement.AsArray());
        }

        private static CodeStatement GenerateMethodStartStatement()
        {
            var initialValueExpression = new CodePropertyReferenceExpression(
                new CodeTypeReferenceExpression(typeof (DateTime)), "Now");

            return new CodeVariableDeclarationStatement(typeof (DateTime),
                                                        VariableNames.MethodStart,
                                                        initialValueExpression);
        }

        private CodeExpression GenerateAspectInvocationForMethod(bool returnsValue)
        {
            var parameterDictionaryExpression = this.GenerateParameterDictionaryExpression(returnsValue);
            var aspectExecutorExpression = new CodeTypeReferenceExpression(typeof (AspectExecutor));
            var getCurrentMethodExpression = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof (MethodBase)), "GetCurrentMethod");

            return new CodeMethodInvokeExpression(aspectExecutorExpression,
                                                  "ExecuteEntryAspects",
                                                  Constructs.TargetFieldExpression,
                                                  getCurrentMethodExpression,
                                                  parameterDictionaryExpression);
        }

        private CodeExpression GenerateAspectInvocationForProperty(bool isGet)
        {
            var parameterDictionaryExpression = this.GenerateParameterDictionaryExpression(isGet);
            var aspectExecutorExpression = new CodeTypeReferenceExpression(typeof (AspectExecutor));
            var getCurrentMethodExpression = new CodeSnippetExpression(
                string.Format("{0}.GetType().GetProperty(\"{1}\")", VariableNames.Target, this.MemberInfo.Name));

            return new CodeMethodInvokeExpression(aspectExecutorExpression,
                                                  "ExecuteEntryAspects",
                                                  Constructs.TargetFieldExpression,
                                                  getCurrentMethodExpression,
                                                  parameterDictionaryExpression);
        }
    }
}