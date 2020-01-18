using System;
using UnityEngine;

namespace Characters {
	[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
	[Serializable]
	public class CharacterStats : ScriptableObject {
		private const int STARTING_HP = 4;
		private const int MAX_HP_ALLOWED = 8;
		private const int MAX_MP_ALLOWED = 8;
		private const int MAX_TEMP_HP = 2;
		
 		[SerializeField]
 		private int baseHp = STARTING_HP;
        private int maxHp;
        
        [Range(0, MAX_HP_ALLOWED)]
 		[SerializeField]
        private int currentHp;

        [Range(0, MAX_TEMP_HP)]
        [SerializeField]
        private int tempHp;
        
 		[SerializeField]
        private int baseMp = 6;
        private int maxMp;
        
        [Range(0, MAX_MP_ALLOWED)]
        [SerializeField]
        private int currentMp;
        
        public int CurrentHp => currentHp;
        public int CurrentMp => currentMp;
        public int TempHp => tempHp;
        public int BaseHp => baseHp;
        public int BaseMp => baseMp;

        public CharacterStats() {
	        this.maxHp = baseHp;
	        this.maxMp = baseMp;
	        this.currentHp = baseHp;
	        this.currentMp = baseMp;
	        this.tempHp = 0;
        }

        public void IncreaseBaseHp() {
	        this.baseHp = Mathf.Clamp(baseHp + 1, STARTING_HP, MAX_HP_ALLOWED);
	        this.currentHp = baseHp;
	        Debug.Log($"baseHp={baseHp}");
        }

        public void IncreaseCurrentHp(int hp) {
	        this.currentHp = Mathf.Clamp(currentHp + hp, 0, maxHp);
        }

        public void DamageCharacter(int dmg) {
	        int temp = tempHp - dmg;
	        int tempDiff = temp < 0 ? Mathf.Abs(temp) : 0;
	        if(tempHp > 0) {
		        this.tempHp = Mathf.Clamp(tempHp - dmg, 0, MAX_HP_ALLOWED);
		        if(tempDiff > 0) {
			        this.currentHp = Mathf.Clamp(currentHp - tempDiff, 0, maxHp);
		        }
	        } else {
		        this.currentHp = Mathf.Clamp(currentHp - dmg, 0, maxHp);
	        }
        }
        
        public void DecreaseCurrentHp(int hp) {
	        this.currentHp = Mathf.Clamp(currentHp - hp, 0, maxHp);
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
	        this.tempHp = Mathf.Clamp(tempHp  + temp, 0, MAX_TEMP_HP);;
        }

        public void DecreaseTempHp(int temp) {
	        this.tempHp = Mathf.Clamp(tempHp - temp, 0, MAX_HP_ALLOWED);
        }

        public void ResetTempHp(int temp) {
	        this.tempHp = 0;
        }
 	}
 }
