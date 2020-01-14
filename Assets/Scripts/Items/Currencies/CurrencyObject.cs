using System;
using UnityEngine;
using Utility;

namespace Items.Currencies {
	public class CurrencyObject : MonoBehaviour {
		[SerializeField] private CurrencyStash currencyStash;
		[SerializeField] private CurrencyType.Type type;
		[SerializeField] private Signal currencySignal;

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
			PlaySFX();
			currencyStash.AddToStash(value);
			currencySignal.DoSignal();
			Destroy(this.gameObject);
		}

		private void PlaySFX() {
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
		}
	}
}