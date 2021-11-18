using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] int life;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] GameObject bullet3L;
    [SerializeField] GameObject bullet3R;


    Animator myAnimator;
    SpriteRenderer myRender;
    BoxCollider2D myCollider;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRender = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Range();
    }


    private void Range()
    {
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            myAnimator.SetBool("isRanged",true);
            Debug.Log("Estan en rango");
            Shoot();

        }
        else {
            myAnimator.SetBool("isRanged", false);
        } 


        
    }

    private void Shoot()
    {
        if (Time.time >= nextFire)
        {
            Instantiate(bullet3L, new Vector2(transform.position.x + 1, transform.position.y+1), transform.rotation);
            Instantiate(bullet3R, new Vector2(transform.position.x - 1, transform.position.y + 1), transform.rotation);
            AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position);
            nextFire = Time.time + 2;

        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.45f);
        Gizmos.DrawSphere(transform.position, range);
    }


    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
