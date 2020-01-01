using UnityEngine;
using static Characters.CharacterState;
using static Characters.InputConstants;

namespace Characters.Allies {
	public class TestPhysicsController : PhysicsObject {

		[Header("Physics Variables")]
		[SerializeField]
		private float maxSpeed = 7;
		[SerializeField]
		private float jumpTakeOffSpeed = 7;

		private SpriteRenderer spriteRenderer;
		private Animator animator;
		
		private static readonly int Grounded = Animator.StringToHash("grounded");
		private static readonly int VelocityX = Animator.StringToHash("velocityX");

		private void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();    
			animator = GetComponent<Animator>();
		}

		protected override void ComputeVelocity() {
			Vector2 move = Vector2.zero;

			move.x = ControllerInputManager.OSX() ? -Input.GetAxisRaw(ControllerInputManager.Horizontal()) : 
				         Input.GetAxisRaw(ControllerInputManager.Horizontal());

			if(Input.GetButtonDown(JUMP_INPUT_NAME) && IsGrounded(state)) {
				velocity.y = jumpTakeOffSpeed;
			} else if(Input.GetButtonUp(JUMP_INPUT_NAME)) {
				if(velocity.y > 0) {
					velocity.y = velocity.y * 0.5f;
				}
			}

			bool flipSprite = spriteRenderer.flipX ? move.x > 0.01f : move.x < 0.01f;
			if(flipSprite) {
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}

			animator.SetBool(Grounded, IsGrounded(state));
			animator.SetFloat(VelocityX, Mathf.Abs(velocity.x) / maxSpeed);

			targetVelocity = move * maxSpeed;
		}
	}
}
