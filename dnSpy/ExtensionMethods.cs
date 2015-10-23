﻿// Copyright (c) 2011 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using dnlib.DotNet;
using dnSpy.NRefactory;
using ICSharpCode.Decompiler;
using ICSharpCode.ILSpy.Options;

namespace ICSharpCode.ILSpy {
	/// <summary>
	/// ExtensionMethods used in ILSpy.
	/// </summary>
	public static class ExtensionMethods {
		public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items) {
			foreach (T item in items)
				list.Add(item);
		}

		public static bool IsCustomAttribute(this TypeDef type) {
			while (type.FullName != "System.Object") {
				var resolvedBaseType = type.BaseType.ResolveTypeDef();
				if (resolvedBaseType == null)
					return false;
				if (resolvedBaseType.FullName == "System.Attribute")
					return true;
				type = resolvedBaseType;
			}
			return false;
		}

		public static void WriteSuffixString(this MDToken token, ITextOutput output) {
			if (!DisplaySettingsPanel.CurrentDisplaySettings.ShowMetadataTokens)
				return;

			output.WriteSpace();
			output.Write('@', TextTokenType.Operator);
			output.Write(string.Format("{0:X8}", token.ToUInt32()), TextTokenType.Number);
		}
	}
}
