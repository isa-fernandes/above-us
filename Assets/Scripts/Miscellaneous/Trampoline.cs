using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float force;

    [SerializeField] AudioClip playerImpulseClip;

    [SerializeField] bool isHorizontal;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(playerImpulseClip);
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (isHorizontal)
        {
            playerRb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        } else
        {
            playerRb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        

    }
}
