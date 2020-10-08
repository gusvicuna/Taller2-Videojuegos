using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1,10)]
    private float _movementSpeed = 5;
    [SerializeField]
    [Range(1, 10)]
    private float _jumpForce = 5;

    private int _diamonds = 0;

    private Rigidbody2D _myRb;
    [SerializeField]
    private float _groundDistanceChecker = 2;
    [SerializeField]
    private LayerMask _layerMaskPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput() {
        CheckMovement();
        CheckJump();
    }

    private void CheckJump() {
        if (Input.GetButtonDown("Jump")) {
            if (IsGrounded())
                Jump();
        }
    }

    private void Jump() {
        
        _myRb.velocity = new Vector2(_myRb.velocity.x, _jumpForce);
    }

    private bool IsGrounded() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _groundDistanceChecker, ~_layerMaskPlayer);
        if (hitInfo.collider != null) return true;
        return false;
    }

    private void CheckMovement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal Move");
        if (horizontalMove != 0) Move(horizontalMove);
    }

    private void Move(float horizontalMove) {
        transform.Translate(new Vector3(horizontalMove * Time.deltaTime * _movementSpeed, 0, 0));
    }

    private void OnDrawGizmosSelected() {
        Debug.DrawRay(transform.position, Vector2.down * _groundDistanceChecker, Color.green);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Diamond")) {
            _diamonds += 1;
            Destroy(collision.gameObject);
        }
    }
}
