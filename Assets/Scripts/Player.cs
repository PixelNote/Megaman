using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpSpeed2;
    [SerializeField] float dashSpeed;
    [SerializeField] float speed;
    [SerializeField] float fireDelay;
    [SerializeField] float dashDelay;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform fire;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] GameObject vfx_death;
    [SerializeField] GameObject EndScreen;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip landClip;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip dashClip;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    bool isPaused;

    float nextFire, nextDash;
    int willLand;




    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        EndScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Movement();
            Jump();
            Falling();
            Fire();
            Dash();
        }
        
    }

    private void Movement()
    {

        float mov = Input.GetAxis("Horizontal");

        if (mov > 0.1f)
        {
            myAnimator.SetBool("isRunning", true);
            transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));
            transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if (mov < -0.1f)
        {
            myAnimator.SetBool("isRunning", true);
            transform.Translate(new Vector2(-mov * speed * Time.deltaTime, 0));
            transform.eulerAngles = new Vector3(0, 180, 0);

        }

        if (mov == 0)
        {
            myAnimator.SetBool("isRunning", false);

        }


    }

    private void Dash()
    {

        if (Input.GetKeyDown(KeyCode.C) && Time.time >= nextDash)
        {
            myAnimator.SetTrigger("dash");
            myAnimator.SetBool("isRunning", false);
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isFalling", false);
            AudioSource.PlayClipAtPoint(dashClip, Camera.main.transform.position);
            if (transform.eulerAngles.y == 180)
            {
                myBody.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
                nextDash = Time.time + dashDelay;

            }
            else
            {
                myBody.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
                nextDash = Time.time + dashDelay;

            }

        }

    }

    private void Jump()
    {
        //bool isGrounded = pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGrounded() && !myAnimator.GetBool("isJumping"))
        {
            myAnimator.SetBool("isFalling", false);
            myAnimator.SetBool("isJumping", false);
            if (willLand == 1) {
                AudioSource.PlayClipAtPoint(landClip, Camera.main.transform.position);
                willLand--;
            }
            

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                myAnimator.SetTrigger("takeof");
                myAnimator.SetBool("isJumping", true);
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position);
                willLand++;
            }

        }
        else if (!isGrounded() && Input.GetKeyDown(KeyCode.UpArrow) && myAnimator.GetBool("isJumping"))
        {
            myAnimator.SetTrigger("takeof2");
            myBody.AddForce(new Vector2(0, jumpSpeed2), ForceMode2D.Impulse);
            AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position);
        }

    }

    void Falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("isJumping"))
            myAnimator.SetBool("isFalling", true);


    }

    private void Fire()
    {

        if (Input.GetKey(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);

            if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextFire)
            {
                Instantiate(bullet, fire.position, fire.rotation);
                AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position);
                nextFire = Time.time + fireDelay;

            }
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
            

        }
    }

   
    bool isGrounded()
    {
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.2f) * -1), Color.red);

        return myRaycast.collider != null;

    }

    void AfterJumpEvent()
    {
        myAnimator.SetBool("isJumping", false);
        myAnimator.SetBool("isFalling", true);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("BulletEnemy")) 
        {
            StartCoroutine(Die());       
        }
        
    }

    IEnumerator Die()
    {
        isPaused = true;
        myBody.isKinematic = true;
        myBody.Sleep();
        myAnimator.SetBool("isDeath", true);

        yield return new WaitForSeconds(1);
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position);
        Instantiate(vfx_death, transform.position, transform.rotation);
        Destroy(this.gameObject);
        EndScreen.SetActive(true);

    }
}
