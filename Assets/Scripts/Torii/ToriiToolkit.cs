using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torii.Resource;

namespace Torii
{
    public static class ToriiToolkit
    {
        /// <summary>
        /// The directory under "StreamingAssets" where Torii data files are stored.
        /// </summary>
        public static readonly string StreamingAssetsDataDirectory = "toriitoolkit";

        public static void Initialize()
        {
            ResourceManager.Initialize();
        }
    }
}
