using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField, Range(0.001f, 1f)]
    private float rateOfFire;

    private bool canFire = true;

    public void Fire()
    {
        if (canFire)
        {
            ObjectPooler.Instance.Spawn("bullet", transform.position, transform.rotation);
            StartCoroutine("WeaponCooldown");
        }
    }

    IEnumerator WeaponCooldown()
    {
        canFire = false;
        yield return new WaitForSecondsRealtime(rateOfFire);
        canFire = true;
    }
}
