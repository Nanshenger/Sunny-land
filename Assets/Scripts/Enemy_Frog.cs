using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform leftPoint, rightPoint;
    private Animator anim;
    private Collider2D coll;
    public LayerMask ground;
    private float leftX, rightX;
    private bool faceLeft = true;
    public float speed, jumpForce;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if(faceLeft) // 面向左侧
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }

            if(transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else // 面向右侧
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }

            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }

        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

}
