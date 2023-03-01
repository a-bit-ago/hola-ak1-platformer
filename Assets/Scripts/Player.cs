using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _anim;

    private float _movementInputDirection;
    private float _movementInAir;
    private int _amountOfJumpsLeft;
    private bool _isFacingRight = true;
    private bool _isGrounded;
    private bool _isWalking;
    private bool _isWallSliding;
    private bool _isTouchingWall;
    private bool _canJump;
    private int _facingDirection = 1;
    
    [SerializeField] private Vector2 wallHopDirection;
    [SerializeField] private Vector2 wallJumpDirection;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private int amountOfJumps = 1;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float wallHopForce = 10.0f;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private float variableAirDragMultiplier = 0.95f;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsWallSliding = Animator.StringToHash("isWallSliding");

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
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
        _anim.SetBool(IsWallSliding, _isWallSliding);
    }
    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayerMask);
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
        if (_canJump && !_isWallSliding)
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpForce);
            _amountOfJumpsLeft--;
        } 
        else if (_isWallSliding && _movementInputDirection == 0 && _canJump)
        {
            _isWallSliding = false;
            _amountOfJumpsLeft--;
            var wallHopForceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -_facingDirection, wallHopForce * wallHopDirection.y);
            _body.AddForce(wallHopForceToAdd, ForceMode2D.Impulse);
        }
        else if ((_isWallSliding || _isTouchingWall) && _movementInputDirection != 0 && _canJump)
        {
            _isWallSliding = false;
            _amountOfJumpsLeft--;
            var wallJumpForceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * _movementInputDirection, wallJumpForce * wallJumpDirection.y);
            _body.AddForce(wallJumpForceToAdd, ForceMode2D.Impulse);
        }
    }

    void ApplyMovement()
    {
        if (_isGrounded)
        {
            _body.velocity = new Vector2(runSpeed * _movementInputDirection, _body.velocity.y);
        }
        else if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            var forceToBeAdded = new Vector2(_movementInAir * _movementInputDirection, 0);
            _body.AddForce(forceToBeAdded);

            if (Mathf.Abs(_body.velocity.x) > runSpeed)
            {
                _body.velocity = new Vector2(runSpeed * _movementInputDirection, _body.velocity.y);
            }
        }
        else if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            var forceToBeAdded = new Vector2(_body.velocity.x * variableAirDragMultiplier, _body.velocity.y);
            _body.AddForce(forceToBeAdded);
        }
        
        
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

        if((_isGrounded && _body.velocity.y <= 0) || _isWallSliding)
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
        _facingDirection *= -1;
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
