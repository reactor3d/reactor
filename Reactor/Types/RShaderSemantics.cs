//
// RShaderSemantics.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Reactor.Types
{
    internal class RShaderSemantics : Dictionary<RShaderSemanticDefinition, RShaderSemantic>
    {
        internal RShaderSemantics(ref string source)
        {
            
            //TODO: use-  @"uniform\s(?<type>.\w*)\s(?<name>.\w*)\s[:]\s(?<macro>.*);"
            source = Regex.Replace(source, @"uniform\s(?<type>.\w*)\s(?<name>.\w*)\s[:]\s(?<macro>.\w*);", delegate(Match match)
            {

                RShaderSemantic semantic = new RShaderSemantic()
                {
                    type = match.Groups["type"].Value.ToLower(),
                    name = match.Groups["name"].Value.ToLower()
                };
                Add(GetSemanticDefinition(match.Groups["macro"].Value.ToUpper()), semantic);
                var returnValue = String.Format("uniform {0} {1};", match.Groups["type"].Value, match.Groups["name"].Value);
                return returnValue;
            });
        }

        RShaderSemanticDefinition GetSemanticDefinition(string semantic)
        {
            return (RShaderSemanticDefinition)Enum.Parse(typeof(RShaderSemanticDefinition), semantic, true);
        }
    }

    internal struct RShaderSemantic
    {
        public string name;
        public string type;
    }

    public enum RShaderSemanticDefinition
    {
        WORLD,
        MODEL,
        VIEW,
        PROJECTION,
        INVERSE_WORLD,
        INVERSE_MODEL,
        INVERSE_VIEW,
        INVERSE_PROJECTION,
        MODELVIEW,
        WORLDVIEW,
        MODELVIEWPROJECTION,
        WORLDVIEWPROJECTION,
        VIEWPROJECTION
    }
}

