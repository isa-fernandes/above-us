using System;
using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float jump = 10;
    [SerializeField] private LayerMask plataformLayerMask;
    //dash variables
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] int startDashTime;
    [SerializeField] int direction;

    [SerializeField] AudioClip playerJumpClip;
    [SerializeField] AudioClip playerHit;
    

    [SerializeField] GameObject dashDust;

    GameOver gameOver;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2d;
    private Animator anim;
    private Vector3 lastPos;
    public Transform spawnPoint;

    public event Action HitByEnemy;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dashTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pega a posição do frame atual
        lastPos = rb.transform.position;
        
        isGrounded();
        wallHit();
        
        if(dashTime <= 0)
        {
            dashTime = 0;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift) && dashTime <= 0 ){
            
            StartCoroutine(Dash());
            dashTime = 2;
        }
        
          
        

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {

            Run();

        }
        else
        {
            anim.SetBool("Running", false);
        }
        //pular do player
        if ((Input.GetKey(KeyCode.Space)))
        {

            if (isGrounded() == true)
            {
                Jump();
            }

        }
        if (isGrounded() == false)
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("IsGrounded", false);
        }
        if (isGrounded() == true)
        {
            idle();
            anim.SetBool("Jumping", false);
            anim.SetBool("IsGrounded", true);
        }
        
    }
    //metodos do player
    //metodo para ativar o idle
    private void idle()
    {
        
        anim.SetBool("Idle", true);
    }


    private IEnumerator Dash() {

        dashDust.SetActive(true);
            
        float dashDistance = 10f;


        if (transform.eulerAngles.y.Equals(0))
        {
            //transform.position += Vector3.right * dashDistance * Time.deltaTime;
            rb.velocity = Vector2.right * dashDistance;
        }
        else if (transform.eulerAngles.y.Equals(180))
        {
            //transform.position += Vector3.left * dashDistance * Time.deltaTime;
            rb.velocity = Vector2.left * dashDistance;
        }

        yield return new WaitForSeconds(0.4f);
        dashDust.SetActive(false);
    }
    //metodo para ativar a corrida
    private void Run()
    {

        anim.SetBool("Running", true);

        anim.SetBool("Idle", false);

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);

        }

        
    }
    //metodo para ativar o ataque (não está sendo usado no momento
    private void Attack()
    {
        anim.SetBool("Attacking", true);
        

    }
    //metodo do pulo
    private void Jump()
    {
        audioSource.PlayOneShot(playerJumpClip);

        anim.SetBool("Jumping",true);
        anim.SetBool("Running", false);
        anim.SetBool("Idle", false);

        rb.velocity = Vector2.up * jump;

        if (isGrounded())
        {
            anim.SetBool("Jumping", false);

        }

    }

    public void TakeDamage()
    {
        audioSource.PlayOneShot(playerHit, 0.5f);

        HitByEnemy();
    }

    //metodo para detectar colisão com o solo
    public bool isGrounded()
    {
        //raycast para detectar colisão com o solo
        float extraHeight = 0.3f;

        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeight, plataformLayerMask);
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
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), raycastColor);

        return raycastHit.collider != null;
    }

    public bool wallHit()
    {
        float extraHeight = 0.5f;
        if (transform.eulerAngles.y.Equals(0))
        
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.right, boxCollider2d.bounds.extents.x + extraHeight, plataformLayerMask);
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
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.right * (boxCollider2d.bounds.extents.x + extraHeight), raycastColor);

            return raycastHit.collider != null;
        }
        else
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.left, boxCollider2d.bounds.extents.x + extraHeight, plataformLayerMask);
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
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.left * (boxCollider2d.bounds.extents.x + extraHeight), raycastColor);

            return raycastHit.collider != null;
        }
        
        
    }
    
}
