using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Range(0.0f, 1.0f)] public float scale;
    [Tooltip("Horizontal Speed")] public float speedH;
    [Tooltip("Dash distance")] public float dashDistance;
    [Tooltip("Horizontal Speed when in air")] public float speedHorizontalAir;
    [Tooltip("Force of Jump")] public float force;
    [SerializeField] BoxCollider2D col;
    private Rigidbody2D rigidbody2d;
    private bool canJumpfield = false;

    // Use this for initialization
    void Start () {
        Vector2 size = col.size;
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider.size.Set(size.x * scale, size.y * scale);
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (canJumpfield && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.AddForce(new Vector2(0f, force));
        }
        Vector3 movement = new Vector3(moveHorizontal * speedHorizontalAir, rigidbody2d.velocity.y, 0.0f);
        if (canJumpfield)
            movement = new Vector3(moveHorizontal * speedH, rigidbody2d.velocity.y, 0.0f);
        rigidbody2d.velocity = movement;
        if( Input.GetButtonDown("Special"))
        {
            Dash(Vector2.right);
        }
    }
    void Dash( Vector2 direction)
    {
        transform.Translate(new Vector3( direction.x * dashDistance, direction.y * dashDistance, 0.0f));
        Debug.Log(direction * dashDistance);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        canJumpfield = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canJumpfield = false;
    }
}
