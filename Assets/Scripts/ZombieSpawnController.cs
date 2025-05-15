using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiePerWave = 5; // Number of zombies to spawn in the first wave
    public int currentZombiePerWave; // Number of zombies to spawn in the current wave
    public float spawnInterval = 5f; // Time interval between spawns
    public int currentWave = 0; // Current wave number
    public int waveCoolDown = 10; // Time to wait before starting the next wave
    public bool isCoolDown;
    public float collDownCounter = 0f; // Cooldown counter
    public List<Zombie> currentZombiesAlive;
    public GameObject zombiePrefab; // Prefab of the zombie to spawn
 

    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;
        currentZombiesAlive = new List<Zombie>();
        isCoolDown = false;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiePerWave; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)); // Random spawn offset
            Vector3 spawnPosition = transform.position + randomOffset; // Spawn position relative to the spawner

            GameObject zombieObject = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity); // Instantiate the zombie
            Zombie zombieScript = zombieObject.GetComponent<Zombie>(); // Get the Zombie script
            currentZombiesAlive.Add(zombieScript); // Add to alive list

            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next zombie
        }
    }

    private void Update()
    {
        List<Zombie> zombiesToRemove = new List<Zombie>();

        foreach (Zombie zombie in currentZombiesAlive)
        {
            if (zombie == null || zombie.HP<=0) // Check if the zombie is dead or null
            {
                zombiesToRemove.Add(zombie); // Add to the list of zombies to remove
            }
        }

        foreach (Zombie zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie); // Remove the dead or null zombie from the list
        }

        zombiesToRemove.Clear(); // Clear the list for the next frame

        if (currentZombiesAlive.Count == 0 && !isCoolDown) // Check if all zombies are dead and not in cooldown
        {
            StartCoroutine(WaveCoolDown()); // Start the cooldown for the next wave
        }

        if (isCoolDown) // Check if in cooldown
        {
            collDownCounter -= Time.deltaTime; // Decrease the cooldown counter
        }
        else
        {
            collDownCounter = waveCoolDown; // Reset the cooldown counter
        }
    }

    private IEnumerator WaveCoolDown()
    {
        isCoolDown = true; // Set cooldown to true
        yield return new WaitForSeconds(waveCoolDown); // Wait for the cooldown time
        isCoolDown = false; // Set cooldown to false
        currentZombiePerWave += 10; // Increase the number of zombies for the next wave
        StartNextWave(); // Start the next wave
    }
}
