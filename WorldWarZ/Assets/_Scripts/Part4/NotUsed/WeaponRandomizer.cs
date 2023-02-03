using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRandomizer : MonoBehaviour
{
    public HumanController_Part3 hc;
    public BulletDestroy bd;
    public Weapon_Part3 wp;

    float fireRate;
    float shootDist;
    int ammo;
    int dmg;

    private void Start()
    {
        fireRate = Random.Range(0.1f, 3f);
        dmg = Random.Range(1, 10);
        shootDist = Random.Range(15, 20);
        ammo = Random.Range(10, 40);
    }

    public void RandomizerPlz()
    {
        wp.waitForSeconds = fireRate;
        bd.minDamage = dmg;
        hc.shootingDist = shootDist;
        hc.currentAmmo = ammo;

        if (hc.currentAmmo > hc.maxAmmo)
        {
            hc.currentAmmo = hc.maxAmmo;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Human")
    //    {
            

    //        Destroy(this.gameObject);
    //    }
    //}
}
