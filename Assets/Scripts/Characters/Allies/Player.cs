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
        pcInputActions.Player.Action1.performed += OnAction1;
        pcInputActions.Player.Action2.performed += OnAction2;
			
        pcInputActions.Enable();
    }
		
    private void OnAction1(CallbackContext ctx) {
        stats.DecreaseCurrentHp(1);
        OnChangeHP();
    }
		
    private void OnAction2(CallbackContext ctx) {
        stats.IncreaseCurrentHp(1);
        OnChangeHP();
    }
    
    public void OnChangeHP() {
        hpSignal.DoSignal();
    }
    
    public void OnChangeMP() {
        mpSignal.DoSignal();
    }
}
