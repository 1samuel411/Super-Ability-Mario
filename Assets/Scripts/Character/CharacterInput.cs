using System;
using UnityEngine;

namespace SuperAbilityMario.Character
{
    [Serializable]
    public struct InputMap
    {
        public float x;
        public bool space;
    }

    public abstract class CharacterInput : MonoBehaviour
    {

        public InputMap InputMap => _inputMap;

        [SerializeField] protected InputMap _inputMap;

    }
}