using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour {

    Vector3 offset;
    Vector3 camPos;
    Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.PLAYER_MOVED += AdjustCamera;
        offset = playerMovement.transform.position - transform.position;
    }
    
    void AdjustCamera(Vector3 playerPos, Vector3 mousePos){
        Vector3 mouseDir = (mousePos - playerPos).normalized;
        camPos = Vector3.Lerp(playerPos, mousePos, 0.2f) - offset;
    }

    void LateUpdate() {
        transform.position = camPos;
    }
}
