using Backgrounds;
using UnityEditor;

namespace Editors {
	[CustomEditor(typeof(BackgroundImporter))]
	public class BackgroundImporterEditor : DruEditor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			BackgroundImporter importer = (BackgroundImporter) target;

			GUIButton("Format Backgrounds", importer.FormatBackgrounds);
		}
	}
}