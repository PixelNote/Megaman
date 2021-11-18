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
        myRb.velocity = transform.right * speed;


    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       myAnimator.SetTrigger("isDestroyed");
       myRb.isKinematic = true;
       myRb.Sleep();
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


