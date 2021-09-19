using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    Rigidbody rgbd;
    Camera mainCam;
    [SerializeField] float speed = 5;
    Vector3 mousePos = Vector3.zero;

    void Start() {
        rgbd = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    void FixedUpdate() {
        UserInputMove();
        UserInputRotate();
    }

    void UserInputMove() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        Vector3 dir = input.normalized;
        Vector3 newPos = rgbd.position + dir * speed * Time.fixedDeltaTime;
        rgbd.MovePosition(newPos);
    }

    void UserInputRotate() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit)){
            mousePos = rayHit.point;
            Vector3 dirToMouse = rayHit.point - rgbd.position;
            float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
            rgbd.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(mousePos, 5);
    }
}
