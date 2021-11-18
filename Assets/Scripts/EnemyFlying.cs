using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFlying : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] float life;
    [SerializeField] AudioClip deathClip;

    Animator myAnimator;
    SpriteRenderer myRender;
    CircleCollider2D myCollider;

    IAstarAI ai;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRender = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<CircleCollider2D>();
        ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Debug.Log(life);
        
        
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
            else {
                myCollider.isTrigger = true;
                ai.isStopped = true;
                myAnimator.SetTrigger("isDeath");
                AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position);

            }
        }

    }



    IEnumerator Damage() {

        myRender.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        myRender.color = Color.white;


    }
    private void Movement()
    {
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player")) != null)
        {
            ai.isStopped = false;
            if (life <= 1) {

                ai.isStopped = true;
            }


        }
        else
        {
            ai.isStopped = true;



        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f,0,0,0.45f);
        Gizmos.DrawSphere(transform.position, range);
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
