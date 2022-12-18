using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMotor : MonoBehaviour
{

    public Vector2 Velocity => _rigidbody2D.velocity;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    internal void ApplyForce(Vector2 force, ForceMode2D forceMode = ForceMode2D.Force)
    {
        if (force == Vector2.zero)
            return;

        _rigidbody2D.AddForce(force, forceMode);
    }

    internal void ApplyHorizontalDrag(float drag)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x * Mathf.Clamp01(1 - drag), _rigidbody2D.velocity.y);
    }

    internal void ApplyVerticalDrag(float drag)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * Mathf.Clamp01(1 - drag));
    }

    internal void LimitXVelocity(float maxVelocity)
    {
        if (Mathf.Abs(_rigidbody2D.velocity.x) > maxVelocity)
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * maxVelocity, _rigidbody2D.velocity.y);
            return;
        }
    }

    public void ResetYVelocity()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
    }

    public void ResetXVelocity()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    public float CalculateJumpForce(float jumpHeight)
    {
        float gravity = _rigidbody2D.gravityScale * -Physics.gravity.y;
        return _rigidbody2D.mass * Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void DisableMotor()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void EnableMotor()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

}
