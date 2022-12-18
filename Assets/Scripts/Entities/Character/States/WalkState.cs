using SuperAbilityMario.Character;
using UnityEngine;

public struct WalkState : IState
{

    void IState.Enter(Character character)
    {
    }

    void IState.Loop(Character character, float deltaTime)
    {
        InputMap inputMap = character.CharacterInput.InputMap;
        CharacterConfig characterConfig = character.CharacterConfig;
        
        bool pressedSpace = Time.time - inputMap.LastPressedSpaceTime <= characterConfig.JumpBufferTime && Time.time > characterConfig.JumpBufferTime;
        bool canJump = Time.time - character.LastJumpTime >= characterConfig.JumpBufferTime;
        if (canJump && pressedSpace && character.IsGrounded)
        {
            Debug.Log("Jumping from space");
            character.ResetJumpCount();
            character.SetState(States.JumpState);
            return;
        }
        if (Time.time - character.LastGroundedTime <= characterConfig.CoyoteJumpTime && canJump && pressedSpace)
        {
            Debug.Log("Jumping from coyote jump");
            character.ResetJumpCount();
            character.SetState(States.JumpState);
            return;
        }
        if (character.JumpCount < characterConfig.MaxJumps && pressedSpace && canJump)
        {
            Debug.Log("Jumping from an extra jump we earned");
            character.SetState(States.JumpState);
            return;
        }
    }

    void IState.FixedLoop(Character character, float deltaTime)
    {
        InputMap inputMap = character.CharacterInput.InputMap;
        CharacterConfig characterConfig = character.CharacterConfig;

        Vector2 velocityTarget = new Vector2(inputMap.X * characterConfig.AccelerationSpeed, 0);

        // Apply base movement force and limit the X Velocity
        character.EntityMotor.ApplyForce(velocityTarget, ForceMode2D.Force);
        character.EntityMotor.LimitXVelocity(characterConfig.MoveSpeed);

        // Handle flipping character and checking if we're changing directions 
        if ((velocityTarget.x > 0 && !character.FacingRight) || (velocityTarget.x < 0 && character.FacingRight))
        {
            character.Flip();
        }
        bool changingDirections = (velocityTarget.x > 0 && character.EntityMotor.Velocity.x < 0) || (velocityTarget.x < 0 && character.EntityMotor.Velocity.x > 0);

        if (character.IsGrounded)
        {
            // Apply different horizontal drags if we're grounded or changing directions
            if (Mathf.Abs(velocityTarget.x) < 0.4f || changingDirections)
            {
                character.EntityMotor.ApplyHorizontalDrag(characterConfig.LinearDrag);
            }
        }
        else
        {
            // When in the air and moving upwards, apply vertical drag if we're not holding space
            if (character.EntityMotor.Velocity.y > 0 && !inputMap.Space)
            {
                character.EntityMotor.ApplyVerticalDrag(characterConfig.VerticalDrag);
            }
        }
    }
}
