using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Codegen;

public class MethodBlock {

	public class Builder {
		internal readonly IList<CodeBuilderUtil.Modifier> modifiers;
		internal readonly string name;
		
		internal CodeBlock codeBlock;
		
		internal Builder(string name) {
			this.modifiers = new List<CodeBuilderUtil.Modifier>();
			this.name = name;
		}

		public Builder CodeBlock(CodeBlock codeBlock) {
			this.codeBlock = codeBlock;
			return this;
		}
	}
}
