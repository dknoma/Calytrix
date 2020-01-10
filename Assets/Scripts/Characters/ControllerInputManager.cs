using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Characters.InputConstants;

namespace Characters {
	public static class ControllerInputManager {
		private static string vertical = VERTICAL_INPUT_NAME;
		private static string horizontal = HORIZONTAL_INPUT_NAME;
		private static string jump = JUMP_INPUT_NAME;
		private static string grab;
		private static string fire;
		private static string pause;
		private static string weapons;
		private static string submit;
		private static string cancel;
		private static bool useController;

		private static readonly IList<string> CONTROLLERS = new List<string>();
		private static readonly IDictionary<string, InputDevice> CONTROLLERS_BY_NAME = new Dictionary<string, InputDevice>();

		private const string KEYBOARD = "keyboard";
		
		private static string currentController = KEYBOARD;

		public static void InitControllers() {
			foreach(string controller in Input.GetJoystickNames()) {
				CONTROLLERS.Add(controller);
			}

			if(CONTROLLERS.Count > 0) {
				currentController = CONTROLLERS[0];
				useController = true;
			}

			var devices = InputSystem.devices;
			foreach(InputDevice inputDevice in devices) {
				CONTROLLERS_BY_NAME.Add(inputDevice.name, inputDevice);
				
//				Debug.Log($"device=[{inputDevice.name}], [{inputDevice.displayName}], [{inputDevice.shortDisplayName}]");
//				
//				if(inputDevice.name.Equals(PS4)) {
//					foreach(InputControl inputDeviceAllControl in inputDevice.allControls) {
//						Debug.Log($"inputDeviceAllControl={inputDeviceAllControl}");
//					}
//				}
			}
		}

		public static float GetRawHorizontal() {
			return Input.GetAxisRaw(Horizontal());
		}
		
		public static float GetHorizontal() {
			return Input.GetAxis(Horizontal());
		}

		public static void VerticalControl() {
			vertical = useController ? "PS4_DPadVertical" : VERTICAL_INPUT_NAME;
		}

		public static string Vertical() {
			return vertical;
		}

		public static void HorizontalControl() {
			horizontal = useController ? "PS4_DPadHorizontal" : HORIZONTAL_INPUT_NAME;
		}

		public static string Horizontal() {
			return horizontal;
		}

		public static void JumpControl() {
			if(useController) {
				jump = JUMP_INPUT_NAME;
			} else {
				jump = JUMP_INPUT_NAME;
			}
		}

		public static string Jump() {
			return jump;
		}

		public static bool Windows() {
			return Application.platform == RuntimePlatform.WindowsPlayer || 
			       Application.platform == RuntimePlatform.WindowsEditor;
		}

		public static bool OSX() {
			return Application.platform == RuntimePlatform.OSXPlayer ||
			       Application.platform == RuntimePlatform.OSXEditor;
		}

		public static void GrabControl(bool useController) {
			if(useController) {
				if (Windows()) {
					grab = "PS4_L2";
				} else if (OSX()) {
					grab = "PS4_R2";
				}
			} else {
				grab = "Fire2";
			}
		}

		public static string Grab() {
			return grab;
		}

		public static void FireControl(bool useController) {
			if(useController) {
				fire = "Fire1";
			} else {
				fire = "Fire1";
			}
		}

		public static string Fire() {
			return fire;
		}

		public static void PauseControl(bool useController) {
			if(useController) {
				pause = "PS4_Options";
			} else {
				pause = "Cancel";
			}
		}

		public static string Pause() {
			return pause;
		}

		public static void WeaponsControl(bool useController) {
			if(useController) {
				weapons = "PS4_Triangle";
			} else {
				weapons = "AltMenu";
			}
		}

		public static string Weapons() {
			return weapons;
		}

		public static void SubmitControl(bool useController) {
			if(useController) {
				submit = "Submit";
			} else {
				submit = "Submit";
			}
		}

		public static string Submit() {
			return submit;
		}

		public static void CancelControl(bool useController) {
			if(useController) {
				cancel = "Cancel";
			} else {
				cancel = "Cancel";
			}
		}

		public static string Cancel() {
			return cancel;
		}
	}
}
