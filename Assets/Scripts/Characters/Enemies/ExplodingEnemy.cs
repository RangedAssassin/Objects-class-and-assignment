using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExplodingEnemy : Enemy
{
    [SerializeField] private bool hasCollided = false;
    [SerializeField] private float explosionDamage = 1f;
    [SerializeField] private GameObject detonateEffect;

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
        Instantiate(detonateEffect,transform.position,transform.rotation);
        SoundManager.instance.PlaySound(explosionClip);
        target.healthValue.DecreasedHealth(explosionDamage);
        gameManager.RemoveEnemyFromList(this);
        OnEnemyDeath.Invoke();//NEW
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Triggered");
            hasCollided = true;
        }
    }
}
