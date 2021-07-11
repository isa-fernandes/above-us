using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [HideInInspector] public float sign;

    [SerializeField] float speed;

    bool touchPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * sign * Time.deltaTime * Vector3.right);

        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !touchPlayer)
        {
            touchPlayer = true;
            Attack(collision.gameObject);
        }
    }

    void Attack(GameObject player)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        playerScript.TakeDamage();
        Destroy(gameObject);
    }
}
