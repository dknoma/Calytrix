using System;
using System.Collections;
using System.Linq;
using System.Text;
using Utility;

namespace Tilemaps {
	[Serializable]
	public class TilesetInfo : JsonInfo {
		public int columns;
		public string image;
		public int imageheight;
		public int imagewidth;
		public int margin;
		public string name;
		public int spacing;
		public int tilecount;
		public string tiledversion;
		public int tileheight;
		public int tilewidth;
		public string version;
		public string type;
		public EditorSettings editorsettings;
		public Tile[] tiles;
		
		[Serializable]
		public class EditorSettings : JsonInfo {
			public Export export;
        
			[Serializable]
			public class Export : JsonInfo {
				public string format;
				public string target;
			}
		}
		
		[Serializable]
		public class Tile : JsonInfo {
			public int id;
			public ObjectGroup objectgroup;
			
        
			[Serializable]
			public class ObjectGroup : JsonInfo {
				public string draworder;
				public string name;
				public Object[] objects;

				[Serializable]
				public class Object : JsonInfo {
					public int id;
					public int x;
					public int y;
					public int width;
					public int height;
					public int roation;
					public string type;
					public bool visible;
				}
			}
		}
	}
}
