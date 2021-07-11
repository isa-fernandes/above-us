using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    
    public int damage;
    public float attackRange = 0.5f;
    public float attackRate =1f;
    public float nextAttack = 0f;

    public Transform attackPoint;
    public LayerMask enemyLayer;
    private Animator anim;

    [SerializeField] AudioClip playerAttack;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(Time.time >= nextAttack)
        {

        
            if (Input.GetKey(KeyCode.Mouse0))
            {
                audioSource.PlayOneShot(playerAttack);

                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }

    }
    void Attack()
    {
       

        anim.SetTrigger("Attacking 0");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            StartCoroutine(enemy.GetComponent<EnemyManager>().TakeDamage(damage));
            

            Debug.Log("hit");
        }
    }
    void OnDrawGizmos()
    {
            if (attackPoint == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
