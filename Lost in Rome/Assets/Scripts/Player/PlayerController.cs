using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    public ContactFilter2D contactFilter;
    public float collisionOffset = 0.05f;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private Vector2 lastMoveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetFloat("Horizontal", Mathf.Abs(moveInput.x));
        anim.SetFloat("Vertical", moveInput.y);

        if (moveInput.x != 0)
        {
            sprite.flipX = moveInput.x < 0;
        }
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero && CanMove(moveInput))
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }

    private bool CanMove(Vector2 direction)
    {
       if (direction != Vector2.zero) 
       {
           int count = rb.Cast(
               direction, 
               contactFilter, 
               castCollisions, 
               speed * Time.fixedDeltaTime + collisionOffset 
           );

           if (count == 0) 
           {
                return true;
           }
       }
       return false;
    }
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            anim.SetBool("IsAttacking", true);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    void OnMove(InputValue moveValue) 
    {
        moveInput = moveValue.Get<Vector2>();
    }

}