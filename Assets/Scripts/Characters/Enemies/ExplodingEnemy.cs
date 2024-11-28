using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExplodingEnemy : Enemy
{
    [SerializeField] private bool hasCollided = false;
    [SerializeField] private float explosionDamage = 1f;

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
        target.healthValue.DecreasedHealth(explosionDamage);
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
