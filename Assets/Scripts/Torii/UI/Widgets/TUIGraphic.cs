using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI.Widgets
{
    public class TUIGraphic : TUIWidget
    {
        public Graphic Graphic { get; set; }

        public Color Color
        {
            get { return Graphic.color; }
            set { Graphic.color = value; }
        }
    }
}
