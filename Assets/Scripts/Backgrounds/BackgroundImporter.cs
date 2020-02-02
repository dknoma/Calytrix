using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Utility;
using static PixelPerfectCameraSettings;
using static Utility.AssetAndPrefabHelper;
using static Utility.ProjectConstants;

namespace Backgrounds {
	public class BackgroundImporter : MonoBehaviour {
		[SerializeField] private BackgroundImporterList backgroundList;
		[SerializeField] private PixelPerfectCameraSettings settings;
		[SerializeField] private bool reSliceBackgrounds;
		[SerializeField] private bool overwriteBackgrounds;

		private readonly string PATH = $"{SETTINGS_PATH}/ppcam_settings.json";
		private readonly string SETTINGS_OBJECT_PATH = $"{IMPORTER_SETTINGS_OBJECT_PATH}/PixelPerfectCameraSettings.asset";

		private string backgroundAssetsPath;
		private string backgroundListAssetName;
		
		public void LoadPixelPerfectCameraSettings() {
			TextAsset json = AssetDatabase.LoadAssetAtPath<TextAsset>(PATH);
			
			if(json != null) {
				PixelPerfectCameraSettings settingsObject = AssetDatabase.LoadAssetAtPath<PixelPerfectCameraSettings>(SETTINGS_OBJECT_PATH);
				if(settingsObject != null) {
					this.settings = settingsObject;
					SettingsData data = JsonUtility.FromJson<SettingsData>(json.text);
					this.settings.Data = data;
				} else {
					settingsObject = ScriptableObject.CreateInstance<PixelPerfectCameraSettings>();
					SettingsData data = JsonUtility.FromJson<SettingsData>(json.text);
					settingsObject.Data = data;
					SaveAssetToDatabase(settingsObject, SETTINGS_OBJECT_PATH);
					this.settings = settingsObject;
				}
			}
			Debug.Assert(json != null, $"Could not locate settings at path=\"{PATH}\"");
		}
		
		public void SavePixelPerfectCameraSettings() {
			string json = JsonUtility.ToJson(settings.Data);
			Debug.Log($"Writing out {json}");
			File.WriteAllText(PATH, json);
		}
		
		public void ImportBackgrounds() {
			string path = AssetDatabase.GetAssetPath(backgroundList);
			this.backgroundAssetsPath = path.SplitBy($"/{backgroundList.name}\\.asset")[0];
			Debug.Log($"backgroundAssetsPath={backgroundAssetsPath}");
			string[] parts = backgroundAssetsPath.Split('/');
			this.backgroundListAssetName = parts[parts.Length - 1];
			
			(bool formatted, int index) = backgroundList.ProcessBackgrounds(reSliceBackgrounds);
			Debug.Assert(formatted, $"Background at index {index} failed to format.");

			GameObject backgroundCamera = FormatBackgrounds(backgroundList.backgrounds);
		}

		private GameObject FormatBackgrounds(List<Background> list) {
//			GameObject backgroundsContainer =
//				new GameObject("Backgrounds") { tag = TilemapConstants.BACKGROUND_CONTAINER_TAG };
			GameObject backgroundCamera = GameObject.FindWithTag(Tags.BACKGROUND_CAMERA_TAG);
			if(backgroundCamera == null) {
				Debug.Log("Creating new camera");
				backgroundCamera = new GameObject(Tags.BACKGROUND_CAMERA_TAG) { tag = Tags.BACKGROUND_CAMERA_TAG };
				backgroundCamera.transform.position = new Vector3(0, 0, -50);
				backgroundCamera.AddComponent<CameraMovement>();
				Camera bgCam = backgroundCamera.TryAddComponent<Camera>();
				bgCam.orthographic = true;
				bgCam.orthographicSize = 7;
				PixelPerfectCamera ppCam = backgroundCamera.TryAddComponent<PixelPerfectCamera>();
				ppCam.InitializeCamera(settings);
				CreateBackgroundObjects(list, backgroundCamera);
			} else {
				backgroundCamera.TryAddComponent<CameraMovement>();
				if(overwriteBackgrounds) {
					backgroundCamera.transform.RemoveAllChildren();
					CreateBackgroundObjects(list, backgroundCamera);
				}
			}

			return backgroundCamera;
		}

		private void CreateBackgroundObjects(List<Background> backgroundList, GameObject backgroundCamera) {
			BackgroundListData data = AssetDatabase.LoadAssetAtPath<BackgroundListData>(
			                                            $"{backgroundAssetsPath}/{backgroundListAssetName}.asset");
			if(data == null) {
				Debug.Log("Creating new list object.");
				data = ScriptableObject.CreateInstance<BackgroundListData>();
				data.listName =  backgroundListAssetName;

				foreach(Background background in backgroundList) {
					BackgroundSettings settings = background.Settings;

					GameObject backgroundContainer = new GameObject($"{background.SpriteTexture.name}_container");

					BackgroundScroll scroll = backgroundContainer.AddComponent<BackgroundScroll>();
					scroll.Initialize(settings);

					GameObject backgroundObject = new GameObject($"{background.SpriteTexture.name}_sprite");
					SpriteRenderer renderer = backgroundObject.AddComponent<SpriteRenderer>();
					renderer.sprite = background.GetSprite();
//					renderer.sortingLayerName = settings.SortLayer;
					renderer.sortingOrder = settings.OrderInLayer;

					backgroundObject.transform.SetParent(backgroundContainer.transform);
					backgroundContainer.transform.SetParent(backgroundCamera.transform);
					backgroundContainer.transform.position = new Vector3(settings.x, settings.y, 0);

					data.AddBackground(SavePrefab(backgroundContainer, 
					                              backgroundAssetsPath,
					                              $"{backgroundContainer.name}.prefab"));
				}

				SaveAssetToDatabase(data, backgroundAssetsPath, $"{backgroundListAssetName}.asset");
			} else {
				foreach(GameObject backgroundPrefab in data.GetBackgroundPrefabs()) {
					Instantiate(backgroundPrefab, backgroundCamera.transform);
				}
			}
		}
	}
}
