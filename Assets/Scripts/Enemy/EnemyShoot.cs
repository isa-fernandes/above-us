using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject shoot;
    [SerializeField] Transform initialPosition;
    [SerializeField] float force;
    [SerializeField] bool hasGravity;

    public void Shoot ()
    {
        GameObject newShoot = Instantiate(shoot, initialPosition);
        if (hasGravity)
        {
            Rigidbody2D bombRb = newShoot.GetComponent<Rigidbody2D>();
            Vector2 vectorForce = new Vector2(force * Mathf.Sign(gameObject.transform.localScale.x), 1);
            bombRb.AddForce(vectorForce, ForceMode2D.Impulse);
        } else
        {
            newShoot.GetComponent<ShootController>().sign = Mathf.Sign(gameObject.transform.localScale.x);
        }
    }

}
