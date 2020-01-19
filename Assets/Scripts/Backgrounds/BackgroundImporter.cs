using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Utility;
using static PixelPerfectCameraSettings;

namespace Backgrounds {
	public class BackgroundImporter : MonoBehaviour {
		[SerializeField] private BackgroundImporterList backgroundList;
		[SerializeField] private PixelPerfectCameraSettings settings;

		private readonly string PATH = $"{ProjectConstants.SETTINGS_PATH}/ppcam_settings.json";
		
		public void LoadPixelPerfectCameraSettings() {
			TextAsset json = AssetDatabase.LoadAssetAtPath<TextAsset>(PATH);
			if(json != null) {
				this.settings = ScriptableObject.CreateInstance<PixelPerfectCameraSettings>();
				SettingsData data = JsonUtility.FromJson<SettingsData>(json.text);
				this.settings.Data = data;
			}
			Debug.Assert(json != null, $"Could not locate settings at path=\"{PATH}\"");
		}
		
		public void SavePixelPerfectCameraSettings() {
			string json = JsonUtility.ToJson(settings.Data);
			Debug.Log($"Writing out {json}");
			File.WriteAllText(PATH, json);
		}
		
		public void FormatBackgrounds() {
			(bool formatted, int index) = backgroundList.FormatBackgrounds();
			Debug.Assert(formatted, $"Background at index {index} failed to format.");

//			GameObject grid = GameObject.FindWithTag(TilemapConstants.ENVIRONMENT_GRID_TAG);
//			GameObject container = GameObject.FindWithTag(TilemapConstants.BACKGROUND_CONTAINER_TAG);
//			if(container != null) {
//				DestroyImmediate(container.gameObject);
//			}
			GameObject backgroundCamera = FormatBackgrounds(backgroundList.backgrounds);
			
//				GameObject newGridObject = new GameObject();
//				Grid newGrid = newGridObject.AddComponent<Grid>();
//				newGrid.cellSize = new Vector3(1, 1, 1);
//				backgroundContainer.transform.SetParent(newGridObject.transform);
		}

		private GameObject FormatBackgrounds(List<Background> list) {
//			GameObject backgroundsContainer =
//				new GameObject("Backgrounds") { tag = TilemapConstants.BACKGROUND_CONTAINER_TAG };
			GameObject backgroundCamera = GameObject.FindWithTag(Tags.BACKGROUND_CAMERA_TAG);
			if(backgroundCamera == null) {
				backgroundCamera = new GameObject(Tags.BACKGROUND_CAMERA_TAG) { tag = Tags.BACKGROUND_CAMERA_TAG };
				backgroundCamera.transform.position = new Vector3(0, 0, -50);
				Camera bgCam = backgroundCamera.AddComponent<Camera>();
				bgCam.orthographic = true;
				bgCam.orthographicSize = 7;
				PixelPerfectCamera ppCam = backgroundCamera.AddComponent<PixelPerfectCamera>();
				ppCam.InitializeCamera(settings);
			}
			
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
				backgroundContainer.transform.SetParent(backgroundCamera.transform);
			}

			return backgroundCamera;
		}
	}
}
