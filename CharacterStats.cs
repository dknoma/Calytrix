﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Character {

	/* Public/inspector elements */
//	public bool bogResist = false;
	public bool burnResist = false;
	public bool poisonResist = false;
	public bool stunResist = false;
	public bool silenceResist = false;

	private int maxLevel = 100;
	public int currentLevel = 1;

	public int expUntilLevelUp;
	public int currentExp = 0;

	private int maxHP = 10;
	public int currentHP;
	private int baseAtk = 5;
	public int currentAtk;
	private int baseDef = 5;
	public int currentDef;

//	private int basePAtk = 5;
//	public int currentPAtk = basePAtk;
//	public int baseMAtk = 5;
//	public int currentMAtk = baseMAtk;
//
//	private int basePDef = 5;
//	public int currentPDef = basePDef;
//	public int baseMDef = 5;
//	public int currentMDef = baseMDef;

//	private int baseSpd;
//	public int currentSpd;

	private StatusEffects characterStatus = new StatusEffects();
	private int[] statusTurnCounter;
	private int[] statChangeTurnCounter;
	private bool canAct = true;
	private bool canCastSpells = true;

	// Status effect constants
	private const int BURN = (int) StatusEffects.Status.Burn;
	private const int POISON = (int) StatusEffects.Status.Poison;
	private const int STUN = (int) StatusEffects.Status.Stun;
	private const int SILENCE = (int) StatusEffects.Status.Silence;

	private const int ATKUP = (int) StatusEffects.StatChange.ATKUp;
	private const int DEFUP = (int) StatusEffects.StatChange.DEFUp;
	private const int HPUP = (int) StatusEffects.StatChange.HPUp;
	private const int ATKDOWN = (int) StatusEffects.StatChange.ATKDown;
	private const int DEFDOWN = (int) StatusEffects.StatChange.DEFDown;
	private const int HPDESTRUCTION = (int) StatusEffects.StatChange.HPDestruct;

	public CharacterStats() {
		this.expUntilLevelUp = calcNextLevel (currentLevel);
		this.currentHP = this.maxHP;
		this.currentAtk = this.baseAtk;
		this.currentDef = this.baseDef;
		this.statusTurnCounter = new int[this.characterStatus.getStatusAfflictions().Length];
		this.statChangeTurnCounter = new int[this.characterStatus.getStatChangeAfflictions().Length];
	}

	/******
	 * HP *
	 ******/
	// Return a copy of this characters max hp
	public int getMaxHP() { return 0 + this.maxHP; }

	// Return a copy of this characters current attack
	public int getCurrentHP() { return 0 + this.currentHP; }

	// Modify current HP w/ new value
	public void modifyHP(int hp) { this.currentHP += hp; }
	// A % modifier for HP
	public void modifyHP(float hpPercentage) { this.currentHP += (int) Mathf.Round(this.maxHP * hpPercentage); }

	/*******
	 * ATK *
	 *******/
	// Return a copy of this characters base attack
	public int getBaseAtk() { return 0 + this.baseAtk; }

	// Return a copy of this characters current attack
	public int getCurrentAtk() { return 0 + this.currentAtk; }

	// Modify current attack w/ new value
	public void modifyAtk(bool up) { this.currentAtk += atkMod(this.baseAtk, up); }

//	public int getBasePAtk() { return 0 + this.basePAtk; }
//
//	// Return a copy of this characters current attack
//	public int getCurrentPAtk() { return 0 + this.currentPAtk; }
//
//	// Modify current attack w/ new value
//	public void modifyPAtk(bool up) { this.currentPAtk += atkMod(this.basePAtk, up); }
//
//	// Return a copy of this characters base attack
//	public int getBaseMAtk() { return 0 + this.baseMAtk; }
//
//	// Return a copy of this characters current attack
//	public int getCurrentMAtk() { return 0 + this.currentMAtk; }
//
//	// Modify current attack w/ new value
//	public void modifyMAtk(bool up) { this.currentMAtk += atkMod(this.baseMAtk, up); }

	/*******
	 * DEF *
	 *******/
	// Return a copy of this characters base defense
	public int getBaseDef() { return 0 + this.baseDef; }

	// Return a copy of this characters current defense
	public int getCurrentDef() { return 0 + this.currentDef; }

	// Modify current defense w/ new value
	public void modifyDef(bool up) { this.currentDef += defMod(this.baseDef, up); }
//	public int getBasePDef() { return 0 + this.basePDef; }
//
//	// Return a copy of this characters current defense
//	public int getCurrentPDef() { return 0 + this.currentPDef; }
//
//	// Modify current defense w/ new value
//	public void modifyPDef(bool up) { this.currentPDef += defMod(this.basePDef, up); }
//
//	// Return a copy of this characters base defense
//	public int getBaseMDef() { return 0 + this.baseMDef; }
//
//	// Return a copy of this characters current defense
//	public int getCurrentMDef() { return 0 + this.currentMDef; }
//
//	// Modify current defense w/ new value
//	public void modifyMDef(bool up) { this.currentMDef += defMod(this.baseMDef, up); }

	// Status effects
	public bool tryStatusAffliction(StatusEffects.Status status, int turnsToAfflict) {
		if(!doesResistStatus(status) && !this.characterStatus.afflictedByStatus(status)) {
			// TODO: afflict the state
			this.characterStatus.afflictStatus(status);
			this.statusTurnCounter[(int) status] = turnsToAfflict;
			return true;
		}
		return false;
	}

	public bool tryStatChange(StatusEffects.StatChange statChange, int turnsToAfflict) {
		if(!doesResistStatChange(statChange) && !this.characterStatus.afflictedByStatChange(statChange)) {
			// TODO: afflict the stat change
			this.characterStatus.afflictStatChange(statChange);
			this.statChangeTurnCounter [(int) statChange] = turnsToAfflict;
			return true;
		}
		return false;
	}

	public bool tryRemoveStatChange(StatusEffects.StatChange statChange) {
		if(!doesResistStatChangeRemoval(statChange) && !this.characterStatus.afflictedByStatChange(statChange)) {
			// TODO: afflict the stat change
			this.characterStatus.removeStatChange((int) statChange);
			this.statChangeTurnCounter [(int) statChange] = 0;
			return true;
		}
		return false;
	}

	public bool doesResistStatus(StatusEffects.Status status) {
		return this.characterStatus.resistsStatusEffect(status);
	}

	public bool doesResistStatChange(StatusEffects.StatChange statChange) {
		return this.characterStatus.resistsStatChange(statChange);
	}

	public bool doesResistStatChangeRemoval(StatusEffects.StatChange statChange) {
		return this.characterStatus.resistsStatChangeRemoval(statChange);
	}

	/**
	 * Initial state of the affliction. This method is used to initialize effects that only happen once
	 * to the character. checkStatusAfflictions() is used for effects that happen each turn.
	 */ 
	public void initAffliction(StatusEffects.Status status, int turns) {
		// Check status afflictions and do action depending on the affliction
		if (tryStatusAffliction (status, turns)) {
			switch ((int) status) {
			case BURN:
				if(tryStatChange(StatusEffects.StatChange.ATKDown, turns)) {
					modifyAtk (false); // Only need to happen once, not every turn
				} else {
					Debug.Log("Already afflicted by physical attack down...");
				}
				break;
			case POISON:
				if(tryStatChange(StatusEffects.StatChange.ATKDown, turns)) {
					modifyAtk (false); // Only need to happen once, not every turn
				} else {
					Debug.Log("Already afflicted by magical attack down...");
				}
				break;
			case STUN:
				this.canAct = false;
				break;
			case SILENCE:
				this.canCastSpells = false;
				break;
			}
		} else {
			Debug.Log("Already afflicted with status " + status);
		}
	}

	/**
	 * Removes the specified affliction. This method reverses any 
	 */ 
	public void removeAffliction(StatusEffects.Status status) {
		switch ((int) status) {
		case BURN:
			modifyAtk (true); // Only need to happen once, not every turn
			this.characterStatus.removeStatus(BURN);
			// Tries to remove atk down if not afflicted with a stat change removal resist
			if (tryRemoveStatChange (StatusEffects.StatChange.ATKDown)) {
				modifyAtk (true);// Only need to happen once, not every turn
			}
			break;
		case POISON:
			this.characterStatus.removeStatus (POISON);
			if (tryRemoveStatChange (StatusEffects.StatChange.ATKDown)) {
				modifyAtk (true);
			}
			break;
		case STUN:
			this.characterStatus.removeStatus(STUN);
			this.canAct = true;
			break;
		case SILENCE:
			this.characterStatus.removeStatus(SILENCE);
			this.canCastSpells = true;
			break;
		}
	}

	/**
	 * Check this characters status afflictions. This method is meant to be executed at the beginning of each of
	 * this character's turns.
	 */ 
	public void checkStatusAfflictions() {
		bool[] afflictions = this.characterStatus.getStatusAfflictions();
		for(int i = 0; i < afflictions.Length; i++) {
			// Check status afflictions and do action depending on the affliction
			switch (i) {
			case BURN:
				// 8% DoT & p atk down
				if (this.statusTurnCounter [BURN] > 0) {
					modifyHP (-0.08f);
				}
				break;
			case POISON:
				// 10% DoT & m atk down
				if (this.statusTurnCounter [BURN] > 0) {
					modifyHP (-0.1f);
				}
				break;
			case STUN:
				// Can't act for x turns
				this.canAct = false;
				break;
			case SILENCE:
				// Can't cast spells for x turns. Can still use skills
				this.canCastSpells = false;
				break;
			default:
				Debug.Log ("Affliction not valid");
				break;
			}
		}
	}

//	public void tryStatUp(StatusEffects.StatUps statUp) {
//		if(!status.alreadyAfflictedStatUp[statUp]) {
//			// TODO: afflict the stat up
//		}
//	}
//
//	public void tryStatDown(StatusEffects.StatDowns statDown) {
//		if(!status.alreadyAfflictedStatDown[statDown]) {
//			// TODO: afflict the stat down
//		}
//	}
		
	// Increase/decrease atk by 25% of the base value
	private int atkMod(int baseAtk, bool up) {
		return (int) (up ? baseAtk * 0.25f : baseAtk * (-0.25f));
	}

	// Increase/decrease defense by 25% of the base value
	private int defMod(int baseDef, bool up) {
		return (int) (up ? baseDef * 0.25f : baseDef * (-0.25f));
	}

	// Calculate the amount of exp required to get to the next level
	// 1->2: 1, 2->3: 5, 3->4: 33, 4->5: 73, ...
	private int calcNextLevel(int currentLevel) {
		return (int) Mathf.Round(4+(15*(Mathf.Pow(currentLevel, 3)))/14);
	}
}
