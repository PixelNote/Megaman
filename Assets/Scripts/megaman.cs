using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megaman : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpSpeed2;
    [SerializeField] BoxCollider2D pies;
    [SerializeField] Sprite idleSprite;
    [SerializeField] Sprite fallingSprite;
    Animator myAnimator;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mover();
        saltar();
        
        
    }
    void mover() {
        float mov = Input.GetAxis("Horizontal");
        if (mov != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(mov), 1);
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
        transform.Translate(new Vector2(mov * speed * Time.deltaTime, 0));

    }

    void saltar() {
        bool isGrounded = pies.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGrounded)
        {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.Space)) {
                myAnimator.SetTrigger("takeof");
                myBody.AddForce(new Vector2(0, jumpSpeed));

            }
        } else if (isGrounded == false) {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                myAnimator.SetTrigger("jump2");
                myBody.AddForce(new Vector2(0, jumpSpeed2));

            }
            else {
                myAnimator.SetBool("falling", true);
            }
        }
        else {
            myAnimator.SetBool("falling", true);
        }

    }

    }

