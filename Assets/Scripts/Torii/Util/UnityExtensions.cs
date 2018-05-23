using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Torii.Util
{
    public static class UnityExtensions
    {
        #region Sprite

        public static bool HasBorder(this Sprite sprite)
        {
            return !sprite.border.Equals(Vector4.zero);
        }

        #endregion

        #region GameObject

        public static T[] GetComponentsInChildrenNonRecursive<T>(this GameObject obj)
        {
            List<T> componentList = new List<T>();

            foreach (Transform child in obj.transform)
            {
                componentList.AddRange(child.GetComponents<T>());
            }

            return componentList.ToArray();
        }

        #endregion

        #region MonoBehaviour

        public static T[] GetComponentsInChildrenNonRecursive<T>(this MonoBehaviour m)
        {
            return m.gameObject.GetComponentsInChildrenNonRecursive<T>();
        }

        #endregion
    }
}
