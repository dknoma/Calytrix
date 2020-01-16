#undef DEBUG

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Tilemaps.SpriteSlicer;
using static Tilemaps.TiledTilemapJsonInfo;
using static Tilemaps.TiledTilemapJsonInfo.Layer;
using static Tilemaps.TilemapConstants;
using static Tilemaps.TiledRenderOrder;
using static Tilemaps.TilemapConstants.CustomPropertyLabels.ValueType;
using static Tilemaps.TilesetFileType;
using static Tilemaps.TilesetFileType.Type;
using Object = UnityEngine.Object;
using Type = System.Type;

namespace Tilemaps {
	public class TiledImporter : MonoBehaviour {
		// Constants
		private const string GRID_NAME = "Level Grid";

		[SerializeField] private TextAsset json;

		// Hide these values from the default drawer so the custom inspector can place them in the right place
		[SerializeField] [HideInInspector] private int gridX = 1;
		[SerializeField] [HideInInspector] private int gridY = 1;
		[SerializeField] [HideInInspector] private int gridZ;

		// Show the path, but disable editing it
		[SerializeField] [DisableInspectorEdit]
		private string path;

		[SerializeField] [DisableInspectorEdit]
		private string tilesetFolder;

		// Tilemap info from Tiled
		private TiledTilemapJsonInfo tilemapJsonInfo;

		[SerializeField] [DisableInspectorEdit]
		private RenderOrder renderOrder;

		[SerializeField] private List<TilesetData> tilesets = new List<TilesetData>();

		// Overwriting settings
		[HideInInspector] public bool overwriteTilesetAssets;
		[HideInInspector] public bool overwriteLevelGrid = true;
		[HideInInspector] public bool overwriteSpritesheetSlice;

		private GameObject grid;

		public string Json => json.name;

		/// <summary>
		/// Processes the tiled .json file, creates tiles from a spritesheet if necessary, then builds the tilemap.
		/// </summary>
		public void ProcessJsonThenBuild() {
			ProcessJSON();
			ProcessSpritesFromSheetIfNecessary();
			BuildTileset();
		}

		/// <summary>
		/// Individual processing methods
		/// </summary>
		public void ProcessJSON() {
			GetAssetPath();
			LoadFromJson();
		}

		private void GetAssetPath() {
			string assetPath = AssetDatabase.GetAssetPath(json);

			this.path = Split(assetPath, "/(\\w)+\\.json")[0];

			string[] parts = Split(path, "/");
			this.tilesetFolder = parts[2];
		}

		private static string[] Split(string input, string regex) {
			return Regex.Split(input, regex);
		}

		private void LoadFromJson() {
			this.tilemapJsonInfo = new TiledTilemapJsonInfo();
			if(json != null) {
				JsonUtility.FromJsonOverwrite(json.text, tilemapJsonInfo);
				this.renderOrder = GetRenderOrder(tilemapJsonInfo.renderorder);

				Debug.Log(tilemapJsonInfo);
			} else {
				Debug.LogError("Something went wrong. JSON file may be invalid");
			}
		}

		/// <summary>
		/// Build tileset
		/// </summary>
		public void BuildTileset() {
			if(tilemapJsonInfo != null) {
				this.grid = GameObject.FindWithTag(ENVIRONMENT_GRID_TAG);
				if(grid != null && overwriteLevelGrid) {
#if DEBUG
                    Debug.Log("Grid exists, need to destroy before instantiating new one.");
#endif
					DestroyImmediate(grid);
					grid = InstantiateNewGrid();
				} else if(grid != null && !overwriteLevelGrid) {
					Debug.Log("Grid exists, do not overwrite grid.");
				} else if(grid == null) {
					grid = InstantiateNewGrid();
				}

				var layers = tilemapJsonInfo.layers;
				foreach(Layer layer in layers) {
					ProcessLayer(layer);
				}
			} else {
				Debug.Log("Tiled info does not seem to be processed. Please make sure to process the JSON file.");
			}
		}

		private GameObject InstantiateNewGrid() {
			GameObject levelGrid = new GameObject(GRID_NAME);
			Grid grid = levelGrid.AddComponent<Grid>();
			grid.cellSize = new Vector3(gridX, gridY, gridZ);
			grid.tag = ENVIRONMENT_GRID_TAG;

			return levelGrid;
		}

		/// <summary>
		/// Process a Layer from the whole tilemap
		/// </summary>
		/// <param name="layer"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private void ProcessLayer(Layer layer) {
			string layerName = layer.name;
			int id = layer.id;
			int totalHeight = layer.height;
			int totalWidth = layer.width;
			int startX = layer.startx;
			int startY = layer.starty;
			int layerX = layer.x;
			int layerY = layer.y;

			IDictionary<string, Property> propertiesByKey = PropertiesArrayToDictionary(layer.properties);
			Tilemap tilemap = NewTilemap(layerName, layerX, layerY, propertiesByKey);

			// Get 
			var chunks = layer.chunks;

			foreach(Chunk chunk in chunks) {
				int[] data = chunk.data;
				int chunkStartX = chunk.x;
				int chunkStartY = chunk.y;
				int height = chunk.height;
				int width = chunk.width;
				int x = chunkStartX;
				int y;

				switch(renderOrder) {
					case RenderOrder.RIGHT_DOWN:
					case RenderOrder.LEFT_DOWN:
						y = -chunkStartY;
						break;
					case RenderOrder.RIGHT_UP:
					case RenderOrder.LEFT_UP:
						y = chunkStartY;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
#if DEBUG
                Debug.Log($"CHUNK START [({x}, {y})]");
#endif
				foreach(int tileIndex in data) {
					if(tileIndex > 0) {
						TilesetData tilesetData = GetTilesetDataFromIndex(tileIndex);
						int offset = tilesetData.firstGid;
						int index = tileIndex - offset;
#if DEBUG
                        Debug.Log($"CHUNK [{tileIndex}, {index}, {offset}, ({x}, {y})]");
#endif
						Tile tile = tilesetData.tilePrefabs[index];
						Vector3Int pos = new Vector3Int(x, y, 0);
						tilemap.SetTile(pos, tile);
					}

					bool validCoordinates;
					(x, y, validCoordinates) = AdvanceOffsets(x, y, width, chunkStartX);

					if(!validCoordinates) {
						break;
					}
				}
			}
		}

		private static string GetPropertyValueAsString(Property property) {
			string res = property == null ? null : (string) property.Value;

			return res;
		}

		private static int GetPropertyValueAsInt(Property property) {
			int res = property == null ? 0 : (int) property.Value;

			return res;
		}

		private static bool GetPropertyValueAsBool(Property property) {
			bool res = property == null ? false : (bool) property.Value;

			return res;
		}

		/// <summary>
		/// Sets the Tilemap
		/// </summary>
		/// <param name="tilemap"></param>
		/// <param name="layerName"></param>
		private static void SetTilemapLayer(Component tilemap, string layerName) {
			if(layerName == null) return;

			tilemap.gameObject.layer = LayerMask.NameToLayer(layerName);
		}

		private static void SetTilemapSortOrder(TilemapRenderer tilemapRenderer, int order) {
			tilemapRenderer.sortingOrder = order;
		}

		/// <summary>
		/// Converts Property array to Dictionary for convenience.
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="propertiesByKey"></param>
		private static ReadOnlyDictionary<string, Property> PropertiesArrayToDictionary(
			IEnumerable<Layer.Property> properties) {
			IDictionary<string, Property> propertiesByKey = new Dictionary<string, Property>();

			foreach(Layer.Property property in properties) {
				Property prop = Property.GetPropertyByType(property);
				propertiesByKey.Add(prop.Key, prop);
			}

			return new ReadOnlyDictionary<string, Property>(propertiesByKey);
		}

		/// <summary>
		/// Gets the proper TilesetData based off the tiled tileIndex. The tileIndex gets the TilesetData if it fits
		/// into the right "bucket" of gid values.
		/// </summary>
		/// <param name="tileIndex"></param>
		/// <returns></returns>
		private TilesetData GetTilesetDataFromIndex(int tileIndex) {
			TilesetData res = null;

			foreach(TilesetData tileset in tilesets) {
				int first = tileset.firstGid;
				int last = tileset.lastGid;

				if(first <= tileIndex && tileIndex <= last) {
					res = tileset;
					break;
				}
			}

			return res;
		}

		/// <summary>
		/// Creates a new Tilemap object for the level. Sets the position to the starting points stated in the Tiled
		/// tilemap.json file. Initializes the Tilemap to have the proper renderer and collider as well.
		/// </summary>
		/// <param name="layerName"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="propertiesByKey"></param>
		/// <returns></returns>
		private Tilemap NewTilemap(string layerName, int x, int y, IDictionary<string, Property> propertiesByKey) {
			GameObject layer = new GameObject(layerName);
			layer.transform.position = new Vector3(x, y, 0);
			layer.transform.parent = grid.transform;
			Tilemap tilemap = layer.AddComponent<Tilemap>();
			TilemapRenderer tilemapRenderer = layer.AddComponent<TilemapRenderer>();
			TilemapCollider2D coll = layer.AddComponent<TilemapCollider2D>();
			tilemapRenderer.sortOrder = TilemapRenderer.SortOrder.TopRight;

			string propertyLayerName = GetPropertyValueAsString(
			                                propertiesByKey.GetOrDefault(CustomPropertyLabels.LAYER_KEY_NAME));
			int orderInLayer = GetPropertyValueAsInt(
			                                propertiesByKey.GetOrDefault(CustomPropertyLabels.SORT_ORDER_KEY_NAME));
			
			SetTilemapLayer(tilemap, propertyLayerName);
			SetTilemapSortOrder(tilemapRenderer, orderInLayer);

			if(propertyLayerName.Equals("Background")) {
				int scrollRate = GetPropertyValueAsInt(propertiesByKey.GetOrDefault(CustomPropertyLabels.SCROLL_RATE));
				// TODO - get scroll rate for background and add parallax
				
			}
			
			
			return tilemap;
		}

		/// <summary>
		/// Advance the current offsets of which coordinate on the tilemap to place a Tile. If a coordinate reaches the
		/// end chunk, the coordinate advances to the next y value and resets the x value to the chunk start x value.
		/// </summary>
		/// <param name="initialX"></param>
		/// <param name="initialY"></param>
		/// <param name="width"></param>
		/// <param name="chunkStartX"></param>
		/// <returns>(x, y, validCoordinates)</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private (int, int, bool) AdvanceOffsets(int initialX, int initialY, int width, int chunkStartX) {
			int x = initialX;
			int y = initialY;

			switch(renderOrder) {
				case RenderOrder.RIGHT_DOWN:
					x++;
					int check = x - chunkStartX;
					if(check == width) {
						x = chunkStartX;
						y--;
					}

					break;
				case RenderOrder.RIGHT_UP:
					x++;
					if(x - chunkStartX == width) {
						x = chunkStartX;
						y++;
					}

					break;
				case RenderOrder.LEFT_DOWN:
					x--;
					if(chunkStartX - x == width) {
						x = chunkStartX;
						y--;
					}

					break;
				case RenderOrder.LEFT_UP:
					x--;
					if(chunkStartX - x == width) {
						x = chunkStartX;
						y++;
					}

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			bool validCoordinates = ValidateCoordinates(chunkStartX, x, width);
			return (x, y, validCoordinates);
		}

		/// <summary>
		/// Checks to see if the current coordinates that a tile is trying to be placed into is valid.
		/// </summary>
		/// <param name="chunkStartX"></param>
		/// <param name="x"></param>
		/// <param name="width"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private bool ValidateCoordinates(int chunkStartX, int x, int width) {
			bool valid = true;
			switch(renderOrder) {
				case RenderOrder.RIGHT_DOWN:
				case RenderOrder.RIGHT_UP:
					if(x - chunkStartX > width) {
						Debug.LogError($"Total width between x={x}, chunkStartX={chunkStartX} is larger than the expected width {width}");
						DestroyImmediate(this.grid);
						valid = false;
					}

					break;
				case RenderOrder.LEFT_DOWN:
				case RenderOrder.LEFT_UP:
					if(chunkStartX - x > width) {
						Debug.LogError($"Total width between x={x}, chunkStartX={chunkStartX} is larger than the expected width {width}");
						DestroyImmediate(this.grid);
						valid = false;
					}

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return valid;
		}

		/// <summary>
		/// Process spritesheets from their png files to create tile prefabs for future use.
		/// </summary>
		public void ProcessSpritesFromSheetIfNecessary() {
			this.ParseTilesetFiles();
		}

		/// <summary>
		/// Parse Tiled .tsx files to get the correct values from the fields and attributes.
		/// </summary>
		private void ParseTilesetFiles() {
			this.tilesets = new List<TilesetData>();
			var tilesetsInfo = tilemapJsonInfo.tilesets;
			int tilesetCount = tilesetsInfo.Length;

			// Create assets for each tileset
			int i = 0;
			foreach(Tileset tileset in tilesetsInfo) {
				string tilesetSource = tileset.source;
				string absoluteTilesetSource = FromAbsolutePath(tilesetSource);
				TilesetInfo tilesetInfo = ParseFile(absoluteTilesetSource);

				string tilesetName = tilesetInfo.name;

				// Get the full path for the tileset ScriptableObject asset
				string tilesetAssetFilePath = GetAssetPath(TILESETS_PATH, tilesetName);
				string layerName = tilemapJsonInfo.layers[i].name;

				// Check if tileset exists
				bool tilesetAssetExists = TilesetDataExists(tilesetAssetFilePath);
				Debug.LogFormat($"{tilesetAssetFilePath} exists = {tilesetAssetExists}");

				// Add existing assets if they don't exist and don't need to be overwritten
				if(tilesetAssetExists && !overwriteTilesetAssets) {
					AddExistingTilesetDataAsset(tilesetAssetFilePath);
				} else {
					string sheetPath = FromAbsolutePath(tilesetInfo.image);

					// If re-slicing sheet, get dimensions via the tileset.json
					if(overwriteSpritesheetSlice) {
						SliceSprite(sheetPath, tilesetInfo.tilewidth, tilesetInfo.tileheight);
					}
#if DEBUG
                    Debug.LogFormat("sheetPath [{0}]", sheetPath);
#endif
					TilesetData asset = ScriptableObject.CreateInstance<TilesetData>();

					asset.name = layerName;
					asset.firstGid = tileset.firstgid;

					// Only overwrite lastGid if more than one tileset. Else default of int.MAX
					if(tilesetCount > i + 1) {
						asset.lastGid = tilesetsInfo[i + 1].firstgid - 1;
					}

					ProcessSpritesFromSheet(tilesetName, sheetPath, asset);
				}

				i++;
			}
		}

		/// <summary>
		/// Parse the tileset file into Unity readable code
		/// </summary>
		/// <param name="tilesetSourcePath"></param>
		/// <returns></returns>
		/// <exception cref="FormatException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private static TilesetInfo ParseFile(string tilesetSourcePath) {
#if DEBUG
            Debug.Log(tilesetSourcePath);
#endif
			TilesetFileType.Type type = GetTypeFromSourcePath(tilesetSourcePath);

			TilesetInfo tilesetInfo;
			switch(type) {
				case JSON:
					Debug.Log($"type={type}");
					tilesetInfo = ParseJson(tilesetSourcePath);
					break;
				case TSX:
					throw new FormatException($"type {type} not supported.");
				default:
					throw new ArgumentOutOfRangeException();
			}
#if DEBUG
            Debug.Log(tsxInfo);
#endif
			return tilesetInfo;
		}

		/// <summary>
		/// Read the text from the file at the path into JSON format, then convert that into TilesetInfo
		/// </summary>
		/// <param name="sourcePath"></param>
		/// <returns></returns>
		private static TilesetInfo ParseJson(string sourcePath) {
			string json;
			using(TextReader reader = new StreamReader(sourcePath)) {
				json = reader.ReadToEnd();
			}

			return JsonUtility.FromJson<TilesetInfo>(json);
		}

		/// <summary>
		/// Check if the asset exists at this path
		/// </summary>
		/// <param name="tilesetAssetFilePath"></param>
		/// <returns></returns>
		private static bool TilesetDataExists(string tilesetAssetFilePath) {
			return File.Exists(tilesetAssetFilePath);
		}

		/// <summary>
		/// Adds this tileset asset into the list for import
		/// </summary>
		/// <param name="filePath"></param>
		private void AddExistingTilesetDataAsset(string filePath) {
			TilesetData tilesetAsset = AssetDatabase.LoadAssetAtPath<TilesetData>(filePath);

			tilesets.Add(tilesetAsset);
		}

		private static string GetAssetPath(string path, string sourcename) {
			return $"{path}/{sourcename}.asset";
		}

		private string FromAbsolutePath(string file) {
			return $"{this.path}/{file}";
		}

		/// <summary>
		/// Process sprites from a .png spritesheet. Spritesheet isn't automatically split and must be done
		/// manually beforehand. 
		/// </summary>
		/// <param name="tilesetName"></param>
		/// <param name="filename"></param>
		/// <param name="asset"></param>
		private void ProcessSpritesFromSheet(string tilesetName, string filename, TilesetData asset) {
			var sprites = AssetDatabase.LoadAllAssetsAtPath(filename)
			                           .OfType<Sprite>()
			                           .ToArray();

			// Sort the order of sprites as they may be out of order
			Array.Sort(sprites, (o1, o2) => {
				                    var s1 = o1.name.Split('_');
				                    var s2 = o2.name.Split('_');
				                    string n1 = s1[s1.Length - 1];
				                    string n2 = s2[s2.Length - 1];

				                    return int.Parse(n1) - int.Parse(n2);
			                    });

			foreach(Sprite sprite in sprites) {
#if DEBUG
                Debug.LogFormat("sprite [{0}]", sprite);
#endif
				CreateTilesetAssets(asset, sprite);
			}

			tilesets.Add(asset);
			SaveAssetToDatabase(asset, TILESETS_PATH, tilesetName);
		}

		private static void SaveAssetToDatabase(Object asset, string path, string filename) {
			AssetDatabase.CreateAsset(asset, GetAssetPath(path, filename));
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();
		}

		/// <summary>
		/// Create Tile assets for a given tileset
		/// </summary>
		/// <param name="asset"></param>
		/// <param name="sprite"></param>
		private void CreateTilesetAssets(TilesetData asset, Sprite sprite) {
			string tilename = sprite.name;

			Tile tile = CreateTileAssets(sprite);
			SaveAssetToDatabase(tile, path, tilename);
#if DEBUG
            Debug.LogFormat("NEW SPRITE [{0}]", newTile);
#endif
			asset.tilePrefabs.Add(tile);
		}

		/// <summary>
		/// Create the Tile assets
		/// </summary>
		/// <param name="sprite"></param>
		/// <returns></returns>
		private static Tile CreateTileAssets(Sprite sprite) {
			Tile tile = ScriptableObject.CreateInstance<Tile>();
			tile.sprite = sprite;

			return tile;
		}

		private abstract class Property {
			public abstract string Key { get; }
			public abstract Type Type { get; }

			public abstract dynamic Value { get; }

			public static Property GetPropertyByType(Layer.Property prop) {
				string typeString = prop.type;
				string key = prop.name;
				string valueString = prop.value;
				CustomPropertyLabels.ValueType type = CustomPropertyLabels.GetTypeByString(typeString);

				switch(type) {
					case BOOL:
						return new Property<bool>(key, bool.Parse(valueString));
					case COLOR:
						Color color = Color.white;
						ColorUtility.TryParseHtmlString(valueString, out color);
						return new Property<Color>(key, color);
					case FLOAT:
						return new Property<float>(key, float.Parse(valueString));
					case FILE:
						return new Property<FileInfo>(key, new FileInfo(valueString));
					case INT:
						return new Property<int>(key, int.Parse(valueString));
					case STRING:
						return new Property<string>(key, valueString);
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private class Property<T> : Property {
			public override string Key { get; }
			public override Type Type => typeof(T);

			public override dynamic Value { get; }

			public Property(string key, T value) {
				this.Key = key;
				this.Value = value;
			}
		}
	}
}