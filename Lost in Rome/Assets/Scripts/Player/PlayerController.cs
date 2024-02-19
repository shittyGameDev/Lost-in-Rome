using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);

        bool isRunning = moveInput.x != 0;
        anim.SetBool("IsRunning", isRunning);

        sprite.flipX = moveInput.x < 0;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}