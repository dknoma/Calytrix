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
			GameObject container = GameObject.FindWithTag(TilemapConstants.BACKGROUND_CONTAINER_TAG);
			if(container != null) {
				DestroyImmediate(container.gameObject);
			}
			GameObject backgroundContainer = FormatBackgrounds(backgroundList.backgrounds);
			
			if(grid != null) {
				backgroundContainer.transform.SetParent(grid.transform);
			} else {
				GameObject newGridObject = new GameObject();
				Grid newGrid = newGridObject.AddComponent<Grid>();
				newGrid.cellSize = new Vector3(1, 1, 1);
				backgroundContainer.transform.SetParent(newGridObject.transform);
			}
		}

		private static GameObject FormatBackgrounds(List<Background> list) {
			GameObject backgroundsContainer =
				new GameObject("Backgrounds") { tag = TilemapConstants.BACKGROUND_CONTAINER_TAG };
			foreach(Background background in list) {
				BackgroundSettings settings = background.Settings;
				
				GameObject backgroundContainer = new GameObject($"{background.SpriteTexture.name}_container");
				
				BackgroundScroll scroll = backgroundContainer.AddComponent<BackgroundScroll>();
				scroll.Initialize(background.Settings);
				
				GameObject spriteObject = new GameObject($"{background.SpriteTexture.name}_sprite");
				SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
				renderer.sprite = background.GetSprite();
//				renderer.sortingLayerName = settings.SortLayer;
				renderer.sortingOrder = settings.OrderInLayer;
				
				spriteObject.transform.SetParent(backgroundContainer.transform);
				backgroundContainer.transform.SetParent(backgroundsContainer.transform);
			}

			return backgroundsContainer;
		}
	}
}
