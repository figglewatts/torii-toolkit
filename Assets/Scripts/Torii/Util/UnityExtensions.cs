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
    }
}
