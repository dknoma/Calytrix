using System;
using System.Collections.Generic;
using Tilemaps;
using UnityEditor;
using UnityEngine;
using static Utility.ProjectConstants;

namespace Backgrounds {
	[Serializable]
	[CreateAssetMenu(fileName = "BackgroundImporterList", menuName = "BackgroundImporter/List", order = 1)]
	public class BackgroundImporterList : ScriptableObject {
		public List<Background> backgrounds = new List<Background>();

		public (bool, int) ProcessBackgrounds(bool overwrite) {
			bool res = true;
			int index = 0;
			
			if(overwrite) {
				foreach(Background background in backgrounds) {
					res = CreateSpriteForTexture2D(background.SpriteTexture);
					if(!res) {
						break;
					}

					index++;
				}
			}

			return (res, index);
		}

		private static bool CreateSpriteForTexture2D(Texture2D spriteTexture) {
			bool formatted = false;
			
			Rect rect = new Rect(0, 0, spriteTexture.width, spriteTexture.height);
			Sprite sprite = Sprite.Create(spriteTexture, rect, new Vector2(0.5f, 0.5f), PIXELS_PER_UNIT);

			string texturePath = AssetDatabase.GetAssetPath(spriteTexture);
			Debug.Log($"so - texturePath={texturePath}");

			if(!string.IsNullOrEmpty(texturePath)) {
				SpriteSlicer.SliceSprite(texturePath, sprite.bounds.size.x, sprite.bounds.size.y, rect);
				formatted = true;
			}

			return formatted;
		}
	}
}