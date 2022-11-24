using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private 
    float _vertical;

    private bool isJumping;

    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumpForce = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        int characterLevel = 32;
        int nextSkillLevel = GenerateCharacter("Spike", characterLevel);
        Debug.Log(nextSkillLevel);
        Debug.Log(GenerateCharacter("Faye", characterLevel));
    }

    public int GenerateCharacter(string characterName, int level)
    {
        Debug.LogFormat("Character: {0} – Level: {1}", characterName, level);
        return level += 5;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        
        transform.position += new Vector3(_horizontal, 0, 0) * (Time.deltaTime * runSpeed);
    }

    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump") && Mathf.Abs(_body.velocity.y) < 0.01f)
        {
            _body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
