using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public UnityAction<int> BULLET_COUNT_CHANGED;
    public UnityAction<bool> RELOADING;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField, Range(0.001f, 1f)]
    private float rateOfFire;

    [SerializeField, Range(1f, 10f)]
    private float reloadTime;

    [SerializeField, Min(1)]
    private int clipSize = 10;
    private int currentClip;

    private int CurrentClip
    {
        get { return currentClip; }
        set
        {
            currentClip = value;
            BULLET_COUNT_CHANGED?.Invoke(value);
        }
    }

    public int ClipSize { get { return clipSize; } }

    private enum State
    {
        StopFiring,
        CanFire,
        NeedReload
    }

    private State state;

    private void Start()
    {
        CurrentClip = clipSize;
        state = State.CanFire;
    }

    public void Fire()
    {
        if (state == State.CanFire)
        {
            ObjectPooler.Instance.Spawn("bullet", transform.position, transform.rotation);
            StartCoroutine("WeaponLogic");
        }
        if (state == State.NeedReload)
        {
            StartCoroutine("Reload");
        }
    }

    private IEnumerator WeaponLogic()
    {
        state = State.StopFiring;

        CurrentClip--;

        if (CurrentClip > 0)
        {
            yield return new WaitForSecondsRealtime(rateOfFire);
            state = State.CanFire;
        }
        else
        {
            state = State.NeedReload;
        }
    }

    private IEnumerator Reload()
    {
        state = State.StopFiring;

        RELOADING?.Invoke(true);

        yield return new WaitForSecondsRealtime(reloadTime);
        CurrentClip = clipSize;

        RELOADING?.Invoke(false);

        state = State.CanFire;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine("Reload");
        }
    }

}
