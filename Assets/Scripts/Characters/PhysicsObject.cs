using System.Collections.Generic;
using UnityEngine;

namespace Characters {
	public class PhysicsObject : MonoBehaviour {
		[SerializeField]
		private float minGroundNormalY = .65f;
		[SerializeField]
		private float gravityModifier = 1f;
		// Set which layers to hit with rb2d.Raycast
		[SerializeField]
		public LayerMask mask;
		
		protected Rigidbody2D rb2d;
		protected Collider2D myCollider;
		protected ContactFilter2D contactFilter;
		[SerializeField] [DisableInspectorEdit]
		protected CharacterState.State state;
	
		protected Vector2 velocity;
		protected Vector2 targetVelocity;
		protected Vector2 groundNormal;
		protected Vector2 move;

		protected readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
		protected readonly List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>();
		
		protected const float minMoveDistance = 0.001f;
		protected const float shellRadius = 0.01f;

		// TODO - figure out how to get raycasts to ignore multiple layer at once
		private void Start() {
			this.rb2d = GetComponent<Rigidbody2D>();
			this.myCollider = GetComponent<Collider2D>();
			
			// Make Raycasts ignore this layer
			contactFilter.useTriggers = false;
			contactFilter.SetLayerMask(mask);
			contactFilter.useLayerMask = true;
		}

		private void Update() {
			targetVelocity = Vector2.zero;
			ComputeVelocity();
		}

		protected virtual void ComputeVelocity() {
		}
	
		private void FixedUpdate() {
			Vector2 verticalVelocity = Physics2D.gravity * (gravityModifier * Time.deltaTime);
//			Debug.Log($"verticalVelocity={verticalVelocity.y}");
			
			velocity += verticalVelocity;

			if(velocity.y < 0) {
//				Debug.Log($"");
				this.state = CharacterState.Falling();
			}
			
//			Debug.Log($"velocity={velocity}");
			
			velocity.x = targetVelocity.x;
	
//			this.state = CharacterState.Default();
			
			Vector2 deltaPosition = velocity * Time.deltaTime;
			Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
			Vector2 move = moveAlongGround * deltaPosition.x;
			
			Movement(move, false);

			move = Vector2.up * deltaPosition.y;

			Movement(move, true);
		}

		private void Movement(Vector2 move, bool yMovement){
			float distance = move.magnitude;

			if(distance > minMoveDistance) {
				// Cast rays from body (excluding our own collider)
				int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
				hitBufferList.Clear();
				for(int i = 0; i < count; i++) {
					hitBufferList.Add(hitBuffer[i]);
				}

				// Check each ray to see if the distance between it and the ground is low enough
				foreach(RaycastHit2D hit in hitBufferList) {
					Vector2 currentNormal = hit.normal;
					if(currentNormal.y > minGroundNormalY) {
						this.state = CharacterState.Default();
						if(yMovement) {
							groundNormal = currentNormal;
							currentNormal.x = 0;
						}
					}

					float projection = Vector2.Dot(velocity, currentNormal);
					if(projection < 0) {
						velocity = velocity - projection * currentNormal;
					}

					float modifiedDistance = hit.distance - shellRadius;
					distance = modifiedDistance < distance ? modifiedDistance : distance;
				}
			}
			rb2d.position = rb2d.position + move.normalized * distance;
		}
	}
}
