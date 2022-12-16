using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void Enter(Character character);

    void Loop(Character character, float deltaTime);

    void FixedLoop(Character character, float deltaTime);
}
