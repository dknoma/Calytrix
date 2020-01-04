using Characters.Allies;
using Utility;

// ReSharper disable InconsistentNaming

namespace Music {
	public static class PlayerSFXManager {
		public static void PlayerJumpSFX(this PlayerController player) {
			AkSoundEngine.PostEvent(Tags.SFXTags.JUMP, player.gameObject);
		}
	}
}