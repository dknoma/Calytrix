using UnityEngine;
using static Characters.CharacterState;
using static Characters.InputConstants;

namespace Characters.Allies {
	public class PlayerController : PhysicsObject {

		[Header("Physics Variables")]
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

			if(Input.GetButtonDown(JUMP_INPUT_NAME) && IsGrounded(state)) {
				velocity.y = jumpTakeOffSpeed;
				this.state = Jumping();
			} else if(Input.GetButtonUp(JUMP_INPUT_NAME)) {
				if(velocity.y > 0) {
					velocity.y = velocity.y * 0.5f;
					this.state = Falling();
				}
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
