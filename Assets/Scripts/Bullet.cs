using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    

    Rigidbody2D myRb;
    Animator myAnimator;



    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        myRb.velocity = transform.right * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       myAnimator.SetTrigger("isDestroyed");
    }


    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }





    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }











}


