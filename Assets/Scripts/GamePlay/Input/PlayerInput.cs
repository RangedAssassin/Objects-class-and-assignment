

using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject nukeEffect;
    [SerializeField] private AudioClip laserShootingSound;


    //For debug only
    public Vector2 direction;
    public Vector3 mousePosition;
    public Vector3 lookDirection;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        
        player.Move(direction);
        
        //get position of mouse
        mousePosition = Input.mousePosition;
        
        //offset position Z to account for -10 position of camera in calculation
        mousePosition.z = -Camera.main.transform.position.z;
        
        //convert mouse position to world position
        Vector3 destination = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = destination - transform.position;   //Destination  minus  origin position
        player.Look(lookDirection);

        if (Input.GetMouseButtonDown(0))
        {
            player.Attack();
            SoundManager.instance.PlaySound(laserShootingSound);
            //Start Shooting
        }

        if (Input.GetMouseButtonDown(1))
        {
            
            gameManager.UseNuke();
            Instantiate(nukeEffect, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        //Add Force Here???
    }
}
