using UnityEngine;

namespace Backgrounds {
	public class BackgroundImporter : MonoBehaviour {
		[SerializeField] private BackgroundImporterList backgroundList;

		public void FormatBackgrounds() {
			(bool formatted, int index) = backgroundList.FormatBackgrounds();
			Debug.Assert(formatted, $"Background at index {index} failed to format.");
			
			
//			backgroundContainer.transform.SetParent(grid.transform);
//			GameObject backgroundContainer = new GameObject($"{layerName}_container");
//			layer.transform.SetParent(backgroundContainer.transform);
		}
	}
}
