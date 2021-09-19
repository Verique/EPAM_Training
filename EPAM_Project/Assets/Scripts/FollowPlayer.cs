using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour {

    [SerializeField] Transform playerTransform;
    Vector3 offset;
    Camera cam;


    void Start() {
        offset = playerTransform.position - transform.position;
        cam = GetComponent<Camera>();
    }

    void LateUpdate() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit rayHit)){
            Vector3 mousePos = rayHit.point;
            Vector3 mouseDir = (mousePos - playerTransform.position).normalized;
            Vector3 camPoint = Vector3.Lerp(playerTransform.position, mousePos, 0.2f);
            transform.position = camPoint - offset;
        }
    }
}
