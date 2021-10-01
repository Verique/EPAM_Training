using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        private const int LmbCode = 0;

        [SerializeField] private Weapon weapon;

        private void Update()
        {
            if (Input.GetMouseButton(LmbCode))
                weapon.Fire();
        }
    }
}