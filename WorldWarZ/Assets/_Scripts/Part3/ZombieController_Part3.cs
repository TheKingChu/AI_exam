/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController_Part3 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform human;
    public HumanController_Part3 hc;
    State currentState;
    public GameObject canvas;
    public int health = 100;
    public GameObject ammoPrefab;

    public List<Transform> humans = new List<Transform>();

    //public ListManager lm;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new ZombieIdleState_Part3(this.gameObject, agent, human, canvas, health);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();

        if(health <= 0)
        {
            transform.gameObject.tag = "dead";
            hc.RemoveFromList();
            Destroy(this.gameObject);
            
            if(UnityEngine.Random.Range(1f, 10f) < 3)
            {
                Instantiate(ammoPrefab, transform.position, transform.rotation);
            }
        }
    }

    public void RemoveHFromList()
    {
        for (int i = humans.Count - 1; i >= 0; i--)
        {
            if (humans[i].tag == "Human")
            {
                humans.RemoveAt(i);
            }
        }
    }

    //zombie dies if it enters the safezone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            hc.RemoveFromList();
            Destroy(this.gameObject);
        }
    }

    //we call this method in the different scripts where we want to deal dmg to the zombie
    public void TakeDamage(int dmgAmount)
    {
        health -= dmgAmount;
    }
}
