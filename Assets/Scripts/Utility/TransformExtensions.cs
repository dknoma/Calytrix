using System.Linq;
using UnityEngine;
using static UnityEngine.Object;

namespace Utility {
    public static class TransformExtensions {
        public static void RemoveAllChildren(this Transform transform) {
            var children = transform.Cast<Transform>().ToList();
            
            foreach(Transform child in children) {
                Debug.Log($"Destroying {child.name}");
                DestroyImmediate(child.gameObject);
            }
        }
    }
}