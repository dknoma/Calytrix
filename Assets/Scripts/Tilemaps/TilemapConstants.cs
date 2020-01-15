using System.Collections.Generic;

namespace Tilemaps {
	public static class TilemapConstants {
		public const string ENVIRONMENT_GRID_TAG = "EnvironmentGrid";
		public const string TILESETS_PATH = "Assets/Tilesets";

		public const int PIXELS_PER_UNIT = 16;

		public const string LAYER_DECORATION = "Decoration";
		public const string LAYER_HAZARD = "Hazard";
		public const string LAYER_LADDER = "Ladder";

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
			public const string SCROLL_RATE = "scroll_rate";

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
