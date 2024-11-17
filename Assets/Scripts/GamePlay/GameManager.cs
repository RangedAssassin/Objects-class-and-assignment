using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Enemy enemyprefab;
    [SerializeField] private Transform[] spawnPointsArray;
    [SerializeField] private List<Enemy> listOfAllEnemiesAlive;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

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

        while (true)
        {
            if (listOfAllEnemiesAlive.Count < 20)//change number to spawn enemies until number is reached.
            {
                Enemy clone = SpawnEnemy();
                //yield return new WaitForEndOfFrame();
                //clone.healthValue.OnDied.AddListener(RemoveEnemyFromList);
            }
            Debug.Log("start waiting for time");
            yield return new WaitForSeconds(5);

        }

    }

}
