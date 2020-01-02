using System;
using UnityEngine;
using static Characters.CharacterState;
using static Characters.InputConstants;

namespace Characters.Allies {
	public class PlayerController : PhysicsObject {

		[Header("Controller Variables")]
		[SerializeField]
		private float maxSpeed = 7;
		[SerializeField]
		private float jumpTakeOffSpeed = 7;

		private SpriteRenderer spriteRenderer;
		private Animator animator;
		
		private static readonly int GROUNDED = Animator.StringToHash("grounded");
		private static readonly int VELOCITY_X = Animator.StringToHash("velocityX");

		private void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();    
			animator = GetComponent<Animator>();
		}

		protected override void ComputeVelocity() {
			Vector2 move = Vector2.zero;

//			move.x = ControllerInputManager.GetHorizontal();
			move.x = ControllerInputManager.GetRawHorizontal();

			switch(state) {
				case State.DEFAULT:
				case State.WALKING:
					if(Input.GetButtonDown(JUMP_INPUT_NAME)) {
						velocity.y = jumpTakeOffSpeed;
						this.state = Jumping();
					} else if(Input.GetButtonUp(JUMP_INPUT_NAME)) {
						if(velocity.y > 0) {
							velocity.y = velocity.y * 0.5f;
							this.state = Falling();
						}
					}
					break;
				case State.JUMPING:
				case State.FALLING:
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

			bool flipSprite = spriteRenderer.flipX ? move.x > 0.01f : move.x < 0.01f;
			if(flipSprite) {
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}

			animator.SetBool(GROUNDED, IsGrounded(state));
			animator.SetFloat(VELOCITY_X, Mathf.Abs(velocity.x) / maxSpeed);

			targetVelocity = move * maxSpeed;
		}
	}
}
