using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private float runSpeed = 10.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _body.velocity = new Vector2(_horizontal * runSpeed, _vertical * runSpeed);   
    }
}
