using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rgbd;

    [SerializeField]
    private float speed = 5;

    public UnityAction<Vector3, Vector3> PLAYER_MOVED;

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        UserInputMove();
        UserInputRotate();

        PLAYER_MOVED?.Invoke(rgbd.position, UserInput.GetPointerPositioninWorld);
    }

    void UserInputMove()
    {
        Vector2 input = UserInput.WASDInput;
        Vector3 dir = input.normalized;
        Vector3 newPos = rgbd.position + dir * speed * Time.fixedDeltaTime;
        rgbd.MovePosition(newPos);
    }

    void UserInputRotate()
    {
        Vector3 dirToMouse = UserInput.GetPointerPositioninWorld - rgbd.position;
        float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
        rgbd.rotation = Quaternion.Euler(0, 0, angle);
    }

}
