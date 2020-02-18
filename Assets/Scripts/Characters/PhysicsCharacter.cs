using System;
using Characters;
using UnityEngine;

public class PhysicsCharacter : PhysicsObject {

    protected override Vector2 ComputeVerticalVelocity() {
        Vector2 verticalVelocity = Vector2.zero;
		
        switch(state) {
            case CharacterState.State.DEFAULT:
            case CharacterState.State.WALKING:
            case CharacterState.State.KNOCKED_BACK:
            case CharacterState.State.JUMPING:
            case CharacterState.State.FALLING:
                verticalVelocity = Physics2D.gravity * (gravityModifier * Time.deltaTime);
                break;
            
            case CharacterState.State.CLIMBING_IDLE:
                break;
            case CharacterState.State.CLIMBING_UP:
                break;
            case CharacterState.State.CLIMBING_DOWN:
                break;
            
            case CharacterState.State.PINNING:
                verticalVelocity = Physics2D.gravity * (gravityModifier * Time.deltaTime);
                break;
            
            case CharacterState.State.PIN_RELEASE:
                break;
            
            case CharacterState.State.P_RIGHT_UP:
            case CharacterState.State.P_RIGHT:
            case CharacterState.State.P_RIGHT_DOWN:
            case CharacterState.State.P_LEFT_UP:
            case CharacterState.State.P_LEFT:
            case CharacterState.State.P_LEFT_DOWN:
                verticalVelocity = Vector2.zero;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return verticalVelocity;
    }
}
