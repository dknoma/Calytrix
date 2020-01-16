using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] private int horizontalScrollRate;
    [SerializeField] private int verticalScrollRate;

    private GameObject originalObject;
    private Camera main;

    private Vector3 previousCamPos;

    private void Awake() {
        this.main = Camera.main;
        Debug.Assert(main != null, nameof(main) + " != null");
        this.previousCamPos = main.transform.position;
    }
    
    private void Update() {
        Vector3 position = main.transform.position;
        float hPara = (previousCamPos.x - position.x) * horizontalScrollRate;
        float vPara = (previousCamPos.y - position.y) * verticalScrollRate;

        
        this.transform.position = new Vector3(position.x - hPara, position.y - vPara, 0);
        
        this.previousCamPos = position;
    }
}
