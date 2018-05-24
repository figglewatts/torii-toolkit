using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Torii.Util;
using UnityEngine;

namespace Torii.UI
{
    public static class TUI
    {
        /// <summary>
        /// The path under "StreamingAssets" in which all UI data files (JSON) are contained.
        /// </summary>
        public static readonly string UIStreamingAssetsDirectory = "ui";

        /// <summary>
        /// The path where custom UI assets will be loaded from.
        /// Combines StreamingAssetsPath with UIStreamingAssetsDirectory.
        /// </summary>
        public static string UIUserDataDirectory
        {
            get { return PathUtil.Combine(Application.streamingAssetsPath, UIStreamingAssetsDirectory); }
        }

        /// <summary>
        /// The path inside Unity Resources folder where UI will be loaded from.
        /// This is to support UI data 'baked into' the game in the resource database,
        /// making it more difficult for users to modify.
        /// </summary>
        public static readonly string UIDataDirectory = "ui";

        public static void Initialize()
        {
            LoadUIData(UIDataDirectory);
            LoadUIUserData(UIUserDataDirectory);
        }

        private static void LoadUIData(string path)
        {

        }

        private static void LoadUIUserData(string path)
        {

        }
    }
}
