using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IInteractable
{

    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private EntityMotor entityMotor;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float height = 0.16f;
    [SerializeField] private float moveSpeed = 1.4f;
    [SerializeField] private float animSpeed = 1.5f;
    [SerializeField] private float width;
    [SerializeField] private float collisionSensitivity;
    private int moveDir = 1;
    


    private void OnEnable()
    {
        StartCoroutine(AnimateIntoApperance());    
    }

    private IEnumerator AnimateIntoApperance()
    {
        float yAnimTarget = transform.position.y + height;
        boxCollider.enabled = false;
        entityMotor.DisableMotor();
        while (transform.position.y < yAnimTarget)
        {
            transform.position += Vector3.up * Time.deltaTime * animSpeed;
            yield return null;
        }
        moveDir = 1;
        entityMotor.EnableMotor();
        boxCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        if (boxCollider.enabled == false)
            return;

        entityMotor.ApplyForce(Vector2.right * moveDir);
        entityMotor.LimitXVelocity(moveSpeed);

        RaycastHit2D hitRight = Physics2D.BoxCast(GetCollisionCenter(true), GetCollisionSize(), 0, Vector2.right, collisionSensitivity, gameConfig.GroundedLayerMask);
        RaycastHit2D hitLeft = Physics2D.BoxCast(GetCollisionCenter(false), GetCollisionSize(), 0, Vector2.left, collisionSensitivity, gameConfig.GroundedLayerMask);

        if (hitRight.collider != null)
            moveDir = -1;
        else if (hitLeft.collider != null)
            moveDir = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(GetCollisionCenter(true), GetCollisionSize());
        Gizmos.DrawCube(GetCollisionCenter(false), GetCollisionSize());
    }

    private Vector2 GetCollisionCenter(bool right)
    {
        return transform.position + Vector3.right * width * 0.5f * (right ? 1 : -1);
    }

    private Vector2 GetCollisionSize()
    {
        return Vector2.one * collisionSensitivity;
    }

    public void Interact(Character character, Vector2 pos)
    {
        if(character is PlayerCharacter)
        {

            boxCollider.enabled = false;
            entityMotor.ResetXVelocity();
            entityMotor.ResetYVelocity();
            entityMotor.DisableMotor();
            ApplyPowerUp(character as PlayerCharacter);

            gameObject.SetActive(false);
        }
    }

    public abstract void ApplyPowerUp(PlayerCharacter character);
}
