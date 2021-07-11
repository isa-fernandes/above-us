using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrampoline : MonoBehaviour
{
    [SerializeField] float jumpSpeed;

    [SerializeField] Transform detectPlayerPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectPlayerRange = 0.2f;

    [SerializeField] AudioClip playerImpulseClip;

    AudioSource audioSource;

    EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager != null && enemyManager.isAlive())
        {
            Collider2D player = Physics2D.OverlapCircle(detectPlayerPoint.position, detectPlayerRange, playerLayer);
            if (player)
            {
                if (enemyManager.GetMoveEnemyScript().isStopped() && !player.GetComponent<PlayerScript>().isGrounded())
                {
                    Trampoline(player);
                }
            }
        }
    }

    void Trampoline (Collider2D player)
    {
        audioSource.PlayOneShot(playerImpulseClip);
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        if (detectPlayerPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPlayerPoint.position, detectPlayerRange);
    }
}
