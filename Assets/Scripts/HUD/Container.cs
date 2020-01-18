using UnityEngine;

public class Container : MonoBehaviour {
    private Camera cam;
    
    private void Start() {
        this.cam = Camera.main;
    }

    private void LateUpdate() {
        this.transform.position = cam.transform.position;
    }
}
