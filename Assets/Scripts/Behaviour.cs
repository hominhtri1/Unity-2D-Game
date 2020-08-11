using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Behaviour : MonoBehaviour
{
	private Rigidbody2D body;

	private Animator animator;

	private bool facingRight;

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	bool IsGrounded()
	{
		LayerMask mask = ~(1 << 8);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, mask);

		if (hit.collider == null)
			return false;
		else
			return true;
	}
	
    void Start()
    {
		body = GetComponent<Rigidbody2D>();
		body.constraints = RigidbodyConstraints2D.FreezeRotation;

		animator = GetComponent<Animator>();

		facingRight = true;
    }

	void Update()
	{
		if ((body.velocity.x > 0 && !facingRight) || (body.velocity.x < 0 && facingRight))
			Flip();

		if (body.velocity.x == 0)
			animator.SetBool("Walking", false);
		else
			animator.SetBool("Walking", true);
	}

    void FixedUpdate()
    {
		bool isGrounded = IsGrounded();

        float forceX = Input.GetAxis("Horizontal") * 20;

		if (Math.Abs(body.velocity.x) > 5)
			forceX = 0;
		
		float forceY = Input.GetAxis("Vertical");
		
		if (forceY > 0)
			forceY = 500;
		else
			forceY = 0;
		
		if (!isGrounded)
			forceY = 0;
		
		body.AddForce(new Vector2(forceX, forceY));
    }
}
