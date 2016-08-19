// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Utilities
{
    internal static class ReflectionHelpers
    {
        public static bool IsValueType(Type targetType)
        {
            if (targetType == null)
            {
                throw new NullReferenceException("Must supply the targetType parameter");
            }
#if WINRT
			return targetType.GetTypeInfo().IsValueType;
#else
            return targetType.IsValueType;
#endif
        }

        public static Type GetBaseType(Type targetType)
        {
            if (targetType == null)
            {
                throw new NullReferenceException("Must supply the targetType parameter");
            }
#if WINRT
			var type = targetType.GetTypeInfo().BaseType;
#else
            var type = targetType.BaseType;
#endif
            return type;
        }

        /// <summary>
        /// Returns true if the given type represents a class that is not abstract
        /// </summary>
        public static bool IsConcreteClass(Type t)
        {
            if (t == null)
            {
                throw new NullReferenceException("Must supply the t (type) parameter");
            }
#if WINRT
			var ti = t.GetTypeInfo();
			if (ti.IsClass && !ti.IsAbstract)
				return true;
#else
            if (t.IsClass && !t.IsAbstract)
                return true;
#endif
            return false;
        }

        public static MethodInfo GetPropertyGetMethod(PropertyInfo property)
        {
            if (property == null)
            {
                throw new NullReferenceException("Must supply the property parameter");
            }

#if WINRT
            return property.GetMethod;
#else
            return property.GetGetMethod();
#endif
        }

        public static MethodInfo GetPropertySetMethod(PropertyInfo property)
        {
            if (property == null)
            {
                throw new NullReferenceException("Must supply the property parameter");
            }

#if WINRT
            return property.SetMethod;
#else
            return property.GetSetMethod();
#endif
        }

        public static Attribute GetCustomAttribute(MemberInfo member, Type memberType)
        {
            if (member == null)
            {
                throw new NullReferenceException("Must supply the member parameter");
            }
            if (memberType == null)
            {
                throw new NullReferenceException("Must supply the memberType parameter");
            }
#if WINRT
			return member.GetCustomAttribute(memberType);
#else
            return Attribute.GetCustomAttribute(member, memberType);
#endif
        }

        /// <summary>
        /// Returns true if the get and set methods of the given property exist and are public
        /// </summary>
        public static bool PropertyIsPublic(PropertyInfo property)
        {
            if (property == null)
            {
                throw new NullReferenceException("Must supply the property parameter");
            }

            var getMethod = GetPropertyGetMethod(property);
            if (getMethod == null || !getMethod.IsPublic)
                return false;

            var setMethod = GetPropertySetMethod(property);
            if (setMethod == null || !setMethod.IsPublic)
                return false;

            return true;
        }

        /// <summary>
        /// Returns true if the given type can be assigned the given value
        /// </summary>
        public static bool IsAssignableFrom(Type type, object value)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");

            return IsAssignableFromType(type, value.GetType());
        }

        /// <summary>
        /// Returns true if the given type can be assigned a value with the given object type
        /// </summary>
        public static bool IsAssignableFromType(Type type, Type objectType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (objectType == null)
                throw new ArgumentNullException("objectType");
#if WINRT
            if (type.GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo()))
                return true;
#else
            if (type.IsAssignableFrom(objectType))
                return true;
#endif
            return false;
        }

    }
}
