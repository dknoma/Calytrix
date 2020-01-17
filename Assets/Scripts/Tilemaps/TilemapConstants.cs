using System.Collections.Generic;
using UnityEditorInternal;

namespace Tilemaps {
	public static class TilemapConstants {
		public const string ENVIRONMENT_GRID_TAG = "EnvironmentGrid";
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

		public static class CustomPropertyLabels {
			public enum ValueType {
				BOOL,
				COLOR,
				FLOAT,
				FILE,
				INT,
				STRING
			}
			
			private const string BOOL = "bool";
			private const string COLOR = "color";
			private const string FLOAT = "float";
			private const string FILE = "file";
			private const string INT = "int";
			private const string STRING = "string";
			
			private static readonly IDictionary<string, ValueType> PROPERTY_TYPES_BY_STRING = new Dictionary<string, ValueType>();
			
			public const string LAYER_KEY_NAME = "layer_name";
			public const string SORT_ORDER_KEY_NAME = "order_in_layer";
			public const string HORIZONTAL_SCROLL_RATE = "horizontal_scroll_rate";
			public const string VERTICAL_SCROLL_RATE = "vertical_scroll_rate";
			public const string SCROLL_TYPE = "scroll_type";
			public const string SCROLL_DIRECTION = "scroll_direction";

			static CustomPropertyLabels() {
				PROPERTY_TYPES_BY_STRING.Add(BOOL, ValueType.BOOL);
				PROPERTY_TYPES_BY_STRING.Add(COLOR, ValueType.COLOR);
				PROPERTY_TYPES_BY_STRING.Add(FLOAT, ValueType.FLOAT);
				PROPERTY_TYPES_BY_STRING.Add(FILE, ValueType.FILE);
				PROPERTY_TYPES_BY_STRING.Add(INT, ValueType.INT);
				PROPERTY_TYPES_BY_STRING.Add(STRING, ValueType.STRING);
			}

			public static ValueType GetTypeByString(string type) {
				return PROPERTY_TYPES_BY_STRING.GetOrDefault(type, ValueType.STRING);
			}
		}
	}
}
