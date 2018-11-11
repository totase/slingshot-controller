using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header ("Movement")]
	public float forceToAdd = 5f;
	private bool addedForce = false;

	[Header ("Ground check")]
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask whatIsGround;

	private bool grounded;
	const float groundedRadius = .15f;

	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		grounded = false;

		Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < groundColliders.Length; i++) {
			if (groundColliders[i].gameObject != gameObject) {
				grounded = true;
			}
		}
	}

	public void AddForce(Vector2 force) {
		if (!grounded) {
			return;
		}

		Vector3 targetVelocity = new Vector2(force.x * forceToAdd, force.y * forceToAdd);

		rb.velocity = targetVelocity;
	}
	
}
