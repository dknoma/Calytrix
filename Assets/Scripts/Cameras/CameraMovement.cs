using Characters.Allies;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraMovement : MonoBehaviour {
 	private PlayerController player;
    
 	private void OnEnable() {
 		player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();    // Get the main player character

        Debug.Log($"player={player.name}");
        
        Vector3 playerPos = player.transform.position;
        Transform thisTransform = transform;
        thisTransform.position = new Vector3(playerPos.x, playerPos.y, thisTransform.position.z);
 	}

 	private void Update() {
	    Vector3 playerPos = player.transform.position;
	    Transform thisTransform = transform;
	    thisTransform.position = new Vector3(playerPos.x, playerPos.y, thisTransform.position.z); 
    }
}
