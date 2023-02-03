/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// not only holds the destrucion of the bullets BUT also
/// the damage dealing
/// </summary>
public class Shooting_Part4 : MonoBehaviour
{
    public HumanController_Part4 hc;

    int minDamage = 10;
    int maxDamage = 50;
    int randomBonus;

    private void Start()
    {
        randomBonus = Random.Range(1, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Zombie"))
        {
            if (hc.dmgDistance >= 16f)
            {
                other.gameObject.GetComponent<ZombieController_Part4>().TakeDamage(25 + randomBonus);
            }
            else if (hc.dmgDistance >= 3f)
            {
                other.gameObject.GetComponent<ZombieController_Part4>().TakeDamage(maxDamage + randomBonus);
            }
            else
            {
                other.gameObject.GetComponent<ZombieController_Part4>().TakeDamage(minDamage + randomBonus);
            }

            Destroy(gameObject, 2);
        }
    }
}

