using System;

public static class CodeBuilderUtil {
	public enum Modifier {
		PUBLIC,
		PROTECTED,
		PRIVATE,
		ABSTRACT,
		STATIC,
		READONLY,
		DEFAULT
	}

	public enum ClassType {
		CLASS,
		ENUM
	}

	public static string ToLowerCase(this Modifier modifier) {
		return modifier != Modifier.DEFAULT ? modifier.ToString().ToLower() : "";
	}
	
	public static string ToLowerCase(this ClassType type) {
		return type.ToString().ToLower();
	}

	public static string FormattedString(this Type type) {
		return type.ToString().Replace('+', '.');
	}
}
