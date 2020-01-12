using Boo.Lang;
using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    private const float ICON_DISTANCE = 0.8125f;

    [SerializeField]
    private GameObject hpContainer;
    [SerializeField]
    private GameObject mpContainer;

    private List<SpriteRenderer> activeHpRenders;
    private List<SpriteRenderer> activeMpRenders;

    private void Awake() {
        foreach(Transform child in hpContainer.transform) {
            activeHpRenders.Add(child.GetComponent<SpriteRenderer>());
        }
    }
}
