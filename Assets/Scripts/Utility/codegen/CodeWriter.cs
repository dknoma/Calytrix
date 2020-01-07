using System.Text;
using Utility.Codegen;

public class CodeWriter { 
    
	public string Write(CodeBlock block) {
		StringBuilder builder = new StringBuilder();

		int j = 0;
		foreach(string part in block.formatParts) {
			if(part[0] == '$' && part.Length > 1) {
				char type = part[1];
				switch(type) {
					case 'A':
						builder.Append(block.args[j++]);
						break;
					case '<':
					case '>':
						break;
				}
			} else {
				builder.Append(part);
			}
		}
			
		return builder.ToString();
	}
}
