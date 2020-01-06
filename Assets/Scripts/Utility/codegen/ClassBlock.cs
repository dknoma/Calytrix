using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public struct ClassBlock {
	private readonly IList<CodeBuilderUtil.Modifier> modifiers;
	private readonly IList<Field> fields;
	private readonly CodeBuilderUtil.ClassType type;
	private readonly string name;

	private ClassBlock(CodeBuilderUtil.ClassType type, string name, IList<CodeBuilderUtil.Modifier> modifiers) {
		this.modifiers = modifiers;
		this.fields = new List<Field>();
		this.type = type;
		this.name = name;
	}

	public Builder ClassBuilder(string className) {
		return new Builder(className);
	}

	public override string ToString() {
		StringBuilder builder = new StringBuilder();
		foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
			builder.Append($"{modifier.ToLowerCase()} ");
		}

		builder.Append($"{type.ToLowerCase()} {name} {{\n");

		return builder.ToString();
	}

	public class Builder {
		private readonly IList<CodeBuilderUtil.Modifier> modifiers;
		private readonly IList<Field> fields;
		private readonly string className;

		private CodeBuilderUtil.ClassType type;

		internal Builder(string className) {
			this.modifiers = new List<CodeBuilderUtil.Modifier>();
			;
			this.className = className;
		}

		public Builder AddModifiers(CodeBuilderUtil.Modifier modifier, params CodeBuilderUtil.Modifier[] optionals) {
			this.modifiers.Add(modifier);
			foreach(CodeBuilderUtil.Modifier optional in optionals) {
				this.modifiers.Add(optional);
			}

			return this;
		}

		public Builder AddType(CodeBuilderUtil.ClassType type) {
			this.type = type;
			return this;
		}

//				public Builder AddClassHeader(ClassType type, string name, params Modifier[] modifiers) {
//					StringBuilder builder = new StringBuilder();
//					foreach(Modifier modifier in modifiers) {
//						builder.Append($"{modifier.ToLowerCase()} ");
//					}
//	
//					builder.Append($"{type.ToLowerCase()} {name} {{\n");
//
//					Add(builder.ToString());
//				
//					return this;
//				}

		public ClassBlock Build() {
			return new ClassBlock(type, className, modifiers);
		}
	}

	private struct Field {
		private readonly CodeBuilderUtil.Modifier[] modifiers;
		private readonly Type returnType;
		private readonly string name;

		public Field(Type returnType, string name, params CodeBuilderUtil.Modifier[] modifiers) {
			this.returnType = returnType;
			this.name = name;
			this.modifiers = modifiers;
		}

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.Append("\t");
			foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
				builder.Append($"{modifier.ToLowerCase()} ");
			}

			builder.Append($"{returnType.FormattedString()} {name};\n");

			return builder.ToString();
		}
	}

	private class Constructor {
		private readonly CodeBuilderUtil.Modifier[] modifiers;
		private readonly string name;
		private readonly IList<Parameter> parameters = new List<Parameter>();
		private readonly IList<string> statements = new List<string>();

		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			builder.Append("\t");
			foreach(CodeBuilderUtil.Modifier modifier in modifiers) {
				builder.Append($"{modifier.ToLowerCase()} ");
			}

			builder.Append($"{name}(");

			foreach(Parameter parameter in parameters) {
				builder.Append($"{parameter}, ");
			}

			builder.Remove(builder.ToString().Length - 1, 1);
			builder.Append(") {\n");

			foreach(string statement in statements) {
				builder.Append(statement);
			}

			builder.Append("}\n");

			return builder.ToString();
		}

		private struct Parameter {
			private Type type;
			private string name;

			public override string ToString() {
				return $"{type.FormattedString()} {name}";
			}
		}
	}
}
