using System;
using System.Collections.Generic;
using System.Text;

public class FieldBlock {
	private readonly IList<CodeBuilderUtil.Modifier> modifiers;
	private readonly Type type;
	private readonly string name;

	private FieldBlock(Builder builder) {
		this.type = builder.type;
		this.name = builder.name;
		this.modifiers = builder.modifiers;
	}

	public static Builder NewBuilder(string name) {
		return new Builder(name);
	}

	public override string ToString() {
		StringBuilder builder = new StringBuilder();
		foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
			builder.Append($"{modifier.ToLowerCase()} ");
		}

		builder.Append($"{type.FormattedString()} {name};\n");

		return builder.ToString();
	}

	public class Builder {
		internal readonly IList<CodeBuilderUtil.Modifier> modifiers;
		internal readonly string name;

		internal Type type;
		
		internal Builder(string name) {
			this.modifiers = new List<CodeBuilderUtil.Modifier>();
			this.name = name;
		}

		public Builder AddModifiers(params CodeBuilderUtil.Modifier[] modifiers) {
			foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
				this.modifiers.Add(modifier);
			}

			return this;
		}

		public Builder AddType(Type type) {
			this.type = type;
			return this;
		}

		public FieldBlock Build() {
			return new FieldBlock(this);
		}
	}
}
