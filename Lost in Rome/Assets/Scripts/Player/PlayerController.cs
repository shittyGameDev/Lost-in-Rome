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
    public static PlayerController Instance { get; private set; } //singleton instans av scriptet
    public bool CanMove { get; set; } = true;
    private Vector2 lastMoveDirection;

    void Awake()
    {
        //s�kerhetsst�ller att de endast finns en instans av scriptet
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

        //om spelaren kan flytta p� sig s� g�rs det
        if (KanMove(moveInput))
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }
    //kontrollerar om spelaren kan r�ra sig i en given riktning baserat p� kollisioner
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
    //hanterar spelarens attackhandling baserat p� inputen 
    public void OnAttack(InputValue value)
    {
       if (value.isPressed)
       {
            //v�ljer animation baserat p� r�relseriktning
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

    public void EnableMovement()
    {
        CanMove = true;
    }

    public void DisableMovement()
    {
        CanMove = false; moveInput = Vector2.zero;
        anim.SetFloat("Horizontal", 0f);
        anim.SetFloat("Vertical", 0f);
    }
    //uppdaterar moveInput baserat p� spelarens r�relse input
    void OnMove(InputValue moveValue) 
    {
        moveInput = moveValue.Get<Vector2>();
    }

}