﻿using UnityEngine;

namespace Music {
	public class MusicTester : MonoBehaviour {
		private void Start() {
			// play music from event name
			AkSoundEngine.PostEvent("mad_forest", gameObject);
		}
	}
}