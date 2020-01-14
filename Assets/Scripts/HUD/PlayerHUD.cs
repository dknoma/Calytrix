using System.Collections.Generic;
using Characters;
using UnityEngine;

public class PlayerHUD : MonoBehaviour {
    private const float ICON_DISTANCE = 0.8125f;

    [Header("Objects")] 
    [SerializeField] private GameObject hpContainer;
    [SerializeField] private GameObject mpContainer;
    [Header("Scriptable Objects")]
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private CurrencyStash currencyStash;

    [Header("Sprites")] 
    [SerializeField] private Sprite fullHpContainer;
    [SerializeField] private Sprite halfHpContainer;
    [SerializeField] private Sprite emptyHpContainer;
    [SerializeField] private Sprite fullMpContainer;
    [SerializeField] private Sprite halfMpContainer;
    [SerializeField] private Sprite emptyMpContainer;
    [SerializeField] private Sprite tempHpContainer;
    [SerializeField] private Sprite blankContainer;

    private readonly IList<SpriteRenderer> hpContainers = new List<SpriteRenderer>();
    private readonly IList<SpriteRenderer> mpContainers = new List<SpriteRenderer>();

    private void Awake() {
        
        foreach(Transform child in hpContainer.transform) {
            hpContainers.Add(child.GetComponent<SpriteRenderer>());
        }
        foreach(Transform child in mpContainer.transform) {
            mpContainers.Add(child.GetComponent<SpriteRenderer>());
        }
        
        UpdateHp();
        UpdateMp();
    }

    /** -------- OnSignal -------- **/
    public void OnHpSignal() {
        UpdateHp();
    }

    public void OnMpSignal() {
        UpdateMp();
    }

    public void OnCurrencySignal() {
        UpdateCurrency();
    }
    
    /** -------- Helper methods -------- **/
    private void UpdateHp() {
        int currentHp = playerStats.CurrentHp;
        int baseHp = playerStats.BaseHp;
        int tempHp = playerStats.TempHp;
        
        for(int i = 0; i < hpContainers.Count; i++) {
            SpriteRenderer container = hpContainers[i];

            if(i < baseHp) {
//                Debug.Log($"currentHp={currentHp}, i={i}, currentHp + tempHp={currentHp + tempHp}");
                if(currentHp <= i && i < currentHp + tempHp) {
                    container.sprite = tempHpContainer;
                } else {
                    container.sprite = i < currentHp ? fullHpContainer : emptyHpContainer;
                }
            } else {
                if(currentHp <= i && i < currentHp + tempHp) {
                    container.sprite = tempHpContainer;
                } else {
                    container.sprite = blankContainer;
                }
            }
        }
    }

    private void UpdateMp() {
        int currentMp = playerStats.CurrentMp;
        int baseMp = playerStats.BaseMp;
        for(int i = 0; i < mpContainers.Count; i++) {
            SpriteRenderer container = mpContainers[i];

            if(i < baseMp) {
                container.sprite = i < currentMp ? fullMpContainer : emptyMpContainer;
            } else {
                container.sprite = blankContainer;
            }
        }
    }

    private void UpdateCurrency() {
        Debug.Log($"{currencyStash}");
    }
}