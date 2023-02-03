/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController_Part4 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform human1,human2,human3,human4;
    State_Part4 currentState;
    public GameObject canvas;
    public int health = 100;
    public GameObject ammoPrefab;


    public HumanController_Part4 hc;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new ZombieIdleState_Part4(this.gameObject, agent, human1, human2, human3, human4, canvas, health);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();

        if (health <= 0)
        {
            if (transform.gameObject.tag == "dead")
            {
                RemoveFromList();
                Destroy(this.gameObject);
            }

            if (UnityEngine.Random.Range(1f, 10f) < 3)
            {
                //Instantiate(ammoPrefab, transform.position, transform.rotation);
            }
        }
    }

    void RemoveFromList()
    {
        for (int i = hc.zombie.Count - 1; i >= 0; i--)
        {
            if (hc.zombie[i].tag == "dead")
            {
                hc.zombie.RemoveAt(i);
                Destroy(this.gameObject);
            }
        }
    }

    //zombie dies if it enters the safezone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            Destroy(this.gameObject);
        }
    }

    //we call this method in the different scripts where we want to deal dmg to the zombie
    public void TakeDamage(int dmgAmount)
    {
        health -= dmgAmount;
    }
}

