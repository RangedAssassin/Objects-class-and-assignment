using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GlobalManager;
    public float timer;
    public GameStatus status;
    public Player playerObject;
    public Enemy enemyObject;

    void Start()
    {
        GlobalManager = this;

        if (status == GameStatus.Loading)
        {
            status = GameStatus.Playing;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
