using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Tilemaps.TilemapConstants;

namespace Tilemaps {
	public static class SpriteSlicer {
		public static void SliceSprite(string texturePath, int sizeX, int sizeY, float pivotX = 0, float pivotY = 0) {
			Debug.Log($"texturePath={texturePath}");
			TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;
			TextureImporterSettings settings = new TextureImporterSettings();
 
			if (textureImporter == null) {
				return;
			}
			
			textureImporter.ReadTextureSettings(settings);
			settings.textureType = TextureImporterType.Sprite;
			settings.spriteMode = (int) SpriteImportMode.Multiple;
			settings.spritePixelsPerUnit = PIXELS_PER_UNIT;
			settings.filterMode = FilterMode.Point;
			settings.wrapMode = TextureWrapMode.Clamp;
			settings.textureShape = TextureImporterShape.Texture2D;
			settings.npotScale = TextureImporterNPOTScale.None;
//			settings.spriteAlignment = (int) SpriteAlignment.Custom;
			
			textureImporter.SetTextureSettings(settings);
			textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
			textureImporter.crunchedCompression = false;
			textureImporter.compressionQuality = 100;
			textureImporter.isReadable = true;
 
			AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
			EditorUtility.SetDirty(textureImporter);
 
			Texture2D sourceTexture = (Texture2D) AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D));
 
			Debug.Log(sourceTexture.width);
			Debug.Log(sourceTexture.height);
 
			List<SpriteMetaData> spriteMetaDatas = new List<SpriteMetaData>();
			int frameNumber = 0;
			for (int i = sourceTexture.height; i > 0; i -= sizeY) {
				for (int j = 0; j < sourceTexture.width; j += sizeX) {
					SpriteMetaData spriteMetaData = new SpriteMetaData {
                                                   name = sourceTexture.name + "_" + frameNumber,
                                                   rect = new Rect(j, i - sizeY, sizeX, sizeY),
                                                   alignment = 0,
                                                   pivot = new Vector2(pivotX, pivotY)};
 
					Debug.Log($"spriteMetaData={spriteMetaData.name},{spriteMetaData.rect},{spriteMetaData.alignment},{spriteMetaData.pivot}");
					
					spriteMetaDatas.Add(spriteMetaData);
					frameNumber++;
				}
			}
 
			Debug.Log($"count={spriteMetaDatas.Count}");
			textureImporter.spritesheet = spriteMetaDatas.ToArray();
			textureImporter.SaveAndReimport();
		}
	}
}
