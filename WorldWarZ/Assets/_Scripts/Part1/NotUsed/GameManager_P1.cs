/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores info of human positions for the zombies to use
/// </summary>
public class GameManager_P1 : MonoBehaviour
{
    //list that will hold all the humans from the humans array
    public List<Transform> humanTransforms = new List<Transform>();
    public List<float> distances = new List<float>(); //list of different distances between zombie and human
    public Transform[] humans; //transform of humans in scene

    public FSM stateMachine;
    float dist;
    public int thisHuman;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.distance = dist;

        //here we add the transforms of the humans to the list so we can access them later
        humanTransforms.AddRange(humans);
    }

    private void Update()
    {
        //HumanDistance();
    }

    public void HumanDistance()
    {
        thisHuman = 0;
        while (thisHuman < humans.Length)
        {
            if (dist < 10)
            {
                stateMachine.currentState = FSM.States.Chase;
            }
            else
            {
                dist = Vector3.Distance(stateMachine.zombieTransform.position, humans[thisHuman].position);
                distances.Add(dist);
                distances[thisHuman] = dist;
                thisHuman++;
            }
        }
        //for (int i = 0; i < humans.Length; i++)
        //{
        //    //getting distance between the zombie and human
        //    dist = Vector3.Distance(stateMachine.zombieTransform.position, humans[i].position);
        //    distances.Add(dist);
        //    distances[i] = dist;
        //}

        distances.Sort();

        foreach(float dist in distances)
        {
            if (dist < 10)
            {
                stateMachine.currentState = FSM.States.Chase;
            }
        }
    }
}
