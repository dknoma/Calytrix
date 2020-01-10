using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters {
	public class PhysicsObject : MonoBehaviour {
		[Header("Physics Variables")]
		[SerializeField]
		private float minGroundNormalY = .65f;
		[SerializeField]
		private float gravityModifier = 1f;
		
		[Header("Layer Physics Interactions")]
		// Set which layers to hit with rb2d.Raycast
		[SerializeField]
		public LayerMask mask;
		
		protected Rigidbody2D rb2d;
		protected Collider2D myCollider;
		protected ContactFilter2D contactFilter;
		
		[Header("Character State")]
		[SerializeField] [DisableInspectorEdit]
		protected CharacterState.State state;
		[SerializeField] [DisableInspectorEdit]
		protected DirectionUtility.FacingState facingState;
	
		protected Vector2 velocity;
		protected Vector2 targetVelocity;
		protected Vector2 groundNormal;

		protected readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
		protected readonly List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>();
		
		protected const float minMoveDistance = 0.001f;
		protected const float shellRadius = 0.01f;

		private void OnEnable(){
			this.rb2d = GetComponent<Rigidbody2D>();
			this.myCollider = GetComponent<Collider2D>();
		}
		
		private void Start() {
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
			Vector2 verticalVelocity = ComputeVerticalVelocity();
			
			velocity += verticalVelocity;
			velocity.x = targetVelocity.x;

			if(velocity.y < 0) {
				this.state = CharacterState.Falling();
			}
			
			Vector2 deltaPosition = velocity * Time.deltaTime;
			Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
			Vector2 move = moveAlongGround * deltaPosition.x;
			
			DoMovement(move, false);

			move = Vector2.up * deltaPosition.y;

			DoMovement(move, true);
		}

		private void DoMovement(Vector2 move, bool yMovement){
			float distance = move.magnitude;

			// If distance is past threshold
			if(distance > minMoveDistance) {
				// Cast rays from body (excluding our own collider)
				int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
				hitBufferList.Clear();
				// Collect all contacts from raycasts
				for(int i = 0; i < count; i++) {
					hitBufferList.Add(hitBuffer[i]);
				}

				// Check each ray to see if the distance between it and the ground is low enough
				for(int index = 0; index < hitBufferList.Count; index++) {
					RaycastHit2D hit = hitBufferList[index];
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

		private Vector2 ComputeVerticalVelocity() {
			Vector2 verticalVelocity = Vector2.zero;
			
			switch(state) {
				case CharacterState.State.DEFAULT:
				case CharacterState.State.WALKING:
				case CharacterState.State.KNOCKED_BACK:
				case CharacterState.State.JUMPING:
				case CharacterState.State.FALLING:
					verticalVelocity = Physics2D.gravity * (gravityModifier * Time.deltaTime);
					break;
				case CharacterState.State.CLIMBING_IDLE:
					break;
				case CharacterState.State.CLIMBING_UP:
					break;
				case CharacterState.State.CLIMBING_DOWN:
					break;
				case CharacterState.State.P_RIGHT_UP:
					break;
				case CharacterState.State.P_RIGHT:
					break;
				case CharacterState.State.P_RIGHT_DOWN:
					break;
				case CharacterState.State.P_LEFT_UP:
					break;
				case CharacterState.State.P_LEFT:
					break;
				case CharacterState.State.P_LEFT_DOWN:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return verticalVelocity;
		}
	}
}
