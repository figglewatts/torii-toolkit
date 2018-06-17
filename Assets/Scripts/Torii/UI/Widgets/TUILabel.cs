using System.Collections;
using System.Collections.Generic;
using Torii.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI.Widgets
{
    public class TUILabel : TUIGraphic
    {
        public static TUILabel Create(string text, TUILabelSettings settings = null, LayoutElementData element = null)
        {
            TUILabel label = createBaseWidget<TUILabel>(element);

            Text uiText = label.gameObject.AddComponent<Text>();
            label.Graphic = uiText;
            uiText.text = text;

            applySettings(ref uiText, settings ?? new TUILabelSettings());

            return label;
        }

        private static void applySettings(ref Text text, TUILabelSettings settings)
        {
            text.alignment = settings.Alignment;
            text.fontSize = settings.FontSize;
            text.font = settings.Font;
            text.fontStyle = settings.FontStyle;
            text.horizontalOverflow = settings.HorizontalWrapping;
            text.lineSpacing = settings.LineSpacing;
            text.verticalOverflow = settings.VerticalWrapping;
            text.color = settings.TextColor;
        }
    }

    public class TUILabelSettings
    {
        private static readonly string[] DEFAULT_FONTNAMES = {
            "Arial", "Helvetica", "Palatino", "Garamond", "Bookman"
        };

        private const int DEFAULT_FONTSIZE = 12;
        
        public TextAnchor Alignment = TextAnchor.UpperLeft;
        public int FontSize = 12;
        public Font Font = UnityEngine.Font.CreateDynamicFontFromOSFont(DEFAULT_FONTNAMES, DEFAULT_FONTSIZE);
        public FontStyle FontStyle = FontStyle.Normal;
        public HorizontalWrapMode HorizontalWrapping = HorizontalWrapMode.Overflow;
        public float LineSpacing = 0;
        public VerticalWrapMode VerticalWrapping = VerticalWrapMode.Truncate;
        public Color TextColor = Color.white;
    }
}