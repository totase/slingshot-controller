using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{

  [Header("Object references")]
  public Rigidbody2D anchor;
  public PlayerController player;

  [Header("Hook variables")]
  public float maxDragDistance;

  private Vector2 _prevVelocity;
  private bool _released = false;
  private Rigidbody2D _rb;
  private bool _dragging = false;

  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (_dragging && !_released)
    {
      Dragging();
    }

    if (!_released)
    {
      if (!_rb.isKinematic && _prevVelocity.sqrMagnitude > _rb.velocity.sqrMagnitude)
      {
        Release();
      }

      if (!_dragging)
      {
        _prevVelocity = _rb.velocity;
      }
    }
  }

  void Dragging()
  {
    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    float distance = Vector3.Distance(mousePos, anchor.position);

    if (distance > maxDragDistance)
    {
      _rb.position = anchor.position + (mousePos - anchor.position).normalized * maxDragDistance;
    }
    else
    {
      _rb.position = mousePos;
    }
  }

  void OnMouseDown()
  {
    _dragging = true;
  }

  void OnMouseUp()
  {
    _dragging = false;
    _rb.isKinematic = false;
  }

  void Release()
  {
    _released = true;
    player.AddForce(_rb.velocity);

    Reset();
  }

  void Reset()
  {
    _rb.isKinematic = true;
    _rb.velocity = new Vector2(0f, 0f);
    _rb.position = anchor.position;

    _released = false;
  }

}
