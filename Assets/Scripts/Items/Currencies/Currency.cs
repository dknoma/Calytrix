using UnityEngine;

namespace Items.Currencies {
    public class Currency : MonoBehaviour {
        [SerializeField]
        private CurrencyType.Type type;

        private int value;
        
        // Start is called before the first frame update
        private void Start() {
            this.value = CurrencyType.GetValueByType(type);
        }

        public void OnCollect() {
            AkSoundEngine.PostEvent("Coin", gameObject);
            Destroy(this);
        }
    }
}
