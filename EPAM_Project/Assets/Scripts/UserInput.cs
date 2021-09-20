using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] Camera cam;

    public Vector3 GetPointerPositioninWorld => Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) ? hit.point : Vector3.zero;
    public Vector2 WASDInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
