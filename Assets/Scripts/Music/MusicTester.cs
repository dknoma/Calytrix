using UnityEngine;
using Utility.Codegen;

namespace Music {
	public class MusicTester : MonoBehaviour {
		private void Start() {
			// play music from event name
//			AkSoundEngine.PostEvent("mad_forest", gameObject);
			AkSoundEngine.PostEvent("compo", gameObject);

			CodeBlock block = CodeBlock.NewBuilder()
			                             .AddStatement("int sum = 0")
										 .BeginControlFlow("for(int i = 0; i <= 10; i++)")
											.AddStatement("sum += i")
										 .EndControlFlow()
			                             .Build();
			
			Debug.Log(block);
		}
	}
}