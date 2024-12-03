using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform weaponTip;
    [SerializeField] private LineRenderer lineRenderer;

    
    private Coroutine hideLineCoroutine;
    private bool isLineDrawn = false;
    

    protected override void Awake()
    {
        base.Awake();
        lineRenderer.enabled = false;

    }

    protected override void Update()
    {
        base.Update();
        if (IsTargetWithinRange())
        {
            DrawLineToPlayer();
            if (isLineDrawn)
            {
                Attack();
            }

        }
        else
        {
            ResetLine();
            
        }
    }

    private bool IsTargetWithinRange()
    {
        return target != null && Vector3.Distance(transform.position, target.transform.position) <= distanceToStop;
    }

    private void ResetLine()
    {
        if (lineRenderer.enabled && (target == null || Vector3.Distance(transform.position, target.transform.position) > distanceToStop))
        {
            lineRenderer.enabled = false;
            isLineDrawn = false;
            
            if (hideLineCoroutine != null)
            {
                StopCoroutine(hideLineCoroutine);
                hideLineCoroutine = null;
            }
        }
    }

    public void DrawLineToPlayer()
    {
        if (!IsTargetWithinRange()) return;
        
        lineRenderer.enabled = true;
        isLineDrawn = true;
        
        if (hideLineCoroutine != null) StopCoroutine(hideLineCoroutine);            
        
        hideLineCoroutine = StartCoroutine(TrackPlayerAndHideLine());
        
    }

    private IEnumerator TrackPlayerAndHideLine()
    {
        float timer = 0f;

        while (timer < attackCooldown)
        {
            if (IsTargetWithinRange())
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
            }
            else 
            {
                ResetLine();
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
        ResetLine();
    }

    public override void Attack()
    {
        if (!IsTargetWithinRange() || !isLineDrawn) return;
        
        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0;

            GameObject bullet = Instantiate(bulletPrefab, weaponTip.position, weaponTip.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.bulletSpeed = 20f;
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }
}
