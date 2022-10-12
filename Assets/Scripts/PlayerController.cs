using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int cherry;
    public int jumpNum;

    public Text CherryNum;
    private bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Movement();
            //if(rb.velocity.y != 0)Debug.Log(rb.velocity.y.ToString());
        }
        SwitchAnim();

    }

    // 移动
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        // 角色移动
        if(horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(facedirection));

        }
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        // 角色跳跃
        if (Input.GetButtonDown("Jump") && jumpNum == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("Jumping", true);
            jumpNum++;
        }

        // 角色掉入虚空复位
        if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 1);
        }
    }

    // 角色动画切换
    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if(rb.velocity.y <= 0.5f && !coll.IsTouchingLayers(ground)) // 如果在掉落且没有碰到地面
        {
            anim.SetBool("Falling", true);
        }

        if(anim.GetBool("Jumping")) // 跳跃
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        else if(isHurt) // 受伤
        {
            anim.SetBool("Hurt", true);
            anim.SetFloat("Running", 0);
            if(Mathf.Abs(rb.velocity.x) < 0.1f || transform.position.y <-20)
            {
                anim.SetBool("Hurt", false);
                anim.SetBool("Idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground)) // 落地
        {
            jumpNum = 0;
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }
    }

    // 收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherry++;
            CherryNum.text = cherry.ToString();
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(anim.GetBool("Falling"))
            {
                enemy.JumpOn();
                jumpNum++;
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("Jumping", true);
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-15, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(15, rb.velocity.y);
                isHurt = true;
            }

        }
    }


}
