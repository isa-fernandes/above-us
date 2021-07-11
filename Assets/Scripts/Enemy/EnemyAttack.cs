using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] bool hasAttack;
    [SerializeField] bool hasShoot;

    // Detect player
    [SerializeField] Transform detectPlayerPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectPlayerRange = 0.2f;

    // Control attack
    [SerializeField] float attackRate = 1.4f;
    private float nextAttack = 0f;

    EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager != null && enemyManager.isAlive())
        {
            Collider2D player = Physics2D.OverlapCircle(detectPlayerPoint.position, detectPlayerRange, playerLayer);
            if (player)
            {
                if (enemyManager.GetMoveEnemyScript() != null)
                {
                    enemyManager.GetMoveEnemyScript().Stop();
                }
                if (Time.time >= nextAttack)
                {

                    Attack(player);
                    nextAttack = Time.time + 1f / attackRate;
                } else
                {
                    if (hasAttack)
                    {
                        enemyManager.GetAnimator().SetBool("Attack", false);
                    }
                }

            }
            else
            {
                if (hasAttack)
                {
                    enemyManager.GetAnimator().SetBool("Attack", false);
                }
                if (enemyManager.GetMoveEnemyScript() != null)
                {
                    enemyManager.GetMoveEnemyScript().Continue();
                }
            }
        }
    }

    void Attack(Collider2D player)
    {
        if (hasAttack)
        {
            enemyManager.GetAnimator().SetBool("Attack", true);
        }
        if (hasShoot)
        {
            EnemyShoot enemyShoot = GetComponent<EnemyShoot>();
            enemyShoot.Shoot();
        } else
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.TakeDamage();
        }
    }

    void OnDrawGizmos()
    {
        if (detectPlayerPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPlayerPoint.position, detectPlayerRange);
    }
}
