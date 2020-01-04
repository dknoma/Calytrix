using System;
using UnityEngine;
using Utility;

namespace Items.Currencies {
	public class Currency : MonoBehaviour {
		[SerializeField] private CurrencyType.Type type;

		private int value;

		private void OnEnable() {
			if(!gameObject.CompareTag(Tags.CURRENCY)) {
				gameObject.tag = Tags.CURRENCY;
			}
		}

		// Start is called before the first frame update
		private void Start() {
			this.value = CurrencyType.GetValueByType(type);
		}

		public void OnCollect() {
			switch(type) {
				case CurrencyType.Type.COIN:
					AkSoundEngine.PostEvent(Tags.COIN, gameObject);
					break;
				case CurrencyType.Type.GEM:
					AkSoundEngine.PostEvent(Tags.GEM, gameObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Destroy(this.gameObject);
		}
	}
}