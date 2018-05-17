using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Torii.UI
{
    public static class TUI
    {
        /// <summary>
        /// The path under "StreamingAssets" in which all UI data files (JSON) are contained.
        /// </summary>
        public static string UIStreamingAssetsDirectory = "ui";

        public static string UIDirectory
        {
            get { return Path.Combine(Application.streamingAssetsPath, UIStreamingAssetsDirectory); }
        }

        public static void Initialize()
        {
            LoadUIData(UIStreamingAssetsDirectory);
        }

        private static void LoadUIData(string path)
        {

        }
    }
}
