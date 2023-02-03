/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Part3 : MonoBehaviour
{
    public HumanController_Part3 hc;
    public Rigidbody bulletPrefab;

    //timer
    float waitTime;
    public float waitForSeconds = 1f;

    //float rotationSpeed = 2f;

    private void Start()
    {
        waitTime = Time.time + waitForSeconds;
    }

    public void WeaponHandler()
    {
        Shooting();
    }

    private void Shooting()
    {
        Rigidbody clone;

        if (Time.time > waitTime)
        {
            if(hc.currentAmmo > 0)
            {
                waitTime = Time.time + waitForSeconds;
                clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
                clone.velocity = transform.TransformDirection(Vector3.forward * 50);

                int ammo = hc.currentAmmo;
                hc.currentAmmo = ammo - 1;
            }
        }
    }
}
