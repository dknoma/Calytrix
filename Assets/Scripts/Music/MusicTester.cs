using UnityEngine;

namespace Music {
	public class MusicTester : MonoBehaviour {
		private void Start() {
			// play music from event name
			AkSoundEngine.PostEvent("mad_forest", gameObject);
			
			Debug.Log(typeof(int[]).FullName);
			Debug.Log(typeof(int[]).Name);
			Debug.Log(typeof(int[]).AssemblyQualifiedName);
//			Debug.Log(typeof(int[]).);
			Debug.Log(typeof(CodeBuilderUtil.Modifier).FullName);
			Debug.Log(typeof(CodeBuilderUtil.Modifier).Name);
			Debug.Log(typeof(CodeBuilderUtil.Modifier).AssemblyQualifiedName);
		}
	}
}