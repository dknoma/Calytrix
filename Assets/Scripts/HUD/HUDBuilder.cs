using UnityEngine;

public class HUDBuilder : MonoBehaviour {
    private const float ICON_DISTANCE = 0.8125f;
    
    [SerializeField] private int maxHpContainerCount = 8;
    [SerializeField] private int maxMpContainerCount = 8;

    [SerializeField] private GameObject hpContainerPrefab;
    [SerializeField] private GameObject mpContainerPrefab;
}
