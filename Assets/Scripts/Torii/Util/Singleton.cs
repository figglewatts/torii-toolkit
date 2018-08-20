using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Util
{
    /// <summary>
    /// Implementation of Singleton pattern. Subclass if you want to use.
    /// </summary>
    /// <typeparam name="T">The type of the Singleton. Should be same as subclass type.</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance = null;

        /// <summary>
        /// Get the instance of this Singleton.
        /// </summary>
        public static T Instance => _instance ?? (_instance = new T());

        private Singleton()
        {
            // intentionally empty, singletons cannot be created
        }
    }
}
