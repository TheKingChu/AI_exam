/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class HumanController_Part3 : MonoBehaviour
{
    [SerializeField] SelectingHuman sh;

    public Weapon_Part3 _weapon;
    public BulletDestroy bd;
    [SerializeField] ZombieController_Part3 zc;
    [HideInInspector] public NavMeshAgent humanAgent;
    [HideInInspector] public Vector3 safeZone;
    public int thisHuman;

    //public ListManager lm;
    public List<Transform> zombies = new List<Transform>();

    //UI
    public GameObject canvas, ammoCanvas;
    public TextMeshProUGUI ammoUI, noAmmoUI, currentAmmoUI;

    //weapon variables
    public GameObject weapon;
    public float shootingDist = 17f;
    public int maxAmmo;
    public int currentAmmo;

    float fireRate;
    float shootDist;
    int ammo;
    int dmg;
    int randomAmmo;

    public float dmgDistance;

    // Start is called before the first frame update
    void Start()
    {

        ammo = Random.Range(10, 20);
        shootDist = shootingDist;
        currentAmmo = 20;
        maxAmmo = currentAmmo;
        randomAmmo = Random.Range(1, 5);


        this.transform.position = new Vector3(Random.Range(48, -48), 1, -47);
        humanAgent = GetComponent<NavMeshAgent>();
        safeZone = new Vector3(0, 1, 48);

        humanAgent.SetDestination(safeZone);
    }

    // Update is called once per frame
    void Update()
    {
        fireRate = Random.Range(0.1f, 3f);
        dmg = Random.Range(1, 10);
        shootDist = Random.Range(15, 20);
        

        ammoUI.text = currentAmmo.ToString();
        if(currentAmmo == 0)
        {
            noAmmoUI.enabled = true;
            currentAmmoUI.enabled = false;
        }
        else
        {
            currentAmmoUI.enabled = true;
            noAmmoUI.enabled = false;
        }

        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    humanAgent.SetDestination(hit.point);
        //}

        //if (humanAgent.remainingDistance < 1)
        //{
        //    humanAgent.SetDestination(safeZone);
        //}

        Shoot();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SafeZone")
        {
            canvas.SetActive(true);
        }

        if(other.tag == "Ammo")
        {
            GameObject.Destroy(other.gameObject);
            currentAmmo = currentAmmo + randomAmmo;

            if(currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }
        }

        if(other.tag == "Weapon")
        {
            _weapon.waitForSeconds = fireRate;
            bd.minDamage = dmg;
            shootingDist = shootDist;
            currentAmmo = ammo;

            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }
            GameObject.Destroy(other.gameObject);
        }
    }

    public void Shoot()
    {
        CheckDist();
        GetClosestEnemy(zombies);
    }

    //checks the distance from human to zombie so we can call this in the bullet script
    public void CheckDist()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(transform.position, Vector3.forward);

        if (Physics.Raycast(forwardRay, out hit, 50))
        {
            if(hit.transform.CompareTag("Zombie"))
            {
                dmgDistance = Vector3.Distance(transform.position, hit.point);
            }
        }
    }

    public Transform GetClosestEnemy(List<Transform> zombies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < zombies.Count; i++)
        {
            Vector3 directionToTarget = zombies[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = zombies[i];
            }
            float distance = Vector3.Distance(transform.position, bestTarget.transform.position);

            if (shootingDist > distance)
            {
                Vector3 relativPos = bestTarget.transform.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(relativPos, Vector3.up);
                transform.rotation = rot;

                _weapon.WeaponHandler();
            }
        }

        return bestTarget;
    }

    public void RemoveFromList()
    {
        for (int i = zombies.Count - 1; i >= 0; i--)
        {
            if (zombies[i].tag == "dead")
            {
                zombies.RemoveAt(i);
            }
        }
    }
}

