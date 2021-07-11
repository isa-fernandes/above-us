using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] LayerMask plataformLayerMask;
    [SerializeField] AudioClip explosionClip;

    CircleCollider2D circleCollider2D;
    AudioSource audioSource;
    Animator animator;
    bool isExploding;
    bool touchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() && !isExploding)
        {
            StartCoroutine(Explode());
        }
        animator.SetBool("Grounded", isGrounded());
        if (isExploding)
        {
            transform.localRotation = new Quaternion(0,0,0,0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !touchPlayer)
        {
            touchPlayer = true;
            StartCoroutine(Attack(collision.gameObject));
        }
    }

    IEnumerator Attack (GameObject player)
    {
        isExploding = true;
        animator.SetTrigger("Explode");
        audioSource.PlayOneShot(explosionClip);

        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        playerScript.TakeDamage();

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator Explode ()
    {
        isExploding = true;
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Explode");
        audioSource.PlayOneShot(explosionClip);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    bool isGrounded()
    {
        //raycast para detectar colisão com o solo
        float extraHeight = .3f;

        RaycastHit2D raycastHit = Physics2D.Raycast(circleCollider2D.bounds.center, Vector2.down, circleCollider2D.bounds.extents.y + extraHeight, plataformLayerMask);
        Color raycastColor;

        //detecção de colisão
        if (raycastHit.collider != null)
        {
            raycastColor = Color.green;

        }
        else
        {
            raycastColor = Color.red;
        }

        //caso queira testar o tamanho do raycast
        Debug.DrawRay(circleCollider2D.bounds.center, Vector2.down * (circleCollider2D.bounds.extents.y + extraHeight), raycastColor);

        return raycastHit.collider != null;
    }
}
