using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 camPos;

    private Vector3 sDampVelocity = Vector3.zero;

    [SerializeField]
    private readonly float smoothTime = 0.2f;

    private void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.PLAYER_MOVED += AdjustCamera;
        offset = playerMovement.transform.position - transform.position;
    }

    private void AdjustCamera(Vector3 playerPos, Vector3 mousePos)
    {
        camPos = Vector3.Lerp(playerPos, mousePos, 0.2f) - offset;
        camPos = Vector3.SmoothDamp(transform.position, camPos, ref sDampVelocity, smoothTime);
    }

    private void LateUpdate()
    {
        transform.position = camPos;
    }
}
