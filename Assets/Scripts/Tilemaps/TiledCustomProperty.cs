using System;
using System.Collections.Generic;
using Utility;

namespace Tilemaps {
    public static class TiledCustomProperty {
        public enum ValueType {
            BOOL,
            COLOR,
            FLOAT,
            FILE,
            INT,
            STRING
        }
			
        private static readonly IDictionary<string, ValueType> PROPERTY_TYPES_BY_STRING = new Dictionary<string, ValueType>();
			
        public const string LAYER_KEY_NAME = "layer_name";
        public const string SORT_ORDER_KEY_NAME = "order_in_layer";
        public const string HORIZONTAL_SCROLL_RATE = "horizontal_scroll_rate";
        public const string VERTICAL_SCROLL_RATE = "vertical_scroll_rate";
        public const string SCROLL_TYPE = "scroll_type";
        public const string SCROLL_DIRECTION = "scroll_direction";

        static TiledCustomProperty() {
            foreach(ValueType type in Enum.GetValues(typeof(ValueType))) {
                PROPERTY_TYPES_BY_STRING.Add(type.ToLowerCase(), type);
            }
        }

        public static ValueType GetTypeByString(string type) {
            return PROPERTY_TYPES_BY_STRING.GetOrDefault(type, ValueType.STRING);
        }
    }
}
