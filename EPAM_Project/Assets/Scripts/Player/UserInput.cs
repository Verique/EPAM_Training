using UnityEngine;

namespace Player
{
    public class UserInput : Input
    {
        public static Vector3? GetPointerPositionInWorld =>
            Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out var hit)
                ? hit.point
                : (Vector3?) null;

        public static Vector2 WasdInput => new Vector2(GetAxisRaw("Horizontal"), GetAxisRaw("Vertical"));
    }
}