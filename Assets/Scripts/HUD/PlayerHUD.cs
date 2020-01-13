using System.Collections.Generic;
using Characters;
using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    private const float ICON_DISTANCE = 0.8125f;

    [Header("Objects")] 
    [SerializeField] private GameObject hpContainer;
    [SerializeField] private GameObject mpContainer;
    [SerializeField] private CharacterStats playerStats;

    [Header("Sprites")] 
    [SerializeField] private Sprite fullHpContainer;
    [SerializeField] private Sprite halfHpContainer;
    [SerializeField] private Sprite emptyHpContainer;
    [SerializeField] private Sprite fullMpContainer;
    [SerializeField] private Sprite halfMpContainer;
    [SerializeField] private Sprite emptyMpContainer;
    [SerializeField] private Sprite tempHpContainer;

    private readonly IList<SpriteRenderer> hpContainers = new List<SpriteRenderer>();
    private readonly IList<SpriteRenderer> mpContainers = new List<SpriteRenderer>();

    private SignalListener hpListener;

//    private readonly HealthHUD healthHud = new HealthHUD();

    private void Awake() {
        this.hpListener = GetComponent<SignalListener>();
        foreach(Transform child in hpContainer.transform) {
            hpContainers.Add(child.GetComponent<SpriteRenderer>());
        }
    }

    public void OnHPSignal() {
        UpdateHp();
    }

    private void UpdateHp() {
        int currentHp = playerStats.CurrentHp;
        for(int i = 0; i < hpContainers.Count; i++) {
            SpriteRenderer container = hpContainers[i];
            if(i <= currentHp) {
            } else {
//                container.sprite =
            }
        }
    }

//    private class HealthHUD {
//        private int maxHp;
//
//        public int CurrentHp { get; private set; }
//
//        public HealthHUD() {
//        }
//
//        public void UpdateHp(CharacterStats playerStats) {
//            // init current and max hp values
//            Debug.Log("Initializing HealthHUD");
//            this.CurrentHp = playerStats.CurrentHp;
//        }
//    }
}