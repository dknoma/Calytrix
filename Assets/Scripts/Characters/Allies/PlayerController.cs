using System;
using Items.Currencies;
using Music;
using UnityEngine;
using Utility;
using static Characters.CharacterState;
using static Characters.DirectionUtility;
using static Characters.DirectionUtility.InputAngleState;
using static Utility.ProjectConstants;
using static UnityEngine.InputSystem.InputAction;

namespace Characters.Allies {
	public class PlayerController : PhysicsCharacter {
		private static readonly int GROUNDED = Animator.StringToHash("grounded");
		private static readonly int VELOCITY_X = Animator.StringToHash("velocityX");
		
		[Header("Controller Variables")] 
		[SerializeField] private float maxSpeed = 7;
		[SerializeField] private float jumpTakeOffSpeed = 7;

		private PCInputActions pcInputActions;
		private SpriteRenderer spriteRenderer;
		private Animator animator;

		private Vector2 move;
		private InputAngleState inputDirection;

		private bool dpadMovement;
		private bool leftStickMovement;
		
		private void Awake() {
			this.pcInputActions = new PCInputActions();
			this.spriteRenderer = GetComponent<SpriteRenderer>();
			this.animator = GetComponent<Animator>();
			
			ControllerInputManager.InitControllers();
			InitInput();
		}

		private void OnDisable() {
			pcInputActions.Disable();
		}
		
		private void OnCollisionEnter2D(Collision2D other) {
			if(other.gameObject.CompareTag(Tags.CURRENCY)) { 
				CurrencyObject currencyObject = other.gameObject.GetComponent<CurrencyObject>();
				currencyObject.OnCollect();
			}
		}

		// TODO - add input to its own script? maybe move and some other shared variables can be stored in
		//		  scriptable objects. Benefit of this is other scripts can easily check these values if needed
		//		- not all scripts may be attached to a collider ie. system that monitors player speed
		private void InitInput() {
			pcInputActions.Player.Move.performed += OnMove;
			pcInputActions.Player.Move.canceled += StopMove;
			pcInputActions.Player.ButtonMove.performed += OnDPadMove;
			pcInputActions.Player.ButtonMove.canceled += StopMove;
			
			pcInputActions.Player.Jump.performed += OnJumpPress;
			pcInputActions.Player.Jump.canceled += OnJumpRelease;
			
			pcInputActions.Player.Action1.performed += OnAction1;
			pcInputActions.Player.Action2.performed += OnAction2;
			pcInputActions.Player.Action3.performed += OnAction3;
			
			pcInputActions.Player.Menu.performed += OnMenu;
			
			pcInputActions.Enable();
		}
		
		private void OnDPadMove(CallbackContext ctx) {
			if(leftStickMovement) return;
			
			dpadMovement = true;
			Vector2 input = ctx.ReadValue<Vector2>().normalized;
			this.inputDirection = CalculateInputAngle(input.x, input.y);
			
			Debug.Log($"inputDirection={inputDirection.ToString()}");

//			Vector2 clampedVector = PixelClamp(RawVector(input));
			Vector2 clampedVector = RawVector(input);
			(this.move.x, this.move.y) = (clampedVector.x, clampedVector.y);
		}

		private Vector2 PixelClamp(Vector2 input) {
			Vector2 vector = new Vector2(
			Mathf.RoundToInt(input.x * PIXELS_PER_UNIT),
			Mathf.RoundToInt(input.y * PIXELS_PER_UNIT));

			return vector / PIXELS_PER_UNIT;
		}
		
		private void OnMove(CallbackContext ctx) {
			if(dpadMovement) return;

			Vector2 input = ctx.ReadValue<Vector2>().normalized;
			if(input != Vector2.zero) {
				leftStickMovement = true;
			} else {
				leftStickMovement = false;
			}

			this.inputDirection = CalculateInputAngle(input.x, input.y);

//			Debug.Log($"inputDirection={inputDirection.ToString()}");

//			Vector2 clampedVector = PixelClamp(RawVector(input));
			Vector2 clampedVector = RawVector(input);
			(this.move.x, this.move.y) = (clampedVector.x, clampedVector.y);
		}

		private static Vector2 RawVector(Vector2 input) {
			float x = 0, y = 0;
			
			if(input.x > 0.5) {
				x = 1f;
			} else if(input.x < -0.5) {
				x = -1f;
			}
			
			if(input.y > 0.5) {
				y = 1f;
			} else if(input.y < -0.5) {
				y = -1f;
			}
			
			return new Vector2(x, y);
		}

		private void StopMove(CallbackContext ctx) {
			dpadMovement = false;
			leftStickMovement = false;
//			Debug.Log($"stop moving: {move}, {dpadMovement}");
			
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
		
		private void OnAction1(CallbackContext ctx) {
			Debug.Log("action1");
		}
		
		private void OnAction2(CallbackContext ctx) {
			Debug.Log("action2");
		}
		
		private void OnAction3(CallbackContext ctx) {
			Debug.Log("action3");
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
			
			if(moveX > 0 && InputRight(inputDirection)) {
				this.facingState = Right();
			} else if(moveX < 0 && InputLeft(inputDirection)) {
				this.facingState = Left();
			}

			OnFacingState();

			animator.SetBool(GROUNDED, IsGrounded(state));
			animator.SetFloat(VELOCITY_X, Mathf.Abs(velocity.x) / maxSpeed);

			targetVelocity = move * maxSpeed;
		}

		private void OnCharacterState() {
//			Debug.Log($"state={state}");
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