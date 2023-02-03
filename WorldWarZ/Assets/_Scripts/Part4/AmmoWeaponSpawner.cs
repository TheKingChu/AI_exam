/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using UnityEngine;

public class AmmoWeaponSpawner : MonoBehaviour
{
    private float minTime = 10;
    private float maxTime = 15;
    private int minSpawn = 1;
    private int maxSpawn = 2;
    public Bounds spawnArea; //set these bounds in the inspector
    public GameObject ammoCrate, weaponCrate;

    private float spawnTimer = 0;

    private Vector3 randomWithinBounds(Bounds r)
    {
        return new Vector3(
            Random.Range(r.min.x, r.max.x),
            Random.Range(r.min.y, r.max.y),
            Random.Range(r.min.z, r.max.z));
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += Random.Range(minTime, maxTime);
            int randomSpawnCount = Random.Range(minSpawn, maxSpawn);
            for (int i = 0; i < randomSpawnCount; i++)
            {
                Instantiate(ammoCrate, randomWithinBounds(spawnArea), Quaternion.identity);
                Instantiate(weaponCrate, randomWithinBounds(spawnArea), Quaternion.identity);
            }
        }
    }
}
