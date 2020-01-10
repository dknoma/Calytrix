namespace Characters {
	public static class InputConstants {
		public const string JUMP_INPUT_NAME = "Jump";
		public const string HORIZONTAL_INPUT_NAME = "Horizontal";
		public const string VERTICAL_INPUT_NAME = "Vertical";
		
		public const string PS4 = "DualShock4GamepadHID";

		public enum JoystickInputs {
			JOYSTICK_BUTTON_0,
			JOYSTICK_BUTTON_1,
			JOYSTICK_BUTTON_2,
			JOYSTICK_BUTTON_3,
			JOYSTICK_BUTTON_4,
			JOYSTICK_BUTTON_5,
			JOYSTICK_BUTTON_6,
			JOYSTICK_BUTTON_7,
			JOYSTICK_BUTTON_8,
			JOYSTICK_BUTTON_9,
			JOYSTICK_BUTTON_10,
			JOYSTICK_BUTTON_11,
			JOYSTICK_BUTTON_12,
			JOYSTICK_BUTTON_13,
			JOYSTICK_BUTTON_14,
			JOYSTICK_BUTTON_15,
			JOYSTICK_BUTTON_16,
			JOYSTICK_BUTTON_17,
			JOYSTICK_BUTTON_18,
			JOYSTICK_BUTTON_19,
			JOYSTICK_BUTTON_20
		}

		// Vector2(horizontal, vertical)
		public enum DirectionInput {
			UP,
			DOWN,
			LEFT,
			RIGHT
		}
		
		public enum ActionInput {
			ACTION,
			JUMP,
			PAUSE,
			INVENTORY
		}
	}
}
