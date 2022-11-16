using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperAbilityMario.Character
{
    public abstract class Character : MonoBehaviour, ICharacter
    {

        // Properties
        public CharacterMotor CharacterMotor => _characterMotor;
        public CharacterInput CharacterInput => _characterInput;
        public CharacterConfig CharacterConfig => _characterConfig;
        protected virtual IState DefaultState => States.WalkState;

        // Fields
        [Header("Component References")]
        [SerializeField] private CharacterMotor _characterMotor;
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        [Header("Data References")]
        [SerializeField] private CharacterConfig _characterConfig;

        // Non-serialized Fields
        private IState _currentState;

        protected virtual void Awake()
        {
            RefreshCharacterConfig();
        }

        protected virtual void Start()
        {
            SetState(DefaultState);
        }

        protected virtual void Update()
        {
            if(_currentState != null)
                _currentState.Loop(this, Time.deltaTime);
        }

        public void SetState(IState state)
        {
            _currentState = state;
            state.Enter(this);
        }

        public void SetCharacterConfig(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            RefreshCharacterConfig();
        }

        private void RefreshCharacterConfig()
        {
            _characterConfig.ApplyBoxCollider(_boxCollider2D);
        }

    }
}
