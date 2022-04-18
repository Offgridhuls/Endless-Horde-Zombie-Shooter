using UnityEngine;

public class AK47Component : WeaponComponent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FireWeapon()
    {
        Vector3 hitLocation;
        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }
            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance,
                    weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;

                TakeDamage(hit);

                Vector3 hitDirection = hit.point - mainCamera.transform.position;

            }

        }
        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
        }
    }

    void TakeDamage(RaycastHit hitInfo)
    {
        IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>(); 
        damagable?.TakeDamage(weaponStats.damage);
    }
}
