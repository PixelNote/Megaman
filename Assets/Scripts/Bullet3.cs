using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{

    [SerializeField] float speedX;
    [SerializeField] float speedY;

    Animator myAnimator;
    Rigidbody2D myRb;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        myRb.velocity = new Vector2(speedX, speedY) ;
       
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

}
