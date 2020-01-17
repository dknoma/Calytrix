using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tilemaps {
    public class TiledImporterProperty {
        public string Key { get; }

        public dynamic Value { get; }

        public TiledImporterProperty(TiledTilemapJsonInfo.Layer.LayerProperty prop) {
            string typeString = prop.type;
            string key = prop.name;
            string valueString = prop.value;
            TiledCustomProperty.ValueType type = TiledCustomProperty.GetTypeByString(typeString);

            switch(type) {
                case TiledCustomProperty.ValueType.BOOL:
                    this.Key = key;
                    this.Value = bool.Parse(valueString);
                    break;
                case TiledCustomProperty.ValueType.COLOR:
                    // Color given in hex #00000000
                    ColorUtility.TryParseHtmlString(valueString, out Color color);
                    this.Key = key;
                    this.Value = color;
                    break;
                case TiledCustomProperty.ValueType.FLOAT:
                    this.Key = key;
                    this.Value = float.Parse(valueString);
                    break;
                case TiledCustomProperty.ValueType.FILE:
                    this.Key = key;
                    this.Value = new FileInfo(valueString);
                    break;
                case TiledCustomProperty.ValueType.INT:
                    this.Key = key;
                    this.Value = int.Parse(valueString);
                    break;
                case TiledCustomProperty.ValueType.STRING:
                    this.Key = key;
                    this.Value = valueString;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public bool GetValueAsBool() {
            return (bool) Value;
        }
        
        public Color GetValueAsColor() {
            return (Color) Value;
        }
        
        public FileInfo GetValueAsFile() {
            return (FileInfo) Value;
        }
        
        public float GetValueAsFloat() {
            return (float) Value;
        }

        public int GetValueAsInt() {
            return (int) Value;
        }

        public string GetValueAsString() {
            return (string) Value;
        }
    }
    
    public static class TiledImporterPropertyDictionaryExtension {
        public static bool TryGetValueAsBool(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsBool() : false;
        }
            
        public static Color TryGetValueAsColor(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsColor() : Color.white;
        }
            
        public static FileInfo TryGetValueAsFile(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsFile() : default;
        }
            
        public static float TryGetValueAsFloat(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsFloat() : 0f;
        }
            
        public static int TryGetValueAsInt(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsInt() : 0;
        }
            
        public static string TryGetValueAsString(this IDictionary<string, TiledImporterProperty> dict, string key) {
            TiledImporterProperty prop = dict.GetOrDefault(key);
            return prop != null ? prop.GetValueAsString() : "";
        }
    }
}