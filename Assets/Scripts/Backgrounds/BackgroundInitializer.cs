using Characters.Allies;
using UnityEngine;

namespace Backgrounds {
    public class BackgroundInitializer : MonoBehaviour {

        private static PlayerController PLAYER;
    
        private void Awake() {
            GameObject playerObj = GameObject.FindWithTag("Player");
            bool playerFound = playerObj.TryGetComponent(out PLAYER);
            Debug.LogError("playerFound=" + playerFound);
            // TODO - figure out spawning mechanics. player should be initialized using spawn points
            //        perhaps a signal should be raised upon player instantiation, updating player reference, etc. 
        }

        public static PlayerController GetPlayerReference() {
            return PLAYER;
        }
    }
}