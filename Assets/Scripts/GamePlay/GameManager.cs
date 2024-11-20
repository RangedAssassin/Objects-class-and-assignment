using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Enemy enemyprefab;
    [SerializeField] private Transform[] spawnPointsArray;
    [SerializeField] private List<Enemy> listOfAllEnemiesAlive;

    private ScoreManager scoreManager;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        scoreManager = GetComponent<ScoreManager>();
        
        StartCoroutine(SpawnWaveOfEnemies());
        SpawnEnemy();
    }

    private Enemy SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPointsArray.Length);
        Transform randomSpawnPoint = spawnPointsArray[randomIndex];
        
        Enemy enemyClone = Instantiate(enemyprefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
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
            if (listOfAllEnemiesAlive.Count < 20)//Enemies are less than 20
            {
                Enemy clone = SpawnEnemy();
                //yield return new WaitForEndOfFrame();
                //clone.healthValue.OnDied.AddListener(RemoveEnemyFromList);
            }
            
            Debug.Log("start waiting for time");
            yield return new WaitForSeconds(Random.Range(1,4));

        }
        //Do Something Else here after Wait
    }

}
