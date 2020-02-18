namespace Characters {
	public static class CharacterState {
		public enum State {
			DEFAULT,
			WALKING,
			
			JUMPING,
			FALLING,
			
			CLIMBING_IDLE,
			CLIMBING_UP,
			CLIMBING_DOWN,
			
			KNOCKED_BACK,
			
			PINNING,
			PIN_RELEASE,
			
			P_RIGHT_UP,
			P_RIGHT,
			P_RIGHT_DOWN,
			P_LEFT_UP,
			P_LEFT,
			P_LEFT_DOWN,
		}

		public static State Default() {
			return State.DEFAULT;
		}

		public static State Jumping() {
			return State.JUMPING;
		}
		
		public static State Falling() {
			return State.FALLING;
		}

		public static bool isPinning(State state) {
			return state == State.PINNING;
		}

		public static bool IsGrounded(State state) {
			return state == State.DEFAULT || state == State.WALKING;
		}

		public static bool IsIdling(State state) {
			return state == State.DEFAULT;
		}

		public static bool IsWalking(State state) {
			return state == State.WALKING;
		}

		public static bool IsJumping(State state) {
			return state == State.JUMPING;
		}

		public static bool IsFalling(State state) {
			return state == State.FALLING;
		}

		public static bool IsClimbingIdle(State state) {
			return state == State.CLIMBING_IDLE;
		}

		public static bool IsClimbingUp(State state) {
			return state == State.CLIMBING_UP;
		}

		public static bool IsClimbingDown(State state) {
			return state == State.CLIMBING_DOWN;
		}

		public static bool IsKnockedBack(State state) {
			return state == State.KNOCKED_BACK;
		}

		public static bool IsPinnedRU(State state) {
			return state == State.P_RIGHT_UP;
		}

		public static bool IsPinnedR(State state) {
			return state == State.P_RIGHT;
		}

		public static bool IsPinnedRD(State state) {
			return state == State.P_RIGHT_DOWN;
		}

		public static bool IsPinnedLU(State state) {
			return state == State.P_LEFT_UP;
		}

		public static bool IsPinnedL(State state) {
			return state == State.P_LEFT;
		}

		public static bool IsPinnedLD(State state) {
			return state == State.P_LEFT_DOWN;
		}
	}
}
