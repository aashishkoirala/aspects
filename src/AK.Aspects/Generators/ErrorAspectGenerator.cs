/*******************************************************************************************************************************
 * AK.Aspects.Generators.ErrorAspectGenerator
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
using System.Reflection;

#endregion

namespace AK.Aspects.Generators
{
    /// <summary>
    /// Generates error aspect code.
    /// </summary>
    /// <author>Aashish Koirala</author>
    internal class ErrorAspectGenerator : AspectGenerator
    {
        public ErrorAspectGenerator(MemberInfo memberInfo) : base(memberInfo) {}

        public CodeStatement GenerateForMethod()
        {
            var methodInfo = this.MemberInfo as MethodInfo;
            if (methodInfo == null) throw new InvalidOperationException();

            var returnsValue = methodInfo.ReturnType != typeof (void);

            var aspectInvocationExpression = this.GenerateAspectInvocationForMethod(returnsValue);
            var throwStatement = (CodeStatement) new CodeThrowExceptionStatement();

            return new CodeConditionStatement(
                aspectInvocationExpression, throwStatement.AsArray());
        }

        public CodeStatement GenerateForPropertyGet()
        {
            var aspectInvocationExpression = this.GenerateAspectInvocationForProperty(true);
            var throwStatement = (CodeStatement) new CodeThrowExceptionStatement();

            return new CodeConditionStatement(
                aspectInvocationExpression, throwStatement.AsArray());
        }

        public CodeStatement GenerateForPropertySet()
        {
            var aspectInvocationExpression = this.GenerateAspectInvocationForProperty(false);
            var throwStatement = (CodeStatement) new CodeThrowExceptionStatement();

            return new CodeConditionStatement(
                aspectInvocationExpression, throwStatement.AsArray());
        }

        private CodeExpression GenerateAspectInvocationForMethod(bool returnsValue)
        {
            var parameterDictionaryExpression = this.GenerateParameterDictionaryExpression(returnsValue);
            var aspectExecutorExpression = new CodeTypeReferenceExpression(typeof (AspectExecutor));
            var getCurrentMethodExpression = new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(typeof (MethodBase)), "GetCurrentMethod");
            var exceptionParameterExpression = new CodeVariableReferenceExpression(VariableNames.Exception);

            return new CodeMethodInvokeExpression(
                aspectExecutorExpression,
                "ExecuteErrorAspects",
                Constructs.TargetFieldExpression,
                getCurrentMethodExpression,
                parameterDictionaryExpression,
                exceptionParameterExpression);
        }

        private CodeExpression GenerateAspectInvocationForProperty(bool isGet)
        {
            var parameterDictionaryExpression = this.GenerateParameterDictionaryExpression(isGet);
            var aspectExecutorExpression = new CodeTypeReferenceExpression(typeof (AspectExecutor));
            var getCurrentMethodExpression = new CodeSnippetExpression(
                string.Format("{0}.GetType().GetProperty(\"{1}\")", VariableNames.Target, this.MemberInfo.Name));
            var exceptionParameterExpression = new CodeVariableReferenceExpression(VariableNames.Exception);

            return new CodeMethodInvokeExpression(
                aspectExecutorExpression,
                "ExecuteErrorAspects",
                Constructs.TargetFieldExpression,
                getCurrentMethodExpression,
                parameterDictionaryExpression,
                exceptionParameterExpression);
        }
    }
}