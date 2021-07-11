using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{

    [SerializeField] Transform detectPlayerPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float detectPlayerRange = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(detectPlayerPoint.position, detectPlayerRange, playerLayer);
        if (player)
        {
            Attack(player);
        }


       
    }
    void Attack(Collider2D player)
    {



        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        playerScript.TakeDamage();

    }

    void OnDrawGizmos()
    {
        if (detectPlayerPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPlayerPoint.position, detectPlayerRange);
    }
}
