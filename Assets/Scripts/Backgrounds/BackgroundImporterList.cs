using System;
using System.Collections.Generic;
using Tilemaps;
using UnityEditor;
using UnityEngine;
using static Tilemaps.TilemapConstants;

namespace Backgrounds {
	[Serializable]
	[CreateAssetMenu(fileName = "BackgroundImporterList", menuName = "BackgroundImporter/List", order = 1)]
	public class BackgroundImporterList : ScriptableObject {
		public List<Background> backgrounds = new List<Background>();

		public void AddBackground(Sprite sprite) {
			backgrounds.Add(new Background(sprite.texture));
		}

		public Sprite GetBackground(int index) {
			Sprite bg = default;

			if(index < backgrounds.Count) {
				bg = backgrounds[index].GetSprite();
			}
			
			return bg;
		}

		public (bool, int) FormatBackgrounds() {
			bool res = false;
			int index = 0;
			foreach(Background background in backgrounds) {
//				background.SetSpriteIfNecessary();
				res = FormatSprite(background.SpriteTexture);
				if(!res) {
					break;
				}
				index++;
			}

			return (res, index);
		}

		private static bool FormatSprite(Texture2D spriteTexture) {
			Rect rect = new Rect(0, 0, spriteTexture.width, spriteTexture.height);
			Sprite sprite = Sprite.Create(spriteTexture, rect, new Vector2(0.5f,0.5f), PIXELS_PER_UNIT);
			
			string texturePath = AssetDatabase.GetAssetPath(spriteTexture);
			Debug.Log($"so - texturePath={texturePath}");
			
			bool formatted = false;
			if(!string.IsNullOrEmpty(texturePath)) {
				SpriteSlicer.SliceSprite(texturePath, sprite.bounds.size.x, sprite.bounds.size.y, rect);
				formatted = true;
			}

			return formatted;
		}
	}
}