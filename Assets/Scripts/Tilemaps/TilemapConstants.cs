using System.Collections.Generic;
using UnityEditorInternal;

namespace Tilemaps {
	public static class TilemapConstants {
		public const string ENVIRONMENT_GRID_TAG = "EnvironmentGrid";
		public const string BACKGROUND_CONTAINER_TAG = "background_container";
		
		public const string TILESETS_PATH = "Assets/Tilesets";

		public const int PIXELS_PER_UNIT = 16;

		public const string LAYER_BACKGROUND = "Background";
		public const string LAYER_DECORATION = "Decoration";
		public const string LAYER_GROUND = "Ground";
		public const string LAYER_HAZARD = "Hazard";
		public const string LAYER_SPECIAL = "Special";
		
		private static readonly IDictionary<string, string> LAYERS_BY_NAME = new Dictionary<string, string>();

		static TilemapConstants() {
			foreach(string layer in InternalEditorUtility.layers) {
				LAYERS_BY_NAME.Add(layer.ToLower(), layer);
			}
		}

		public static string GetLayerByName(string name) {
			return LAYERS_BY_NAME.GetOrDefault(name.ToLower(), LAYER_GROUND);
		}
	}
}
