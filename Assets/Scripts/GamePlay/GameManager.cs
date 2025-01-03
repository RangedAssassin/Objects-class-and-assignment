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
    [SerializeField] private List<Enemy> listOfAllEnemiesAlive;
    [SerializeField] private AudioClip nukeExplosion;
    [SerializeField] private AudioClip noNukes;
    [SerializeField] private GameObject nukeEffect;


    [SerializeField] private int initialWaveSize = 4;
    [SerializeField] private float waveDelay = 2f;
    [SerializeField] private int currentWave = 1;
    
    public UnityEvent onWaveComplete;

    private ScoreManager scoreManager;
    
    [SerializeField] private UIManager uiManager;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;

    [SerializeField] private int nukeCount;
    private int maxNukes = 3;

    public int NukeCount { get => nukeCount; private set => nukeCount = value; }

    private bool isSpawningWave = false;
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
        int enemiesToSpawn = initialWaveSize + (currentWave - 1);

        for (int i = 0; i < enemiesToSpawn; i++)
        { 
            int randomIndex = Random.Range(0, spawnPointsArray.Length);
            Transform randomSpawnPoint = spawnPointsArray[randomIndex];

            Enemy enemyClone = Instantiate(enemyprefabs[Random.Range(0, enemyprefabs.Length)], randomSpawnPoint.position, randomSpawnPoint.rotation);
            listOfAllEnemiesAlive.Add(enemyClone);

            enemyClone.OnEnemyDeath.AddListener(() => RemoveEnemyFromList(enemyClone));
        }

        currentWave++;
    }

    public void RemoveEnemyFromList(Enemy enemyToBeRemoved)
    {
        if (listOfAllEnemiesAlive.Contains(enemyToBeRemoved))
        {
            scoreManager.IncreaseScore(ScoreType.EnemyKilled);
            listOfAllEnemiesAlive.Remove(enemyToBeRemoved);
        }
    }

    private IEnumerator SpawnNextWave()
    {
        isSpawningWave = true;
        yield return new WaitForSeconds(waveDelay);

        SpawnWaveOfEnemies();
        isSpawningWave= false;
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
