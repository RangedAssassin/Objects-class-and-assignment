using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Enemy[] enemyprefabs;
    [SerializeField] private Transform[] spawnPointsArray;
    [SerializeField] public List<Enemy> listOfAllEnemiesAlive;
    [SerializeField] private AudioClip nukeExplosion;
    [SerializeField] private AudioClip noNukes;
    [SerializeField] private GameObject nukeEffect;
    public int NukeCount { get => nukeCount; private set => nukeCount = value; }


    [SerializeField] private int initialWaveSize = 4;
    [SerializeField] private float waveDelay = 2f;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int maxEnemiesPerWave = 30;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private float minSpawnDistance = 2f;
    [SerializeField] private float proximitySpawns = 1.5f;   
    private bool isSpawningWave = false;
    private List<Vector3> currentWaveSpawnedPositions = new List<Vector3>();

    public UnityEvent onWaveComplete;

    private ScoreManager scoreManager;
    
    [SerializeField] private UIManager uiManager;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;

    [SerializeField] private int nukeCount;
    private int maxNukes = 3;




    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        listOfAllEnemiesAlive = new List<Enemy>();

        scoreManager = GetComponent<ScoreManager>();
        //--------------------------------------------
        Player player = FindObjectOfType<Player>();
        if (player != null && player.healthValue != null)
        {
            player.healthValue.OnDied.AddListener(GameOver);
        }
        else
        {
            Debug.LogError("Player or healthValue is null.");
        }
        //----------------------------------------------
        FindObjectOfType<Player>().healthValue.OnDied.AddListener(GameOver);

        SpawnWaveOfEnemies();
    }

    private void Update()
    {
        if (!isSpawningWave && listOfAllEnemiesAlive.Count == 0)
        {
            onWaveComplete.Invoke();
            StartCoroutine("SpawnNextWave");
        }
    }

    private void GameOver()
    {
        OnGameOver.Invoke();
        StopAllCoroutines();
    }

    private void SpawnWaveOfEnemies()
    {
        currentWaveSpawnedPositions.Clear();

        int enemiesToSpawn = Mathf.Min(initialWaveSize + (currentWave - 1), maxEnemiesPerWave);
        int spawnedEnemies = 0;
        int maxSpawnAttemps = 100;
        int attemps = 0;

        while (spawnedEnemies < enemiesToSpawn && attemps < maxSpawnAttemps)
        { 
            attemps++;

            int randomIndex = Random.Range(0, spawnPointsArray.Length);
            Vector3 baseSpawnPosition = spawnPointsArray[randomIndex].position;
            
            Vector3 spawnPosition = GetRandomPositionWithinRadius(baseSpawnPosition,spawnRadius);

            if (IsSpawnPointValid(spawnPosition))
            {
                Enemy enemyClone = Instantiate(enemyprefabs[Random.Range(0, enemyprefabs.Length)], spawnPosition, Quaternion.identity);
                listOfAllEnemiesAlive.Add(enemyClone);
                uiManager.UpdateEnemiesAlive(listOfAllEnemiesAlive.Count);
                currentWaveSpawnedPositions.Add(spawnPosition);

                enemyClone.OnEnemyDeath.AddListener(() => RemoveEnemyFromList(enemyClone));
                spawnedEnemies++;
            }
        }

        if (spawnedEnemies < enemiesToSpawn)
        {
            Debug.LogWarning("Not all enemies could spawn due to limited spawn points.");
        }

        currentWave++;
    }

    private Vector3 GetRandomPositionWithinRadius(Vector3 center, float radius)
    {
        // Generate a random position within a circular area
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        return new Vector3(center.x + randomOffset.x, center.y + randomOffset.y, center.z);
    }
    private bool IsSpawnPointValid(Vector3 position)
    {
        // Ensure the position is far enough from all previously spawned positions
        foreach (Vector3 spawnedPosition in currentWaveSpawnedPositions)
        {
            if (Vector3.Distance(spawnedPosition, position) < minSpawnDistance)
            {
                return false; // Too close to an existing enemy
            }
        }

        return true; // Spawn point is valid
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform spawnPoint in spawnPointsArray)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, minSpawnDistance);
        }
        Gizmos.color = Color.blue;
        foreach (Vector3 item in currentWaveSpawnedPositions)
        {
            Gizmos.DrawWireSphere(item, proximitySpawns);
        }
        Gizmos.color = Color.yellow;
        foreach (Vector3 pos in currentWaveSpawnedPositions)
        {
            Gizmos.DrawSphere(pos, 0.2f); // Draw small spheres at enemy positions
        }
    }


    public void RemoveEnemyFromList(Enemy enemyToBeRemoved)
    {
        if (listOfAllEnemiesAlive.Contains(enemyToBeRemoved))
        {
            scoreManager.IncreaseScore(ScoreType.EnemyKilled);
            listOfAllEnemiesAlive.Remove(enemyToBeRemoved);

            uiManager.UpdateEnemiesAlive(listOfAllEnemiesAlive.Count);
        }
    }

    private IEnumerator SpawnNextWave()
    {
        isSpawningWave = true;
        uiManager.WaveInfo.SetActive(true);
        float remainingTime = waveDelay;
        while (remainingTime > 0f)
        {
            int displayTime = Mathf.CeilToInt(remainingTime);
            // Update UI with the remaining time
            uiManager.UpdateWaveText(currentWave, displayTime, listOfAllEnemiesAlive.Count);

            // Decrease the remaining time
            remainingTime -= Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }
        //yield return new WaitForSeconds(waveDelay);

        SpawnWaveOfEnemies();
        isSpawningWave= false;
        uiManager.WaveInfo.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void IncreaseNukeCount()
    {
            nukeCount++;
            nukeCount = Mathf.Clamp(nukeCount, 0, maxNukes);
            uiManager.UpdateNukeCount(nukeCount);
    }
    public void DestroyAllEnemiesOnList()
    {
        // Iterate backward through the list to avoid issues when removing items
        for (int i = listOfAllEnemiesAlive.Count - 1; i >= 0; i--)
        {
            Enemy enemy = listOfAllEnemiesAlive[i];
            if (enemy != null)
            {
                enemy.PlayDeadEffect(); // Properly invoke the death logic
            }
        }

        listOfAllEnemiesAlive.Clear(); // Clear the list after all enemies are processed
    }



    public void UseNuke(Character player)
    {
        if (nukeCount >= 1f)
        {
            nukeCount--;
            uiManager.UpdateNukeCount(nukeCount);
            SoundManager.instance.PlaySound(nukeExplosion);
            Instantiate(nukeEffect, player.transform.position, player.transform.rotation);
            DestroyAllEnemiesOnList();
        }
        else
        {
            SoundManager.instance.PlaySound(noNukes);
        }
    }

    public void OnMainMenuReturnClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
