using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace ZYTools
{
    public class SortHierarchyObject
    {
        #region Execute
        [MenuItem("GameObject/Sort/OrderByNameUp", false, priority = -11)]
        static void ExecuteUp()
        {
            new SortHierarchyObject().Sort(OrderByNameUp());
        }

        [MenuItem("GameObject/Sort/OrderByNameDown", false, priority = -11)]
        static void ExecuteDown()
        {
            new SortHierarchyObject().Sort(OrderByNameDown());
        }
        #endregion
        #region SortSelectionObject
        void Sort(Comparison<Transform> Comparer)
        {
            List<Transform> objs = new List<Transform>();

            foreach (var item in Selection.transforms)
            {
                objs.Add(item);
            }

            objs.Sort(Comparer);

            foreach (var item in objs)
            {
                item.SetSiblingIndex(objs.IndexOf(item));
            }
        }
        #endregion
        #region OrderMethods
        private static Comparison<Transform> OrderByNameDown()
        {
            return (Transform a, Transform b) =>
            {
                return b.name.CompareTo(a.name);
            };
        }
        private static Comparison<Transform> OrderByNameUp()
        {
            return (Transform a, Transform b) =>
            {
                return a.name.CompareTo(b.name);
            };
        }
        #endregion
    }
}
#endif