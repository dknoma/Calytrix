using System;
using System.Collections.Generic;
using System.Text;
using Utility.Codegen;

public class MethodBlock {
	internal readonly IList<CodeBuilderUtil.Modifier> modifiers;
	internal readonly IList<string> parameters;
	internal readonly Type returnType;
	internal readonly string name;
	internal readonly CodeBlock codeBlock;

	private MethodBlock(Builder builder) {
		this.modifiers = builder.modifiers; 
		this.parameters = builder.parameters; 
		this.name = builder.name; 
		this.codeBlock = builder.codeBlock; 
	}

	public static Builder NewBuilder(string name) {
		return new Builder(name);
	}

	public override string ToString() {
		StringBuilder builder = new StringBuilder();
		foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
			builder.Append($"{modifier.ToLowerCase()} ");
		}

		builder.Append($"{(returnType == null ? "void" : returnType.FormattedString())} {name}({string.Join(", ", parameters)}) {{\n");

		builder.Append(IndentCodeBlock(codeBlock));
//		builder.Append($"{codeBlock.ToString().Replace("\n", "\n")}");
//		builder.Append($"    {codeBlock.ToString().Replace("\n", "\n    ")}");
		builder.Append("}");

		return builder.ToString();
	}

	private string IndentCodeBlock(CodeBlock block) {
		string res = $"    {block.ToString().Replace("\n", "\n    ")}";
		return res.Substring(0, res.Length - 4);
	}

	public class Builder {
		internal readonly IList<CodeBuilderUtil.Modifier> modifiers;
		internal readonly IList<string> parameters;
		internal readonly string name;
		
		internal Type returnType;
		internal CodeBlock codeBlock;
		
		internal Builder(string name) {
			this.modifiers = new List<CodeBuilderUtil.Modifier>();
			this.parameters = new List<string>();
			this.name = name;
		}

		public Builder AddModifiers(params CodeBuilderUtil.Modifier[] modifiers) {
			foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
				this.modifiers.Add(modifier);
			}

			return this;
		}

		public Builder Parameters(params string[] parameters) {
			foreach(string parameter in parameters) {
				this.parameters.Add(parameter);
			}

			return this;
		}

		public Builder ReturnType(Type returnType) {
			this.returnType = returnType;
			return this;
		}

		public Builder AddCodeBlock(CodeBlock codeBlock) {
			this.codeBlock = codeBlock;
			return this;
		}

		public MethodBlock Build() {
			return new MethodBlock(this);
		}
	}
}
