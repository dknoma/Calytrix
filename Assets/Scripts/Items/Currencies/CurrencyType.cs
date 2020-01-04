using System.Collections.Generic;
using Tilemaps;

namespace Items.Currencies {
	public static class CurrencyType {
		public enum Type {
			COIN,
			GEM
		}

		private const int COIN_VALUE = 1;
		private const int GEM_VALUE = 10;

		private static readonly IDictionary<Type, int> VALUE_BY_TYPE = new Dictionary<Type, int>();

		static CurrencyType() {
			VALUE_BY_TYPE.Add(Type.COIN, COIN_VALUE);
			VALUE_BY_TYPE.Add(Type.GEM, GEM_VALUE);
		}

		public static int GetValueByType(Type type) {
			return VALUE_BY_TYPE.GetOrDefault(type, COIN_VALUE);
		}
	}
}