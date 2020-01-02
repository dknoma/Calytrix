using Characters.Allies;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
 	private Camera cam;
 	private PlayerController player;
    
 	private void OnEnable() {
 		cam = Camera.main;
 		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();    // Get the main player character

        Debug.Log($"player={player.name}");
        
 		Vector3 playerPos = player.transform.position;
 		if (cam != null) { 
	        // Start the camera at the current players location each load
 			transform.position = new Vector3(playerPos.x, playerPos.y, cam.transform.position.z); 
 		}
 	}

 	private void Update() {
	    Vector3 playerPos = player.transform.position;
	    Transform transform1 = cam.transform;
	    transform1.position = new Vector3(playerPos.x, playerPos.y, transform1.position.z); 
    }
}
