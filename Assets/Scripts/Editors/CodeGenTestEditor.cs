using UnityEditor;

namespace Editors {
	[CustomEditor(typeof(CodeGenTester))]
	public class CodeGenTestEditor : DruEditor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			CodeGenTester tester = (CodeGenTester) target;
		
			MiniButton("CodeBlock", tester.CodeBlockTest);
			MiniButton("FieldBlock", tester.FieldBlockTest);
			MiniButton("MethodBlock", tester.MethodBlockTest);
		}
	}
}
