using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_slide_down : MonoBehaviour
{
    
    bool has_attacked;
    PlayerScript player_link;
    public Animator anim;
    public Collider2D col;

    [SerializeField] int damage;

    // Detect player
    [SerializeField] Transform detectPlayerPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectPlayerRange = 0.2f;

    [SerializeField] float attackRate = 1.4f;

    private float nextAttack = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player_link = GameObject.FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (has_attacked)
        {

            if (Time.time >= nextAttack)
            {
                nextAttack = Time.time + 1f / attackRate;
            }

        }

    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,playerLayer);

        if (hit.collider.gameObject.tag == "Player")
        {
            anim.SetTrigger("actived");
            col.enabled = true;

        }
    }

    public void Attack(GameObject player)
    {
        has_attacked = true;

        player.GetComponent<PlayerScript>().TakeDamage();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attack(collision.gameObject);
        }
    }
    
}

