using Characters;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour {
    [SerializeField] private CharacterStats stats;
    [SerializeField] private Signal hpSignal;
    [SerializeField] private Signal mpSignal;

    private PCInputActions pcInputActions;

    private void Awake() {
        this.pcInputActions = new PCInputActions();
        
        InitInput();
    }

    private void OnDisable() {
        pcInputActions.Disable();
    }

    private void InitInput() {
        pcInputActions.Player.Action2.performed += OnAction2;
			
        pcInputActions.Enable();
    }
		
    private void OnAction2(CallbackContext ctx) {
        OnChangeHP(1);
    }
    
    public void OnChangeHP(int value) {
        hpSignal.DoSignal();
    }
    
    public void OnChangeMP(int value) {
        mpSignal.DoSignal();
    }
}
