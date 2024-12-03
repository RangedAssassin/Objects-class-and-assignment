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

    private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;

    private int nukeCount;
 

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
        
        StartCoroutine(SpawnWaveOfEnemies());
        SpawnEnemy();
    }

    private void GameOver()
    {
        OnGameOver.Invoke();
        StopAllCoroutines();
    }

    private Enemy SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPointsArray.Length);
        Transform randomSpawnPoint = spawnPointsArray[randomIndex];
        
        Enemy enemyClone = Instantiate(enemyprefabs[Random.Range(0,enemyprefabs.Length)], randomSpawnPoint.position, randomSpawnPoint.rotation);
        listOfAllEnemiesAlive.Add(enemyClone);
        return enemyClone;
        //enemyClone.healthValue.OnDied.AddListener(RemoveEnemyFromList);
    }

    public void RemoveEnemyFromList(Enemy enemyToBeRemoved)
    {
        scoreManager.IncreaseScore(ScoreType.EnemyKilled);
        listOfAllEnemiesAlive.Remove(enemyToBeRemoved);


        //for (int index = 0; index < listOfAllEnemiesAlive.Count; index++)
        //{
        //    if (listOfAllEnemiesAlive[index] == null)
        //    {
        //        listOfAllEnemiesAlive.RemoveAt(index);
        //    }
        //    //code here
        //}
    }


    private IEnumerator SpawnWaveOfEnemies()
    {
        //Do Something Here
        while (true)
        {
            if (listOfAllEnemiesAlive.Count < 5)//Enemies are less than 20
            {
                Enemy clone = SpawnEnemy();
                //yield return new WaitForEndOfFrame();
                //clone.healthValue.OnDied.AddListener(RemoveEnemyFromList);
            }
            
            //Debug.Log("start waiting for time");
            yield return new WaitForSeconds(Random.Range(1,4));

        }
        //Do Something Else here after Wait
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void IncreaseNukeCount()
    {
        nukeCount++;
        uiManager.UpdateNukeCount(nukeCount);
       
    }
    public void DestroyAllEnemiesOnList()
    {
        for (int i = listOfAllEnemiesAlive.Count - 1; i >= 0; i--)
        {
            Enemy enemy = listOfAllEnemiesAlive[i];
            if (enemy != null)
            {
                scoreManager.IncreaseScore(ScoreType.EnemyKilled);
                enemy.DropPickup();
                enemy.PlayDeadEffect();
                Destroy(enemy.gameObject);
            }
        }

        listOfAllEnemiesAlive.Clear();
    }


    //public void DestroyAllEnemiesOnList()
    //{
    //    foreach (Enemy enemy in listOfAllEnemiesAlive)
    //    {
    //        if (enemy != null)
    //        {
    //            scoreManager.IncreaseScore(ScoreType.EnemyKilled);
    //            enemy.DropPickup();
    //            enemy.PlayDeadEffect();
    //            Destroy(enemy.gameObject);
    //        }
    //    }
        
    //}

    public void UseNuke()
    {
        if (nukeCount >= 1f)
        {
            nukeCount--;
            uiManager.UpdateNukeCount(nukeCount);

            DestroyAllEnemiesOnList();
            listOfAllEnemiesAlive.Clear();


        }

    }
}
