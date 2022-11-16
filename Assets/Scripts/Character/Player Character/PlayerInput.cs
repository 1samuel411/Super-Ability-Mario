using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterInput
{

    void Update()
    {
        _inputMap.x = Input.GetAxis("Horizontal");
        _inputMap.space = Input.GetKeyDown(KeyCode.Space);
    }

}
