using System;
using Items.Currencies;
using Music;
using UnityEngine;
using Utility;
using static Characters.CharacterState;
using static Characters.DirectionUtility;
using static Characters.DirectionUtility.InputAngleState;
using static Characters.InputConstants;
using static UnityEngine.InputSystem.InputAction;

namespace Characters.Allies {
	public class PlayerController : PhysicsObject {
		[Header("Controller Variables")] [SerializeField]
		private float maxSpeed = 7;

		[SerializeField] private float jumpTakeOffSpeed = 7;

		private PCInputActions pcInputActions;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private static readonly int GROUNDED = Animator.StringToHash("grounded");
		private static readonly int VELOCITY_X = Animator.StringToHash("velocityX");

		private Vector2 move;
		private InputAngleState inputDirection;
		
		private void Awake() {
			this.pcInputActions = new PCInputActions();
			this.spriteRenderer = GetComponent<SpriteRenderer>();
			this.animator = GetComponent<Animator>();
			
			ControllerInputManager.InitControllers();

			pcInputActions.Player.MovementPress.performed += OnMovement;
			pcInputActions.Player.MovementRelease.performed += StopMovement;
			pcInputActions.Player.JumpPress.performed += OnJumpPress;
			pcInputActions.Player.JumpRelease.performed += OnJumpRelease;
			pcInputActions.Player.Action.performed += OnAction;
			pcInputActions.Player.SpecialAction.performed += OnSpecialAction;
			pcInputActions.Player.Menu.performed += OnMenu;
			
			pcInputActions.Enable();
		}

		private void OnDisable() {
			pcInputActions.Disable();
		}
		
		private void OnCollisionEnter2D(Collision2D other) {
			if(other.gameObject.CompareTag(Tags.CURRENCY)) { 
				Currency currency = other.gameObject.GetComponent<Currency>();
				currency.OnCollect();
			}
		}

		/*
		 * 
			if(x > 0) {
				this.inputAngleState &= RIGHT_MASK;
			} else if(x < 0) {
				this.inputAngleState &= LEFT_MASK;
			}
			
			if(y > 0) {
				this.inputAngleState &= UP_MASK;
			} else if(x < 0) {
				this.inputAngleState &= DOWN_MASK;
			}
		 */
		private void OnMovement(CallbackContext ctx) {
			Vector2 input = ctx.ReadValue<Vector2>();
			
			this.inputDirection = CalculateInputAngle(input.x, input.y);
			
			(this.move.x, this.move.y) = RawVector(input);
			Debug.Log($"moving: {move}");
		}

		private static InputAngleState CalculateInputAngle(float x, float y) {
			InputAngleState angleState = DEFAULT;
			if(x > 0 && y > 0) {
				angleState = RIGHT_UP;
			} else if(x > 0 && MagnitudeWithinRange(y)) {
				angleState = RIGHT;
			} else if(x > 0 && y < 0) {
				angleState = RIGHT_DOWN;
			} else if(MagnitudeWithinRange(x) && y < 0) {
				angleState = DOWN;
			} else if(x < 0 && y < 0) {
				angleState = LEFT_DOWN;
			} else if(x < 0 && MagnitudeWithinRange(x)) {
				angleState = LEFT;
			} else if(x < 0 && y > 0) {
				angleState = LEFT_UP;
			} else if(MagnitudeWithinRange(x) && y > 0) {
				angleState = UP;
			}
			return angleState;
		}

		private static bool MagnitudeWithinRange(float mag) {
			return 0.1 <= mag && mag <= 0.1;
		}

		private static (int, int) RawVector(Vector2 input) {
			int x = 0, y = 0;
			
			if(input.x > 0) {
				x = 1;
			} else if(input.x < 0) {
				x = -1;
			}
			
			if(input.y > 0) {
				y = 1;
			} else if(input.y < 0) {
				y = -1;
			}
			return (x, y);
		}

		private void StopMovement(CallbackContext ctx) {
			Debug.Log($"stop moving: {move}");
			this.move = Vector2.zero;
			this.inputDirection = DEFAULT;
		}
		
		private void OnJumpPress(CallbackContext ctx) {
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
		
		private void OnJumpRelease(CallbackContext ctx) {
			Debug.Log("falling");
			if(velocity.y > 0) {
				this.state = Falling();
			}
		}
		
		private void OnAction(CallbackContext ctx) {
			Debug.Log("action");
		}
		
		private void OnSpecialAction(CallbackContext ctx) {
			Debug.Log("action");
		}
		
		private void OnMenu(CallbackContext ctx) {
			Debug.Log("menu");
		}

		protected override void ComputeVelocity() {
#if USING_INPUT_OLD
			Vector2 move = Vector2.zero;
			move.x = ControllerInputManager.GetRawHorizontal();
#endif
			float moveX = move.x;

			OnCharacterState();
			
			if(moveX > 0) {
				this.facingState = Right();
			} else if(moveX < 0) {
				this.facingState = Left();
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
#if USING_INPUT_OLD
					if(Input.GetButtonUp(JUMP_INPUT_NAME)) {
#endif
					if(velocity.y > 0) {
						velocity.y = velocity.y * 0.5f;
						this.state = Falling();
					}
#if USING_INPUT_OLD
					}
#endif
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
				case FacingState.RIGHT:
					spriteRenderer.flipX = false;
					break;
				case FacingState.LEFT:
					spriteRenderer.flipX = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}