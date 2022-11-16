using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JumpState : IState
{
    void IState.Enter(Character character)
    {
        CharacterConfig characterConfig = character.CharacterConfig;
        float jumpHeight = character.CharacterMotor.CalculateJumpSpeed(characterConfig.JumpHeight);

        character.CharacterMotor.ResetYVelocity();
        character.CharacterMotor.ApplyVelocity(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        character.SetState(States.WalkState);
    }

    void IState.Loop(Character character, float deltaTime)
    {
    }
}
