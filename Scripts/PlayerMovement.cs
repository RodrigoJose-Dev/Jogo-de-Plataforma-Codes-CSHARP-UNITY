using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
    public float speed;
    public float jumpForce;
        
    private Animator anim;
    private Vector3 move;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    //groundcheck
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    public Joystick joystick;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //verificando groundcheck
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        
    }

    private void Update()
    {

        move = new Vector3(joystick.Horizontal, 0, 0); //movimentação do jogador no joystick
        transform.position += move * speed * Time.deltaTime;

        Flip();
        Animations();
    }

    
    public void Jump()
    {
        if (isGrounded) //se estiver no chão, o pulo é habilitado
        {
            audioSource.Play();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
    }

    void Flip()
    {
        if (move.x < 0)
        {
            sr.flipX = true;
        }
        else if (move.x > 0)
        {
            sr.flipX = false;
        }
    }

    void Animations()
    {
        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else if (!isGrounded)
        {
            anim.SetBool("isJumping", true);
        }

        if (move.x > 0f || move.x < 0f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

}
