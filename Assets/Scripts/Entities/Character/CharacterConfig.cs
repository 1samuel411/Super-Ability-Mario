using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterConfig : ScriptableObject
{

    public float MoveSpeed { get => _moveSpeed; }
    public float LinearDrag { get => _linearDrag; }
    public float VerticalDrag { get => _verticalDrag; }
    public float JumpTimer { get => _jumpTimer; }
    public float AccelerationSpeed { get => _accelerationSpeed; }
    public float JumpHeight { get => _jumpHeight; }

    public float JumpBufferTime { get => _jumpBufferTime; }
    public float CoyoteJumpTime { get => _coyoteJumpTime; }

    public int MaxJumps { get => _maxJumps; }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _linearDrag;
    [SerializeField] private float _verticalDrag;
    [SerializeField] private float _jumpTimer;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _jumpHeight;

    [SerializeField] private float _jumpBufferTime;
    [SerializeField] private float _coyoteJumpTime;
    
    [SerializeField] private int _maxJumps;

    [SerializeField] private Vector2 _collisionOffset;
    [SerializeField] private Vector2 _collisionSize;

    public void ApplyColliderBounds(CapsuleCollider2D capsuleCollider)
    {
        capsuleCollider.offset = _collisionOffset;
        capsuleCollider.size = new Vector2(_collisionSize.x, _collisionSize.y);
    }

    public float GetColliderWidth()
    {
        return _collisionSize.x;
    }

}
