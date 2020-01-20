using UnityEngine;

namespace Utility {
    public static class GameObjectExtensions {
        public static T TryAddComponent<T>(this GameObject obj) where T : MonoBehaviour{
            if(!obj.TryGetComponent(out T comp)) {
                comp = obj.AddComponent<T>();
            }
            
            return comp;
        }
    }
}