using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;
    private float _movementInputDirection;
    private bool _isFacingRight = true;
    
    [SerializeField] private float runSpeed = 8.0f;

    [SerializeField] private float jumpForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpForce);
        }
    }

    void ApplyMovement()
    {
        _body.velocity = new Vector2(runSpeed * _movementInputDirection, _body.velocity.y);
    }

    void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
        {
            Flip();
        }
        else if (_isFacingRight == false && _movementInputDirection > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    
    
}
