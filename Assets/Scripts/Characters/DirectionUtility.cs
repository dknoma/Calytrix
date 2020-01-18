using static Characters.DirectionUtility.InputAngleState;

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
			return input == RIGHT || 
			       input == RIGHT_UP ||
			       input == RIGHT_DOWN;
		}
		

		public static bool InputLeft(InputAngleState input) {
			return input == LEFT || 
			       input == LEFT_UP ||
			       input == LEFT_DOWN;
		}

		public static InputAngleState CalculateInputAngle(float x, float y) {
			InputAngleState angleState = DEFAULT;
			if(x >= 0.5 && y >= 0.5) {
				angleState = RIGHT_UP;
			} else if(x >= 0.5 && MagnitudeWithinRange(y)) {
				angleState = RIGHT;
			} else if(x >= 0.5 && y <= -0.5) {
				angleState = RIGHT_DOWN;
			} else if(MagnitudeWithinRange(x) && y <= -0.5) {
				angleState = DOWN;
			} else if(x <= -0.5 && y <= -0.5) {
				angleState = LEFT_DOWN;
			} else if(x <= -0.5 && MagnitudeWithinRange(y)) {
				angleState = LEFT;
			} else if(x <= -0.5 && y >= 0.5) {
				angleState = LEFT_UP;
			} else if(MagnitudeWithinRange(x) && y >= 0.5) {
				angleState = UP;
			} else {
				angleState = DEFAULT;
			}
			
			return angleState;
		}
		
		private static bool MagnitudeWithinRange(float mag) {
			return -0.5 <= mag && mag <= 0.5;
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