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
    public static PlayerController Instance { get; private set; }
    public bool CanMove { get; set; } = true;
    private Vector2 lastMoveDirection;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(!CanMove) return;
        anim.SetFloat("Horizontal", Mathf.Abs(moveInput.x));
        anim.SetFloat("Vertical", moveInput.y);

        if (moveInput.x != 0)
        {
            sprite.flipX = moveInput.x < 0;
        }
    }

    private void FixedUpdate()
    {
        if (!CanMove || moveInput == Vector2.zero) return;

        if (KanMove(moveInput))
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }

    private bool KanMove(Vector2 direction)
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
           if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
           {
               anim.SetTrigger(moveInput.y > 0 ? "AttackUp" : "AttackDown");
           }
           else
           {
               anim.SetTrigger("AttackHorizontal");
           }
       }
    }

    void OnMove(InputValue moveValue) 
    {
        moveInput = moveValue.Get<Vector2>();
    }

}