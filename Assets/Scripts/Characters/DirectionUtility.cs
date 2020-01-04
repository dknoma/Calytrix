namespace Characters {
	public static class DirectionUtility {
		public enum FacingState {
			RIGHT,
			LEFT
		}

		public static FacingState Right() {
			return FacingState.RIGHT;
		}

		public static FacingState Left() {
			return FacingState.LEFT;
		}

		public enum InputAngleState {
			UP,
			RIGHT_UP,
			RIGHT,
			RIGHT_DOWN,
			DOWN,
			LEFT_DOWN,
			LEFT,
			LEFT_UP
		}
	}
}