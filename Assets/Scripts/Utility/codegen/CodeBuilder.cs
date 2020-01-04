using System;
using System.Text;
using static CodeBuilderUtil;

namespace Utility.Codegen {
	public class CodeBuilder {
		private ClassHeader classHeader;
		private Field[] fields;
		private Constructor[] constructors;

		private struct ClassHeader {
			private readonly Modifier[] modifiers;
			private readonly ClassType type;
			private readonly string name;

			public ClassHeader(ClassType type, string name, params Modifier[] modifiers) {
				this.type = type;
				this.name = name;
				this.modifiers = modifiers;
			}

			public override string ToString() {
				StringBuilder builder = new StringBuilder();
				foreach(Modifier modifier in modifiers) {
					builder.Append($"{modifier.ToLowerCase()} ");
				}

				builder.Append($"{type.ToLowerCase()} {name} {{\n");

				return builder.ToString();
			}
		}

		private struct Field {
			private readonly Modifier[] modifiers;
			private readonly Type returnType;
			private readonly string name;

			public Field(Type returnType, string name, params Modifier[] modifiers) {
				this.returnType = returnType;
				this.name = name;
				this.modifiers = modifiers;
			}
			
			public override string ToString() {
				StringBuilder builder = new StringBuilder();
				builder.Append("\t");
				foreach(Modifier modifier in modifiers) {
					builder.Append($"{modifier.ToLowerCase()} ");
				}

				builder.Append($"{returnType.Name} {name};\n");

				return builder.ToString();
			}
		}

		private struct Constructor {
			private readonly Modifier[] modifiers;
			private readonly string name;
			private readonly Parameter[] parameters;
			
			public override string ToString() {
				StringBuilder builder = new StringBuilder();
				builder.Append("\t");
				foreach(Modifier modifier in modifiers) {
					builder.Append($"{modifier.ToLowerCase()} ");
				}

				builder.Append($"{name}(");
				
				foreach(Parameter parameter in parameters) {
					builder.Append($"{parameter}, ");
				}

				builder.Remove(builder.ToString().Length - 1, 1);
				builder.Append(") {\n");
				
				
				
				builder.Append("}\n");

				return builder.ToString();
			}

			private struct Parameter {
				private Type type;
				private string name;

				public override string ToString() {
					return $"{type.Name} {name}";
				}
			}
		}
	}
}