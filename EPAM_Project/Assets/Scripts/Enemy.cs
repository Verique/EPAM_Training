using Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private const float Height = -5f;

    private Transform target;

    private Rigidbody rgbd;
    private Transform eTransform;

    [SerializeField] private float enemySpeed = 100f;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>()?.transform;
        rgbd = GetComponent<Rigidbody>();
        eTransform = transform;
    }

    private void FixedUpdate()
    {
        if (target is null)
        {
            return;
        }
        
        var lookPos = target.position;
        lookPos.z = Height;
        eTransform.LookAt(lookPos);
        rgbd.velocity = eTransform.forward * enemySpeed;
    }
}