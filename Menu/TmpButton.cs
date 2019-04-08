﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace Menu {
	public class TmpButton : Button {
	
		private TextMeshProUGUI text;
		private Vector3 textLocalPos;

		private const Transition MY_TRANSITION = Transition.SpriteSwap;

		protected override void Start() {
			transition = MY_TRANSITION;
			if(text == null) {
				text = GetComponentInChildren<TextMeshProUGUI>();
				Vector3 localPos = text.transform.localPosition;
				textLocalPos = new Vector3(localPos.x + 0.5f, localPos.y, localPos.z-1);
				Debug.LogFormat("null: {0}", textLocalPos);
			} else {
				textLocalPos = new Vector3(0.5f, 0, -1);
				Debug.LogFormat("local: {0}", textLocalPos);
			}
		}

		//TODO: change state transition to let button know when to change text transform
		protected override void DoStateTransition(SelectionState state, bool instant) {
			Sprite newSprite;
			switch (state) {
				case SelectionState.Normal:
					Debug.LogFormat("Normal {0}", name);
					if(text == null) {
						text = GetComponentInChildren<TextMeshProUGUI>();
						Vector3 localPos = text.transform.localPosition;
						textLocalPos = new Vector3(localPos.x + 0.5f, localPos.y, localPos.z-1);
						Debug.LogFormat("init: {0}", textLocalPos);
					}
					text.transform.localPosition = textLocalPos;
					Debug.LogFormat("text local pos: {0}, {1}", textLocalPos, text.transform.localPosition);
					newSprite = null;
					break;
				case SelectionState.Highlighted:
					Debug.LogFormat("Highlighting {0}", name);
					newSprite = spriteState.highlightedSprite;
					text.transform.localPosition = new Vector3(0, 0.5f, -1);
					break;
				case SelectionState.Pressed:
					Debug.LogFormat("Pressing {0}", name);
					newSprite = spriteState.pressedSprite;
					text.transform.localPosition = textLocalPos;
					break;
				case SelectionState.Disabled:
					Debug.LogFormat("Disabling {0}", name);
					newSprite = spriteState.disabledSprite;
					break;
				default:
					newSprite = null;
					break;
			}
			if (!gameObject.activeInHierarchy)
				return;
			switch (transition) {
				case Transition.SpriteSwap:
					DoSpriteSwap(newSprite);
					break;
			}
		}
    
		private void DoSpriteSwap(Sprite newSprite) {
			if (image == null)
				return;
			image.overrideSprite = newSprite;
		}
	}
}