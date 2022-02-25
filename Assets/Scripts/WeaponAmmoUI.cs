using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] public TMP_Text weaponNameText;
    [SerializeField] public TMP_Text currentBulletCountText;
    [SerializeField] public TMP_Text totalBulletCountText;

    [SerializeField] private WeaponComponent weaponComponent;
   //Start is called before the first frame update
    void Start()
    {
        //weaponComponent = FindObjectOfType<WeaponComponent>();
    }


    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }
    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletCountText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalBulletCountText.text = weaponComponent.weaponStats.totalBullets.ToString();
    }
}
