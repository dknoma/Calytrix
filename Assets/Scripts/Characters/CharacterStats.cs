using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters {
	[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
	[Serializable]
	public class CharacterStats : ScriptableObject {
		private const int MAX_HP_ALLOWED = 8;
		private const int MAX_MP_ALLOWED = 8;
		
 		[SerializeField]
 		private int baseHp = 4;
        private int maxHp;
        
 		[SerializeField]
        private int currentHp;


        private int tempHp;
        
 		[SerializeField]
        private int baseMp = 15;
        private int maxMp;
        
        [SerializeField]
        private int currentMp;
        
        public int CurrentHp => currentHp;
        public int CurrentMp => currentMp;

        public CharacterStats() {
	        this.maxHp = baseHp;
	        this.maxMp = baseMp;
	        this.currentHp = baseHp;
	        this.currentMp = baseMp;
	        this.tempHp = 0;
        }

        public void IncreaseBaseHp() {
	        this.baseHp = Mathf.Clamp(baseHp + 1, 4, MAX_HP_ALLOWED);
	        this.currentHp = baseHp;
	        Debug.Log($"baseHp={baseHp}");
        }

        public void IncreaseCurrentHp(int hp) {
	        this.currentHp = Mathf.Clamp(currentHp - hp, 0, maxHp);
        }

        public void DecreaseCurrentHp(int hp) {
	        this.currentHp = Mathf.Clamp(currentHp + hp, 0, maxHp);
        }

        public void IncreaseBaseMp() {
	        this.baseMp = Mathf.Clamp(baseMp + 1, 4, MAX_MP_ALLOWED);
	        this.currentMp = baseMp;
        }

        public void IncreaseCurrentMp(int mp) {
	        this.currentMp = Mathf.Clamp(currentMp + mp, 0, maxMp);
        }

        public void DecreaseCurrentMp(int mp) {
	        this.currentMp = Mathf.Clamp(currentMp - mp, 0, maxMp);
        }

        public void IncreaseTempHp(int temp) {
	        this.tempHp += temp;
        }

        public void DecreaseTempHp(int temp) {
	        this.tempHp = Mathf.Clamp(tempHp - temp, 0, MAX_HP_ALLOWED);
        }

        public void ResetTempHp(int temp) {
	        this.tempHp = 0;
        }
 	}
 }
