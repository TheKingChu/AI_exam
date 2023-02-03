/* Made by
 * Charlie Eik�s &  Heimir Sindri �orl�ksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController_Part2 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform human;
    State currentState;
    public GameObject canvas;
    int health = 100;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new ZombieIdleState(this.gameObject, agent, human, canvas, health);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }

    //zombie dies if it enters the safezone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            Destroy(this);
        }
    }
}
