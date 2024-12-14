using UnityEngine;

public class Enemy : Character
{   
    [SerializeField] protected float distanceToStop;
    [SerializeField] protected float attackCooldown;

    [SerializeField] protected float attackTimer;
    [SerializeField] protected Player target;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected AudioClip enemyShootSound;

    [SerializeField] protected GameObject[] pickups;
    [SerializeField][Range(0, 100)] public float dropChance = 100f;


    protected override void Awake()
    {   
        base.Awake();
        target = GameObject.FindObjectOfType<Player>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    protected virtual void Update()
    {
        if (!target) return;
       
        Vector2 destination = target.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 directionClass = destination - currentPosition;
        
        if (Vector2.Distance(destination, currentPosition) > distanceToStop)
        {
            Move(directionClass.normalized);
        }
        else
        {
            Attack();
        }
        
        Look(directionClass.normalized);
    }

    public override void Attack()
    {   // Attack method to be overridden by subclasses
        base.Attack();
        
        if (attackTimer >= attackCooldown)
        {
            //Debug.Log("Attacking!");
            target.healthValue.DecreasedHealth(1);
            attackTimer = 0;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }

    }

    public override void PlayDeadEffect()
    {
        //Another Solution without GameManager
        //FindObjectOfType<ScoreManager>().IncreaseScore(ScoreType.EnemyKilled);
        GameManager.instance.RemoveEnemyFromList(this);
        DropPickup();
        base.PlayDeadEffect();  
    }

    public void DropPickup()
    {
        if (Random.Range(0f, 100f) <= dropChance)
        {
            int randomIndex = Random.Range(0, pickups.Length);
            Instantiate(pickups[randomIndex], transform.position, Quaternion.identity);
        }
    }

}
