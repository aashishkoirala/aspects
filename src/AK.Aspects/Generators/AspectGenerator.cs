/*******************************************************************************************************************************
 * AK.Aspects.Generators.AspectGenerator
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

using System.CodeDom;
using System.Reflection;
using System.Text;

#endregion

namespace AK.Aspects.Generators
{
    /// <summary>
    /// Provides access to an common functionality for aspect code generators.
    /// </summary>
    /// <author>Aashish Koirala</author>
    internal class AspectGenerator
    {
        protected readonly MemberInfo MemberInfo;

        private EntryAspectGenerator entry;
        private ExitAspectGenerator exit;
        private ErrorAspectGenerator error;

        public AspectGenerator(MemberInfo memberInfo)
        {
            this.MemberInfo = memberInfo;
        }

        public EntryAspectGenerator Entry
        {
            get { return this.entry ?? (this.entry = new EntryAspectGenerator(this.MemberInfo)); }
        }

        public ExitAspectGenerator Exit
        {
            get { return this.exit ?? (this.exit = new ExitAspectGenerator(this.MemberInfo)); }
        }

        public ErrorAspectGenerator Error
        {
            get { return this.error ?? (this.error = new ErrorAspectGenerator(this.MemberInfo)); }
        }

        protected CodeExpression GenerateParameterDictionaryExpression(bool? isPropertyGet = false)
        {
            var parameterDictionarySnippet = "new System.Collections.Generic.Dictionary<string, object> {{ {0} }}";

            var sb = new StringBuilder();

            if (this.MemberInfo is MethodInfo)
            {
                foreach (var parameterInfo in ((MethodInfo) this.MemberInfo).GetParameters())
                    sb.AppendFormat("{{ \"{0}\", {0} }}, ", parameterInfo.Name);

                var index = 1;
                foreach (var type in ((MethodInfo) this.MemberInfo).GetGenericArguments())
                {
                    sb.AppendFormat("{{ \"T{0}\", typeof({1}) }}, ", index, type.Name);
                    index++;
                }
            }
            else if (this.MemberInfo is PropertyInfo)
            {
                foreach (var parameterInfo in ((PropertyInfo) this.MemberInfo).GetIndexParameters())
                    sb.AppendFormat("{{ \"{0}\", {0} }}, ", parameterInfo.Name);

                if (!(isPropertyGet ?? false)) sb.AppendFormat("{{ \"{0}\", {0} }}", "value");
            }

            parameterDictionarySnippet = string.Format(parameterDictionarySnippet, sb);

            return new CodeSnippetExpression(parameterDictionarySnippet);
        }
    }
}