using UnityEngine;

namespace SuperAbilityMario.Character
{
    public abstract class Character : MonoBehaviour, ICharacter
    {

        // Properties
        public CharacterMotor CharacterMotor => _characterMotor;
        public CharacterInput CharacterInput => _characterInput;
        public CharacterConfig CharacterConfig => _characterConfig;
        public bool IsGrounded => _isGrounded;
        public bool FacingRight => _facingRight;
        public float LastStateChangedTime => _lastStateChangedTime;
        public float LastGroundedTime => _lastGroundedTime;
        public float LastJumpTime => _lastJumpTime;
        public int JumpCount => _jumpCount;
        protected virtual IState DefaultState => States.WalkState;

        // Fields
        [Header("Component References")]
        [SerializeField] private CharacterMotor _characterMotor;
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private CapsuleCollider2D _capsuleCollider2D;

        [Header("Data References")]
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private GameConfig _gameConfig;

        [SerializeField] private float groundedCheckOffset;
        [SerializeField] private float groundedCheckHeight;

        // Non-serialized Fields
        [SerializeReference] private IState _currentState;
        private bool _isGrounded;
        private bool _facingRight = true;
        private float _lastStateChangedTime;
        private float _lastGroundedTime;
        private float _lastJumpTime;
        private int _jumpCount;

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
            _isGrounded = CheckIfGrounded();
            if(_isGrounded)
            {
                _lastGroundedTime = Time.time;
            }

            if (_currentState != null)
                _currentState.Loop(this, Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            if (_currentState != null)
                _currentState.FixedLoop(this, Time.deltaTime);
        }

        public void SetState(IState state)
        {
            _currentState = state;
            _lastStateChangedTime = Time.time;
            state.Enter(this);
        }

        public void SetCharacterConfig(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            RefreshCharacterConfig();
        }

        public void IncrementJumpCount()
        {
            _jumpCount++;
            _lastJumpTime = Time.time;
        }

        public void ResetJumpCount()
        {
            _jumpCount = 0;
        }

        public void Flip()
        {
            _facingRight = !_facingRight;
            transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
        }

        private void RefreshCharacterConfig()
        {
            _characterConfig.ApplyColliderBounds(_capsuleCollider2D);
        }

        private bool CheckIfGrounded()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(GetGroundedBoxCastCenter(), GetGroundedBoxCastSize(), 0, Vector2.down, 0, _gameConfig.LayerMask);
            return raycastHit2D.collider != null;
        }

        private Vector2 GetGroundedBoxCastCenter()
        {
            return (Vector2)transform.position + (Vector2.down * (groundedCheckOffset + groundedCheckHeight * 0.5f));
        }

        private Vector2 GetGroundedBoxCastSize()
        {
            return new Vector2(_characterConfig.GetColliderWidth() * 0.9f, groundedCheckHeight);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(GetGroundedBoxCastCenter(), GetGroundedBoxCastSize());
        }

    }
}
