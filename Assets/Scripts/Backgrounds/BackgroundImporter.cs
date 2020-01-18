using System.Collections.Generic;
using Tilemaps;
using UnityEngine;

namespace Backgrounds {
	public class BackgroundImporter : MonoBehaviour {
		[SerializeField] private BackgroundImporterList backgroundList;

		public void FormatBackgrounds() {
			(bool formatted, int index) = backgroundList.FormatBackgrounds();
			Debug.Assert(formatted, $"Background at index {index} failed to format.");

			GameObject grid = GameObject.FindWithTag(TilemapConstants.ENVIRONMENT_GRID_TAG);
			GameObject backgroundContainer = FormatBackgrounds(backgroundList.backgrounds);
			if(grid != null) {
				backgroundContainer.transform.SetParent(grid.transform);
			} else {
				Grid newGrid = new Grid();
				newGrid.cellSize = new Vector3(1, 1, 1);
			}
		}

		public GameObject FormatBackgrounds(List<Background> list) {
			GameObject backgroundsContainer = new GameObject("Backgrounds");
			foreach(Background background in list) {
				GameObject backgroundContainer = new GameObject($"{background.SpriteTexture.name}_container");
				
				BackgroundScroll scroll = backgroundContainer.AddComponent<BackgroundScroll>();
				scroll.Initialize(background.Settings);
				
				GameObject sprite = new GameObject(background.Sprite.name);
				sprite.transform.SetParent(backgroundContainer.transform);
			}

			return backgroundsContainer;
		}
	}
}
