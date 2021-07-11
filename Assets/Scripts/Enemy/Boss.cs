using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject door;
    
    [SerializeField] AudioClip strangeDoorClip;

    AudioSource audioSource;
    Animator doorAnimator;
    bool isOpened;
    float nextTurn = 0f;
    float turnRate = 1f;
    // Start is called before the first frame update
    [SerializeField] int health;

    // Lifebar
    [SerializeField] GameObject lifeBar;
    [SerializeField] float maxLifeScaleX = 4;

    Animator animator;
    EnemyMove moveEnemyScript;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = door.GetComponentInChildren<Animator>();
        animator = GetComponentInChildren<Animator>();
        moveEnemyScript = GetComponent<EnemyMove>();
        lifeBar.transform.localScale = new Vector3(maxLifeScaleX, 1.5f, 1f);

    }

    public void UpdateLifebar (int health)
    {
        Debug.Log(health);
        lifeBar.transform.localScale = new Vector3((health * maxLifeScaleX) / 100, 1.5f, 1f);
    }

    public IEnumerator TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.SetBool("Death", true);
            StartCoroutine(Turn());

            yield return new WaitForSeconds(1);
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
                
            

        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public bool isAlive()
    {
        return health > 0;
    }

    public EnemyMove GetMoveEnemyScript()
    {
        return moveEnemyScript;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public IEnumerator Turn()
    {
        isOpened = !isOpened;
        Debug.Log(isOpened);
        if (isOpened)
        {
            door.GetComponent<AudioSource>().PlayOneShot(strangeDoorClip, 0.05f);
        }
        doorAnimator.SetBool("Open", isOpened);
        yield return new WaitForSeconds(1);
        door.SetActive(!isOpened);
    }

}
