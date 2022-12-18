using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JumpState : IState
{
    void IState.Enter(Character character)
    {
    }

    void IState.Loop(Character character, float deltaTime)
    {
    }
    void IState.FixedLoop(Character character, float deltaTime)
    {
        CharacterConfig characterConfig = character.CharacterConfig;
        if (Time.time >= character.LastStateChangedTime + characterConfig.JumpTimer)
        {
            float jumpForce = character.EntityMotor.CalculateJumpForce(characterConfig.JumpHeight);

            character.IncrementJumpCount();
            character.EntityMotor.ResetYVelocity();
            character.EntityMotor.ApplyForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            character.SetState(States.WalkState);
        }
    }
}
