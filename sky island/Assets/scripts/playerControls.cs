using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    private Animator anim;

    public float speed;

    Rigidbody2D rb;

    Vector2 moveVelocity;

    Vector2 moveInput;

    bool facingRight = true;

    public bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if(move)
        {
            if (moveInput.y == 1)
            {
                anim.SetInteger("walk", 1);
            }
            else if (moveInput.x == 1 || moveInput.x == -1)
            {
                if ((facingRight == false && moveInput.x == 1) || (facingRight == true && moveInput.x == -1))
                {
                    flip();
                }
                anim.SetInteger("walk", 3);
            }
            else if (moveInput.y == -1)
            {
                anim.SetInteger("walk", 2);
            }
            else if (moveInput.y == 0 && moveInput.x == 0)
            {
                anim.SetInteger("walk", 0);
            }
        }
       else
        {
            anim.SetInteger("walk", 0);
        }
    }

    public void FixedUpdate()
    {
        if(move)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            moveVelocity = moveInput * speed;

            rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        }
    }

    void flip()
    {
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;

        scaler.x *= -1;

        transform.localScale = scaler;
    }

    public void moveEnable(bool en)
    {
        move = en;
    }
}
