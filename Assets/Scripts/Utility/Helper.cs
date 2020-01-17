using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility {
    public static class Helper {

        // GameObject
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T:Component{
            Transform t = parent.transform;
            T res = null;
            foreach(Transform tr in t) {
                if(tr.CompareTag(tag)) {
                    res = tr.GetComponent<T>();
                    break;
                }
            }
            return res;
        }
    
        public static T FindComponentInSiblingsWithTag<T>(this GameObject me, string tag) where T:Component{
            Transform parent = me.transform.parent;
            T res = null;
            foreach(Transform sib in parent) {
                if(sib.CompareTag(tag) && !sib.Equals(me.transform)) {
                    res = sib.GetComponent<T>();
                    break;
                }
            }
            return res;
        }

        public static bool IsNull<T>(this T obj )where T:Component {
            return EqualityComparer<T>.Default.Equals(obj,default);
        }
    
        public static bool IsObjectNull<T>(T obj )  {
            return EqualityComparer<T>.Default.Equals(obj,default);
        }

        public static bool IsReallyNull<T>(this T obj) {
            return ReferenceEquals(obj, null);
        }
    
        // Enum
        public static string ToLowerCase(this Enum e) {
            return e.ToString().ToLower();
        }

        // LinkedList
        public static bool IsFirst<T>(this LinkedList<T> list, T obj) {
            return obj.Equals(list.First);
        }

        public static bool IsLast<T>(this LinkedList<T> list, T obj) {
            return obj.Equals(list.Last);
        }

        public static T SetFirstAsLast<T>(this LinkedList<T> list) {
            LinkedListNode<T> first = list.First;
            list.RemoveFirst();
            list.AddLast(first);
            return first.Value;
        }

        public static T SetLastAsFirst<T>(this LinkedList<T> list) {
            LinkedListNode<T> last = list.Last;
            list.RemoveLast();
            list.AddFirst(last);
            return last.Value;
        }
    }
}
