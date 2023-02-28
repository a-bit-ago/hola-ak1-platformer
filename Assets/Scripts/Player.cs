using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _anim;

    private float _movementInputDirection;
    private int _amountOfJumpsLeft;
    private bool _isFacingRight = true;
    private bool _isGrounded;
    private bool _isWalking;
    private bool _canJump;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private int amountOfJumps = 1;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateAnimations();
        CheckMovementDirection();
        CheckIfCanJump();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    void UpdateAnimations()
    {
        _anim.SetBool(IsWalking, _isWalking);
        _anim.SetBool(IsGrounded, _isGrounded);
        _anim.SetFloat(YVelocity, _body.velocity.y);
    }
    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
    }

    void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y * variableJumpHeightMultiplier);
        }
    }

    void Jump()
    {
        if (_canJump)
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpForce);
            _amountOfJumpsLeft--;
        }
    }

    void ApplyMovement()
    {
        _body.velocity = new Vector2(runSpeed * _movementInputDirection, _body.velocity.y);
        
        if(_body.velocity.x != 0)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }
    }
    
    private void CheckIfCanJump()
    {
        // TODO: Missing wallcheck
        if(_isGrounded && _body.velocity.y <= 0)
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if(_amountOfJumpsLeft <= 0)
        {
            _canJump = false;
        }
        else
        {
            _canJump = true;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // Wall Gizmos
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
