using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExplodingEnemy : Enemy
{
    [SerializeField] private bool hasCollided = false;

    protected override void Update()
    {
        base.Update();
        if (hasCollided == true)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        Debug.Log("Ka-Boom i exploded");
        target.healthValue.DecreasedHealth(3);
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered");
            hasCollided = true;
        }
    }
}
