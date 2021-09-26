using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : Input
{
    public static Vector3 GetPointerPositioninWorld => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) ? hit.point : Vector3.zero;
    public static Vector2 WASDInput => new Vector2(GetAxisRaw("Horizontal"), GetAxisRaw("Vertical"));
}
