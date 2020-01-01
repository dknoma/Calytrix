using UnityEngine;

namespace Characters {
	public static class ControllerInputManager {
		private static string vertical = "Vertical";
		private static string horizontal = "Horizontal";
		private static string jump = "Jump";
		private static string grab;
		private static string fire;
		private static string pause;
		private static string weapons;
		private static string submit;
		private static string cancel;

		public static void VerticalControl(bool useController) {
			vertical = useController ? "PS4_DPadVertical" : "Vertical";
		}

		public static string Vertical() {
			return vertical;
		}

		public static void HorizontalControl(bool useController) {
			horizontal = useController ? "PS4_DPadHorizontal" : "Horizontal";
		}

		public static string Horizontal() {
			return horizontal;
		}

		public static void JumpControl(bool useController) {
			if(useController) {
				jump = "Jump";
			} else {
				jump = "Jump";
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
