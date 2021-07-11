using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] bool isBoss = false;

    Animator animator;
    EnemyMove moveEnemyScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        moveEnemyScript = GetComponent<EnemyMove>();
       
        
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        if (isBoss)
        {
            gameObject.GetComponent<Boss>().UpdateLifebar(health);
        }
        if (health <= 0)
        {
            animator.SetBool("Death", true);
            if (isBoss)
            {
                StartCoroutine(gameObject.GetComponent<Boss>().Turn());
            }
            
            yield return new WaitForSeconds(1);
            if (gameObject != null)
                Destroy(gameObject);

        } else
        {
            animator.SetTrigger("Hit");
        }
    }

    public bool isAlive ()
    {
        return health > 0;
    }

    public EnemyMove GetMoveEnemyScript ()
    {
        return moveEnemyScript;
    }

    public Animator GetAnimator ()
    {
        return animator;
    }

}
