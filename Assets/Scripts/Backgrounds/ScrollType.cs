using System;
using System.Collections.Generic;
using Tilemaps;
using Utility;

namespace Backgrounds {
	public static class ScrollType {
		public enum Type {
			NORMAL,
			AUTO,
			NONE
		}
		
		public enum ScrollDirection {
			LEFT,
			RIGHT,
			NONE
		}
		
		private static readonly IDictionary<string, Type> SCROLL_TYPE_BY_NAME = new Dictionary<string, Type>();
		private static readonly IDictionary<string, ScrollDirection> SCROLL_DIRECTION_BY_NAME = new Dictionary<string, ScrollDirection>();

		static ScrollType() {
			foreach(Type type in Enum.GetValues(typeof(Type))) {
				SCROLL_TYPE_BY_NAME.Add(type.ToLowerCase(), type);
			}
			
			foreach(ScrollDirection direction in Enum.GetValues(typeof(ScrollDirection))) {
				SCROLL_DIRECTION_BY_NAME.Add(direction.ToLowerCase(), direction);
			}
		}

		public static Type GetTypeByName(string name) {
			return SCROLL_TYPE_BY_NAME.GetOrDefault(name.ToLower());
		}

		public static ScrollDirection GetScrollDirectionByName(string name) {
			return SCROLL_DIRECTION_BY_NAME.GetOrDefault(name.ToLower());
		}
	}
}