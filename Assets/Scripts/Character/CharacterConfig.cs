using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterConfig : ScriptableObject
{

    public float MoveSpeed { get => _moveSpeed; }
    public float AccelerationSpeed { get => _accelerationSpeed; }
    public float JumpHeight { get => _jumpHeight; }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _jumpHeight;

    [SerializeField] private Vector2 _collisionOffset;
    [SerializeField] private Vector2 _collisionSize;

    public void ApplyBoxCollider(BoxCollider2D boxCollider)
    {
        boxCollider.offset = _collisionOffset;
        boxCollider.size = new Vector2(_collisionSize.x - boxCollider.edgeRadius, _collisionSize.y - boxCollider.edgeRadius);
    }

}
