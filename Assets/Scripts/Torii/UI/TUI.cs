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
        /// The path where UI assets will be loaded from.
        /// Combines StreamingAssetsPath with UIStreamingAssetsDirectory.
        /// </summary>
        public static string UIDirectory
        {
            get { return PathUtil.Combine(Application.streamingAssetsPath, UIStreamingAssetsDirectory); }
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
