using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  [Header("Movement")]
  public float forceToAdd = 5f;

  [Header("Ground check")]
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask whatIsGround;

  private bool _grounded;
  const float _groundedRadius = .15f;

  private Rigidbody2D _rb;

  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    _grounded = false;

    Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, _groundedRadius, whatIsGround);
    for (int i = 0; i < groundColliders.Length; i++)
    {
      if (groundColliders[i].gameObject != gameObject)
      {
        _grounded = true;
      }
    }
  }

  public void AddForce(Vector2 force)
  {
    if (!_grounded)
    {
      return;
    }

    Vector3 targetVelocity = new Vector2(force.x * forceToAdd, force.y * forceToAdd);

    _rb.velocity = targetVelocity;
  }

}
