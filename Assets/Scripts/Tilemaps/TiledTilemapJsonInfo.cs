using System;
using System.Collections;
using System.Linq;
using System.Text;
using Utility;

namespace Tilemaps {
    /// <summary>
    /// Unity class used for holding serialized Tiled JSON tilemap info.
    /// </summary>
    [Serializable]
    public class TiledTilemapJsonInfo : JsonInfo {
        public int compressionlevel;
        public EditorSettings editorsettings;
        public int height;
        public bool infinite;
        public Layer[] layers;
        public int nextlayerid;
        public int nextobjectid;
        public string orientation;
        public string renderorder;
        public string tiledversion;
        public int tileheight;
        public Tileset[] tilesets;
        public int tilewidth;
        public string type;
        public double version;
        public int width;
        
        /// <summary>
        /// Subclasses
        /// </summary>
        [Serializable]
        public class EditorSettings : JsonInfo {
            public Export export;
            
            [Serializable]
            public class Export {
                public string format;
                public string target;
            }
        }

        [Serializable]
        public class Layer : JsonInfo {
            public int id;
            public string name;
            public Chunk[] chunks;
            public Property[] properties;
            public int height;
            public int width;
            public int startx;
            public int starty;
            public int x;
            public int y;
            public string type;
            public int opacity;
            public bool visible;
            
            [Serializable]
            public class Chunk : JsonInfo {
                public int[] data;
                public int height;
                public int width;
                public int x;
                public int y;
            }

            [Serializable]
            public class Property : JsonInfo {
                public string name;
                public string type;
                public string value;
            }
        }

        [Serializable]
        public class Tileset : JsonInfo {
            public int firstgid;
            public string source;
        }
    }
}