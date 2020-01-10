using System;
using Items.Currencies;
using Music;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Utility;
using static Characters.CharacterState;
using static Characters.InputConstants;

namespace Characters.Allies {
	public class PlayerController : PhysicsObject {
		[Header("Controller Variables")] [SerializeField]
		private float maxSpeed = 7;

		[SerializeField] private float jumpTakeOffSpeed = 7;

		private GamepadActions gamepadActions;
		
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private static readonly int GROUNDED = Animator.StringToHash("grounded");
		private static readonly int VELOCITY_X = Animator.StringToHash("velocityX");

		private Vector2 move;
		
		private void Awake() {
			this.gamepadActions = new GamepadActions();
			this.spriteRenderer = GetComponent<SpriteRenderer>();
			this.animator = GetComponent<Animator>();
			
			ControllerInputManager.InitControllers();

			gamepadActions.Player.MovementPress.performed += StartMovement;
			gamepadActions.Player.MovementRelease.performed += StopMovement;
			gamepadActions.Player.JumpPress.performed += OnJumpPress;
			gamepadActions.Player.JumpRelease.performed += OnJumpRelease;
			
			gamepadActions.Enable();
		}

		private void OnDisable() {
			gamepadActions.Disable();
		}
		
		private void OnCollisionEnter2D(Collision2D other) {
			if(other.gameObject.CompareTag(Tags.CURRENCY)) { 
				Currency currency = other.gameObject.GetComponent<Currency>();
				currency.OnCollect();
			}
		}

		private void StartMovement(InputAction.CallbackContext ctx) {
			Vector2 m = ctx.ReadValue<Vector2>();
			if(m.x > 0) {
				this.move.x = 1;
			} else if(m.x < 0) {
				this.move.x = -1;
			}
			
			
			if(m.y > 0) {
				this.move.y = 1;
			} else if(m.y < 0) {
				this.move.y = -1;
			}
			
			Debug.Log($"moving: {move}");
		}

		private void StopMovement(InputAction.CallbackContext ctx) {
			Debug.Log($"stop moving: {move}");
			this.move = Vector2.zero;
		}
		
		public void OnJumpPress(InputAction.CallbackContext ctx) {
			Debug.Log("jumping");
			switch(state) {
				case State.DEFAULT:
				case State.WALKING:
					velocity.y = jumpTakeOffSpeed;
					this.state = Jumping();
					this.PlayerJumpSFX();
					break;
			}
		}
		
		public void OnJumpRelease(InputAction.CallbackContext ctx) {
			Debug.Log("falling");
			if(velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
				this.state = Falling();
			}
		}
		
//		public void OnAction(InputValue value) {
//			Debug.Log("action");
////			m_Move = value.Get<Vector2>();
//		}
//		
//		public void OnMenu(InputValue value) {
//			Debug.Log("menu");
////			m_Move = value.Get<Vector2>();
//		}

		protected override void ComputeVelocity() {
#if USING_INPUT_OLD
//			Debug.Log($"input=\"{Input.inputString}\"");
//			Debug.Log($"GetJoystickNames=\"{string.Join(", ", Input.GetJoystickNames())}\"");
//			
//			Debug.Log($"inputt=\"{Input.GetButtonDown(JOYSTICK_BUTTON_3.ToString())}\"");
//			Debug.Log($"inputt=\"{InputSystem.GetDevice<InputDevice>()}\"");
//			Debug.Log($"controller=\"{}\"");
			
			Vector2 move = Vector2.zero;

			move.x = ControllerInputManager.GetRawHorizontal();
#endif
			float moveX = move.x;

			OnCharacterState();
			
			if(moveX > 0) {
				this.facingState = DirectionUtility.Right();
			} else if(moveX < 0) {
				this.facingState = DirectionUtility.Left();
			}

			OnFacingState();

			animator.SetBool(GROUNDED, IsGrounded(state));
			animator.SetFloat(VELOCITY_X, Mathf.Abs(velocity.x) / maxSpeed);

			targetVelocity = move * maxSpeed;
		}

		private void OnCharacterState() {
			switch(state) {
				case State.DEFAULT:
				case State.WALKING:
					break;
				case State.JUMPING:
					break;
				case State.FALLING:
					// When letting go of jump, make sure that vertical velocity is updated correctly
					if(Input.GetButtonUp(JUMP_INPUT_NAME)) {
						if(velocity.y > 0) {
							velocity.y = velocity.y * 0.5f;
							this.state = Falling();
						}
					}

					break;
				case State.CLIMBING_IDLE:
					break;
				case State.CLIMBING_UP:
					break;
				case State.CLIMBING_DOWN:
					break;
				case State.KNOCKED_BACK:
					break;
				case State.P_RIGHT_UP:
					break;
				case State.P_RIGHT:
					break;
				case State.P_RIGHT_DOWN:
					break;
				case State.P_LEFT_UP:
					break;
				case State.P_LEFT:
					break;
				case State.P_LEFT_DOWN:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnFacingState() {
			switch(facingState) {
				case DirectionUtility.FacingState.RIGHT:
					spriteRenderer.flipX = false;
					break;
				case DirectionUtility.FacingState.LEFT:
					spriteRenderer.flipX = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}