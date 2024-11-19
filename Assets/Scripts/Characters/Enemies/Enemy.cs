using UnityEngine;

public class Enemy : Character
{   
    [SerializeField] protected float distanceToStop;
    [SerializeField] protected float attackCooldown;

    [SerializeField] protected float attackTimer;
    [SerializeField] protected Player target;

    protected override void Start()
    {   
        base.Start();
        target = GameObject.FindObjectOfType<Player>();
    
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
        GameManager.instance.RemoveEnemyFromList(this);
        base.PlayDeadEffect();  
    }
}
