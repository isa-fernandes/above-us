using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] GameObject leftLever;
    [SerializeField] GameObject rightLever;
    [SerializeField] GameObject door;

    [SerializeField] AudioClip leverClip;
    [SerializeField] AudioClip strangeDoorClip;

    AudioSource audioSource;
    Animator doorAnimator;
    bool isOpened;
    float nextTurn = 0f;
    float turnRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = door.GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Turn ()
    {
        audioSource.PlayOneShot(leverClip);
        isOpened = !isOpened;
        if (isOpened)
        {
            door.GetComponent<AudioSource>().PlayOneShot(strangeDoorClip, 0.05f);
        }
        rightLever.SetActive(isOpened);
        leftLever.SetActive(!isOpened);
        doorAnimator.SetBool("Open", isOpened);
        yield return new WaitForSeconds(1);
        door.SetActive(!isOpened);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextTurn)
        {
            StartCoroutine(Turn());
            nextTurn = Time.time + 1f / turnRate;
        }
    }
}
