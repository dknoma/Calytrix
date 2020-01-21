using System.Collections.Generic;
using UnityEngine;

namespace Backgrounds {
    public class BackgroundListData : ScriptableObject {
        [SerializeField] private List<GameObject> backgroundPrefabs = new List<GameObject>();
        [SerializeField] [DisableInspectorEdit] private string m_listName;

        public string listName {
            get => m_listName;
            set => this.m_listName = value;
        }

        public IEnumerable<GameObject> GetBackgroundPrefabs() {
            return backgroundPrefabs;
        }

        public void AddBackground(GameObject bg) {
            if(bg != null && bg != default) {
                backgroundPrefabs.Add(bg);
            }
        }

        public GameObject GetBackground(int index) {
            GameObject obj = default;
            if(index < backgroundPrefabs.Count) {
                obj = backgroundPrefabs[index];
            }
            return obj;
        }
    }
}