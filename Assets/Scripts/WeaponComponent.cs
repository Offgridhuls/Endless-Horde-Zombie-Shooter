using System.Collections;
using System.Collections.Generic;
using Codice.Client.Commands;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum WeaponType
{
    None, 
    Pistol, 
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, 
    FullAuto, 
    ThreeRoundBurst
}

[System.Serializable]
public struct WeaponStats
{ 
    WeaponType weaponType;

    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;

    public int totalBullets;

    public LayerMask weaponHitLayers;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;

    protected WeaponHolder weaponHolder;

    [SerializeField] 
    public WeaponStats weaponStats;


    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Awake()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;

    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }

    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        Debug.Log("Firing Weapon");
        weaponStats.bulletsInClip--;
    }

    public virtual void StartReloading()
    {

    }

    public virtual void StopReloading()
    {

    }
}
