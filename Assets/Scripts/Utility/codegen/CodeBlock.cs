using System;
using System.Collections.Generic;
using System.Text;
using static CodeBuilderUtil;

namespace Utility.Codegen {
	public class CodeBlock {
		private readonly IList<string> formatParts;
		private readonly IList<object> args;

		private CodeBlock() {
			this.formatParts = new List<string>();
			this.args = new List<object>();
		}
		
		private CodeBlock(IList<string> formatParts, IList<object> args) {
			this.formatParts = formatParts;
			this.args = args;
		}

		public static Builder NewBuilder() {
			return new Builder();
		}
			
		public override string ToString() {
			StringBuilder builder = new StringBuilder();

			int j = 0;
			foreach(string part in formatParts) {
				builder.Append(part);

				if(part[0] == '$' && part.Length > 1) {
					char type = part[1];
					switch(type) {
						case 'A':
							builder.Append(args[j++]);
							break;
						default:
							break;
					}
				}
			}

//			builder.Append("}");
			
			return builder.ToString();
		}

		public class Builder {
			private readonly IList<string> formatParts;
			private readonly IList<object> args;

			private int indentLevel = 0;
			
			internal Builder() {
				this.formatParts = new List<string>();
				this.args = new List<object>();
			}

			public Builder AddClassHeader(ClassType type, string name, params Modifier[] modifiers) {
				StringBuilder builder = new StringBuilder();
				foreach(Modifier modifier in modifiers) {
					builder.Append($"{modifier.ToLowerCase()} ");
				}
	
				builder.Append($"{type.ToLowerCase()} {name} {{\n");

				Add(builder.ToString());
				
				return this;
			}
			
			public string Indent(int indentLevel) {
				return "".PadRight(indentLevel * 4);
			}

			private string Indent() {
				return "".PadRight(indentLevel * 4);
			}
		
			public Builder AddStatement(string format, params object[] args) {
				this.Add($"{Indent()}{format}\n", args);
				return this;
			}

			public Builder BeginControlFlow(string format, params object[] args) {
				this.Add($"{Indent()}{format} {{\n", args);
				indentLevel++;
				return this;
			}

			public Builder EndControlFlow() { 
				indentLevel--;
				if(indentLevel < 0) {
					indentLevel = 0;
				}
				this.Add($"{Indent()}}}\n");
				return this;
			}

			public Builder EndControlFlow(string format, params object[] args) {
				indentLevel--;
				if(indentLevel < 0) {
					indentLevel = 0;
				}
				this.Add($"{Indent()}}}{format};\n", args);
				return this;
			}

			public CodeBlock Build() {
				return new CodeBlock(this.formatParts, this.args);
			}

			private Builder Add(string format, params object[] args) {
				int len = format.Length;
				int progress = 0;
				int indexStart = 0;
				int argsCount = 0;
				while(progress < len) {
					if(format[progress] != '$') {
						// Get the index 
						indexStart = format.IndexOf('$', progress + 1);
						// if index not found, default to end of string.
						if (indexStart == -1) {
							indexStart = len;
						}

						// take substring up until the first $ formatter 
						this.formatParts.Add(format.Substring(progress, indexStart));
						progress = indexStart;
					} else {
						int index = progress + 1;
						if (index < len) {
							if(ValidArgFormatter(format[index])){
								this.args.Add(args[argsCount++]);
							}
						}
						
						index = format.IndexOf('$', progress + 1);
						// if index not found, default to end of string.
						if (index == -1) {
							index = len;
						}

						// take substring up until the first $ formatter 
						this.formatParts.Add(format.Substring(progress, index));
						progress = index;
					}
				}

				if(argsCount != args.Length) {
					throw new FormatException("Number of valid argument formats doesn't match the number of valid arguments.");
				}
				
				return this;
			}

			private static bool ValidArgFormatter(char c) {
				return c == 'A';
			}
		}
	}
}