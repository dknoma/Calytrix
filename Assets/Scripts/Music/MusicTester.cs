using UnityEngine;

namespace Music {
    public class MusicTester : MonoBehaviour {
        private void Start() {
            AkSoundEngine.PostEvent("Compo", gameObject);
        }
    }
}
