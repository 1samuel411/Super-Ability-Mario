using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    public Vector2 Velocity => _rigidbody2D.velocity;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    internal void ApplyVelocity(Vector2 force, ForceMode2D forceMode = ForceMode2D.Force)
    {
        if (force == Vector2.zero)
            return;

        _rigidbody2D.AddForce(force, forceMode);
        if (Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Abs(force.x))
        {
            _rigidbody2D.AddForce(new Vector2(-force.x, 0), forceMode);
        }
    }

    public void ResetYVelocity()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
    }

    public float CalculateJumpSpeed(float jumpHeight)
    {
        float gravity = _rigidbody2D.gravityScale * -Physics.gravity.y;
        return _rigidbody2D.mass * Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
