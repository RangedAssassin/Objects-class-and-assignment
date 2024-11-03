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

        playerObject = new Player(); /*playerObject = new Health();*/ //new" creates a new instance of the "blueprint" code
        enemyObject = new Enemy(10,5,1);

        playerObject.Move();

        enemyObject.Move();


        playerObject.currentWeapon = new Weapon();
        playerObject.Interact();

    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
