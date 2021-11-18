using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    [SerializeField] float life;
    [SerializeField] float fireDelay;
    [SerializeField] GameObject bullet2;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip deathClip;



    float nextFire;

    BoxCollider2D myCollider;
    SpriteRenderer myRender;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRender = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }


    bool isRanged()
    {
        RaycastHit2D myRaycast = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, myCollider.bounds.extents.x + 20, LayerMask.GetMask("Player"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2((myCollider.bounds.extents.x + 20), 0), Color.blue);

        return myRaycast.collider != null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (life > 1)
            {
                StartCoroutine(Damage());
                life--;
            }
            else
            {
                myAnimator.SetTrigger("isDeath");
                myCollider.isTrigger = true;
                AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position);


            }
        }

    }

    IEnumerator Damage()
    {

        myRender.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        myRender.color = Color.white;


    }



    private void Shoot()
    {
        if (isRanged())
        {

            if (Time.time >= nextFire)
            {
                Instantiate(bullet2, new Vector2(transform.position.x - 2, transform.position.y), transform.rotation);
                AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position);
                nextFire = Time.time + fireDelay;

            }
        }




    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }

}