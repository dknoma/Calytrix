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
			LEFT_UP,
			DEFAULT
		}

		public static bool InputRight(InputAngleState input) {
			return input == InputAngleState.RIGHT || 
			       input == InputAngleState.RIGHT_UP ||
			       input == InputAngleState.RIGHT_DOWN;
		}
		

		public static bool InputLeft(InputAngleState input) {
			return input == InputAngleState.LEFT || 
			       input == InputAngleState.LEFT_UP ||
			       input == InputAngleState.LEFT_DOWN;
		}

//		public const int UP = 0b0001;
//		public const int DOWN = 0b0010;
//		public const int LEFT = 0b0100;
//		public const int RIGHT = 0b1000;
//
//		public const int UP_MASK = 0b1101;
//		public const int DOWN_MASK = 0b1110;
//		public const int LEFT_MASK = 0b0111;
//		public const int RIGHT_MASK = 0b1011;
	}
}