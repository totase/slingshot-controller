using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour {

	[Header ("Object references")]
	public Rigidbody2D anchor;

	[Header ("Hook variables")]
	public float maxDragDistance;

	private Vector2 prevVelocity;

	private bool released = false;

	private Rigidbody2D rb;  
	private bool dragging = false;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (dragging && !released) {
			Dragging();
		}

		if (!released) {
			if (!rb.isKinematic && prevVelocity.sqrMagnitude > rb.velocity.sqrMagnitude) {
				Release();
			}

			if (!dragging) {
				prevVelocity = rb.velocity;
			}
		}
	}

	void Dragging() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float distance = Vector3.Distance(mousePos, anchor.position);

		if (distance > maxDragDistance) {
			rb.position = anchor.position + (mousePos - anchor.position).normalized * maxDragDistance;
		} else {
			rb.position = mousePos;
		}
	}

	public void SetPosition(Vector3 pos) {
		anchor.gameObject.transform.position = pos;
		
		Reset();
	}

	void OnMouseDown() {
		dragging = true;
	}

	void OnMouseUp() {
		dragging = false;
		rb.isKinematic = false;
	}

	void Release() {
		released = true;

		Reset();
	}

	void Reset() {
		rb.isKinematic = true;
		rb.velocity = new Vector2(0f, 0f);
		rb.position = anchor.position;

		released = false;
	}

}
