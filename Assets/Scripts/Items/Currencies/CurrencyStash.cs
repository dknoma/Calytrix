using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyStash", menuName = "ScriptableObjects/CurrencyStash", order = 1)]
public class CurrencyStash : ScriptableObject {
    private const int MAX_STASH_COUNT = 9999;
    
    [SerializeField] private int stash = 0;

    public void AddToStash(int count) {
        this.stash = Mathf.Clamp(stash + count, 0, MAX_STASH_COUNT);
    }
    
    public void RemoveFromStash(int count) {
        this.stash = Mathf.Clamp(stash - count, 0, MAX_STASH_COUNT);
    }

    public override string ToString() {
        return $"stash={stash}";
    }
}
