using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

namespace Tilemaps {
    public abstract class Property {
        public abstract string Key { get; }
        public abstract Type Type { get; }

        public abstract dynamic Value { get; }
			

        public static string GetPropertyValueAsString(Property property) {
            string res = property == null ? "" : (string) property.Value;

            return res;
        }

        public static int GetPropertyValueAsInt(Property property) {
            int res = property == null ? 0 : (int) property.Value;

            return res;
        }

        public static bool GetPropertyValueAsBool(Property property) {
            bool res = property == null ? false : (bool) property.Value;

            return res;
        }

        public static Property GetPropertyByType(TiledTilemapJsonInfo.Layer.LayerProperty prop) {
            string typeString = prop.type;
            string key = prop.name;
            string valueString = prop.value;
            CustomPropertyLabels.ValueType type = CustomPropertyLabels.GetTypeByString(typeString);

            switch(type) {
                case CustomPropertyLabels.ValueType.BOOL:
                    return new Property<bool>(key, bool.Parse(valueString));
                case CustomPropertyLabels.ValueType.COLOR:
                    Color color = Color.white;
                    ColorUtility.TryParseHtmlString(valueString, out color);
                    return new Property<Color>(key, color);
                case CustomPropertyLabels.ValueType.FLOAT:
                    return new Property<float>(key, float.Parse(valueString));
                case CustomPropertyLabels.ValueType.FILE:
                    return new Property<FileInfo>(key, new FileInfo(valueString));
                case CustomPropertyLabels.ValueType.INT:
                    return new Property<int>(key, int.Parse(valueString));
                case CustomPropertyLabels.ValueType.STRING:
                    return new Property<string>(key, valueString);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal class Property<T> : Property {
        public override string Key { get; }
        public override Type Type => typeof(T);

        public override dynamic Value { get; }

        public Property(string key, T value) {
            this.Key = key;
            this.Value = value;
        }
    }
    
    internal static class CustomPropertyLabels {
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

        static CustomPropertyLabels() {
            foreach(ValueType type in Enum.GetValues(typeof(ValueType))) {
                PROPERTY_TYPES_BY_STRING.Add(type.ToLowerCase(), type);
            }
        }

        public static ValueType GetTypeByString(string type) {
            return PROPERTY_TYPES_BY_STRING.GetOrDefault(type, ValueType.STRING);
        }
    }
}