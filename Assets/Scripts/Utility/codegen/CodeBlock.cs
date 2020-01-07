using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using static CodeBuilderUtil;

namespace Utility.Codegen {
	public class CodeBlock {
		internal readonly IList<string> formatParts;
		internal readonly IList<object> args;

		private int indentLevel;
		
		private CodeBlock(Builder builder) {
			this.formatParts = new ReadOnlyCollection<string>(builder.formatParts);
			this.args = new ReadOnlyCollection<object>(builder.args);
		}

		public static Builder NewBuilder() {
			return new Builder();
		}
			
		public override string ToString() {
			StringBuilder builder = new StringBuilder();

			int j = 0;
			foreach(string part in formatParts) {
				if(part[0] == '$' && part.Length > 1) {
					char type = part[1];
					switch(type) {
						case 'A':
							builder.Append(args[j++]);
							break;
						case '>':
							indentLevel++;
							builder.Append(Indentation());
							break;
						case '<':
							indentLevel--;
							builder.Append(Indentation());
							break;
					}
				} else {
					builder.Append(part);
				}
			}
			
			return builder.ToString();
		}

		private string Indentation() {
			return "".PadRight(indentLevel * 4);
		}
		
		public class Builder {
			internal readonly IList<string> formatParts;
			internal readonly IList<object> args;

			private int indentLevel;
			
			internal Builder() {
				this.formatParts = new List<string>();
				this.args = new List<object>();
			}

			private void Indent() {
				this.formatParts.Add("$>");
//				indentLevel++;
			}

			private void Unindent() {
				this.formatParts.Add("$<");
//				indentLevel--;
//				if(indentLevel < 0) {
//					indentLevel = 0;
//				}
			}
		
			public Builder AddStatement(string format, params object[] args) {
				return this.Add($"{format};\n", args);
			}

			public Builder BeginControlFlow(string format, params object[] args) {
				this.Add($"{format} {{\n", args);
				Indent();
				return this;
			}

			public Builder NextControlFlow(string format, params object[] args) {
				Unindent();
				this.Add($"}} {format} {{\n", args);
				Indent();
				return this;
			}

			public Builder EndControlFlow() { 
				Unindent();
				this.Add("}\n");
				return this;
			}

			public Builder EndControlFlow(string format, params object[] args) {
				Unindent();
				return this.Add($"}} {format};\n", args);
			}
		
//			public Builder AddStatement(string format, params object[] args) {
//				return this.Add($"{Indentation()}{format};\n", args);
//			}
//
//			public Builder BeginControlFlow(string format, params object[] args) {
//				this.Add($"{Indentation()}{format} {{\n", args);
//				Indent();
//				return this;
//			}
//
//			public Builder NextControlFlow(string format, params object[] args) {
//				Unindent();
//				this.Add($"{Indentation()}}} {format} {{\n", args);
//				Indent();
//				return this;
//			}
//
//			public Builder EndControlFlow() { 
//				Unindent();
//				this.Add($"{Indentation()}}}\n");
//				return this;
//			}
//
//			public Builder EndControlFlow(string format, params object[] args) {
//				Unindent();
//				return this.Add($"{Indentation()}}} {format};\n", args);
//			}

			public CodeBlock Build() {
				return new CodeBlock(this);
			}

			private Builder Add(string format, params object[] args) {
				int len = format.Length;
				int progress = 0;
				int indexStart;
				int argsCount = 0;
				while(progress < len) {
					char c = format[progress];
					if(c != '$') {
						// Get the index 
						indexStart = format.IndexOf('$', progress + 1) - progress;
						// if index not found, default to end of string.
						if (indexStart < 0) {
							indexStart = len - progress;
						}
						
//						Debug.Log($"len={len}, progress={progress}, indexStart={indexStart}");

						// take substring up until the first $ formatter 
						this.formatParts.Add(format.Substring(progress, indexStart));
						progress += indexStart;
					} else {
						progress++;
						indexStart = progress;
						char formatType = format[indexStart];
						
						if (indexStart < len) {
							if(ValidArgFormatter(formatType)){
//								Debug.Log($"argsCount={argsCount}");
								this.args.Add(args[argsCount]);
								argsCount++;
							}
							
							this.formatParts.Add($"{c}{formatType}");
						}

						progress++;
					}
					
//					Debug.Log($"formatParts={string.Join(", ", this.formatParts)}");
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