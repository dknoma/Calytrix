﻿using System;
using UnityEngine;
using UnityEngine.UI;

public enum Transition {
	None,
	Fade,
	Triangle,
	HorizontalDistortion,
	Sawtooth,
	Angular,
	Bubble
}

[ExecuteInEditMode]
public class ScreenTransitionManager : MonoBehaviour {

	public static ScreenTransitionManager transitionManager;

	public bool inDebug;
	
	public Transition transition = Transition.Triangle;
	public Material defaultTexture;
	public Material triangleTransitionMaterial;
	public Material horizontalDistortionTransitionMaterial;
	public Material sawtoothTransitionMaterial;
	public Material angularTransitionMaterial;
	public GameObject debugTextbox;
	
	private bool isTransitioningToBlack;
	private bool isTransitioningFromBlack;
	private float transitionCounter;
	private float transitionCutoff;
	private Text debugText;
	private readonly bool[] implemented = new bool[(int)Transition.Bubble + 1];
	private static readonly int Cutoff = Shader.PropertyToID("_Cutoff");
	private static readonly int Reverse = Shader.PropertyToID("_Reverse");

	private void OnEnable() {
		if (transitionManager == null) {
			transitionManager = this;
		} else if (transitionManager != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		ResetMaterial(triangleTransitionMaterial);
		ResetMaterial(horizontalDistortionTransitionMaterial);
		ResetMaterial(sawtoothTransitionMaterial);
		ResetMaterial(angularTransitionMaterial);
		implemented[(int) Transition.Triangle] = true;
		implemented[(int) Transition.HorizontalDistortion] = true;
		implemented[(int) Transition.Sawtooth] = true;
		implemented[(int) Transition.Angular] = true;
		debugText = debugTextbox.GetComponent<Text>();
	}

	private void Update() {
		if (!inDebug) return;
		TestButtons();
		if (debugText == null) return;
		debugText.text = string.Format("Press Tab to change:\n{0}", transition.ToString());
	}

	private void FixedUpdate() {
		TransitionScreen();
	}
	
	/// <summary>
	/// Render the material graphic based on current Transition state
	/// </summary>
	/// <param name="src"></param>
	/// <param name="dest"></param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	private void OnRenderImage(RenderTexture src, RenderTexture dest) {
		switch (transition) {
			case Transition.Fade:
				Graphics.Blit(src, dest, defaultTexture);
				break;
			case Transition.Triangle:
				if (triangleTransitionMaterial != null) {
					Graphics.Blit(src, dest, triangleTransitionMaterial);
				}
				break;
			case Transition.HorizontalDistortion:
				if (horizontalDistortionTransitionMaterial != null) {
					Graphics.Blit(src, dest, horizontalDistortionTransitionMaterial);
				}
				break;
			case Transition.Sawtooth:
				if (sawtoothTransitionMaterial != null) {
					Graphics.Blit(src, dest, sawtoothTransitionMaterial);
				}
				break;
			case Transition.Angular:
				if (angularTransitionMaterial != null) {
					Graphics.Blit(src, dest, angularTransitionMaterial);
				}
				break;
			case Transition.Bubble:
				Graphics.Blit(src, dest, defaultTexture);
				break;
			case Transition.None:
				Graphics.Blit(src, dest, defaultTexture);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	
	private void TestButtons() {
		if (Input.GetButtonDown("Fire2")) {
			if (!implemented[(int) transition]) return;
			isTransitioningToBlack = true;
		} else if (Input.GetButtonDown("Fire3")) {
			if (!implemented[(int) transition]) return;
			isTransitioningFromBlack = true;
		} else if (Input.GetButtonDown("Tab")) {
			switch (transition) {
				case Transition.Fade:
					transition = Transition.Triangle;
					break;
				case Transition.Triangle:
					transition = Transition.HorizontalDistortion;
					break;
				case Transition.HorizontalDistortion:
					transition = Transition.Sawtooth;
					break;
				case Transition.Sawtooth:
					transition = Transition.Angular;
					break;
				case Transition.Angular:
					transition = Transition.Bubble;
					break;
				case Transition.Bubble:
					transition = Transition.Fade;
					break;
				case Transition.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
	
	/// <summary>
	/// Transition the screen depending on the Transition state
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	private void TransitionScreen() {
		switch (transition) {
			case Transition.Fade:
				break;
			case Transition.Triangle:
				NormalScreenTransition(triangleTransitionMaterial, 0.02f);
				break;
			case Transition.HorizontalDistortion:
				NormalScreenTransition(horizontalDistortionTransitionMaterial, 0.02f);
				break;
			case Transition.Sawtooth:
				NormalScreenTransition(sawtoothTransitionMaterial, 0.03f);
				break;
			case Transition.Angular:
				NormalScreenTransition(angularTransitionMaterial, 0.02f);
				break;
			case Transition.Bubble:
				break;
			case Transition.None:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void NormalScreenTransition(Material mat, float cutoffSpeed) {
		// Smoothly transition to black
		if (isTransitioningToBlack) {
			mat.SetFloat(Reverse, 0);
			transitionCutoff = Mathf.Clamp(transitionCutoff + cutoffSpeed, 0, 1); // Set max of cutoff to 1
			mat.SetFloat(Cutoff, transitionCutoff);
			if (Mathf.Abs(transitionCutoff - 1) < Mathf.Epsilon) {
				isTransitioningToBlack = false;
			}
		} else {
			// Smoothly transition from black
			if (isTransitioningFromBlack) {
				mat.SetFloat(Reverse, 1);
				transitionCutoff = Mathf.Clamp(transitionCutoff - cutoffSpeed, 0, 1); // Set max of cutoff to 1
				mat.SetFloat(Cutoff, transitionCutoff);
				if (transitionCutoff < Mathf.Epsilon) {
					isTransitioningFromBlack = false;
				}
			}   
		}
	}

	private static void ResetMaterial(Material mat) {
		if (mat == null) return;
		mat.SetFloat(Reverse, 0);
		mat.SetFloat(Cutoff, 0);
	}
}
