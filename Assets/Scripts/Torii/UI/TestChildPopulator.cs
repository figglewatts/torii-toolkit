using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Torii.UI
{
    public class TestChildPopulator : AbstractWidgetChildPopulator
    {
        public TestChildPopulator(TUIStyleSheet sheet) : base(sheet) { }

        public int NumberOfChildren { get; set; }

        public override TUIWidget[] CreateChildren()
        {
            TUIWidget[] widgets = new TUIWidget[NumberOfChildren];

            for (int i = 0; i < NumberOfChildren; i++)
            {
                TUIWidget blueWidget = _styleSheet.CreateWidget("blue");
                var i1 = i;
                blueWidget.RegisterEvent(EventTriggerType.PointerClick, pointerData =>
                {
                    Debug.Log("Button " + i1 + " pressed!");
                });
                widgets[i] = blueWidget;
            }

            return widgets;
        }
    }
}
