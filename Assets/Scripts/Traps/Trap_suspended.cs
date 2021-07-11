using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_suspended : MonoBehaviour
{
    bool has_attacked;
    private PlayerScript player_link;

    [SerializeField] int damage;

    [SerializeField] float attackRate = 1.4f;

    private float nextAttack = 0f;

    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_link ==  null) {
            player_link = FindObjectOfType<PlayerScript>();
        }
        if (has_attacked)
        {

            if (Time.time >= nextAttack)
            {
                nextAttack = Time.time + 1f / attackRate;
            }

        }

    }

    public void Attack()
    {
        has_attacked = true;

        player_link.TakeDamage();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attack();
        }
    }


}
