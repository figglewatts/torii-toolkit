using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleJSON;
using Torii.Exceptions;
using Torii.Resource;
using Torii.Util;
using UnityEngine;
using UnityEngine.UI;


namespace Torii.UI
{
    // TODO: change all EnumUtil.Parse to EnumUtil.TryParse with exception

    // TODO: a more robust serialization/deserialization solution... (newtonsoft json??)

    // TODO: reimplement this

    public class TUIStyle
    {
        /*
        protected string BackgroundGraphicPath { get; set; }
        protected bool UseStreamingAssets { get; set; }
        protected WidgetLayoutType LayoutType { get; set; }
        protected WidgetBackgroundType BackgroundType { get; set; }
        protected LayoutElementData LayoutElement { get; set; }
        protected Vector2 Pivot { get; set; }
        protected Color Color { get; set; }
        protected AnchorType Anchor { get; set; }
        protected RectOffset Padding { get; set; }
        protected TextAnchor ChildAlignment { get; set; }

        // GridLayout styling
        protected Vector2 GridCellSize { get; set; }
        protected GridLayoutGroup.Constraint GridConstraint { get; set; }
        protected int GridConstraintCount { get; set; }
        protected GridLayoutGroup.Axis GridStartAxis { get; set; }
        protected GridLayoutGroup.Corner GridStartCorner { get; set; }
        protected Vector2 GridLayoutSpacing { get; set; }

        // HorizontalOrVerticalLayout styling
        protected bool LayoutChildControlWidth { get; set; }
        protected bool LayoutChildControlHeight { get; set; }
        protected bool LayoutChildExpandWidth { get; set; }
        protected bool LayoutChildExpandHeight { get; set; }
        protected float LayoutSpacing { get; set; }

        // ChildPopulator styling
        protected string ChildPopulatorTypeName { get; set; }
        protected JSONArray ChildPopulatorProperties { get; set; }
        protected AbstractWidgetChildPopulator ChildPopulatorInstance { get; set; }

        protected TUIStyleSheet _parentSheet { get; set; }
        protected string _className { get; set; }

        public TUIStyle(JSONNode json, TUIStyleSheet sheet, string className)
        {
            _parentSheet = sheet;
            _className = className;

            string backgroundType =
                json.GetValueOrDefault<JSONString>("backgroundType", WidgetBackgroundType.Sprite.ToString());
            BackgroundType = parseEnumWithException<WidgetBackgroundType>(backgroundType, "backgroundType");

            BackgroundGraphicPath = json["backgroundGraphic"];

            UseStreamingAssets = json.GetValueOrDefault<JSONBool>("streamingAssets", true);

            string layoutType = json.GetValueOrDefault<JSONString>("layoutType", WidgetLayoutType.None.ToString());
            LayoutType = parseEnumWithException<WidgetLayoutType>(layoutType, "layoutType");

            JSONNode layoutElement = json["layoutElement"];
            LayoutElement = layoutElement == null ? null : new LayoutElementData(layoutElement);

            Pivot = json.GetValueOrDefault<JSONNode>("pivot", new Vector2(0, 1));

            Color = json.GetValueOrDefault<JSONNode>("color", UnityEngine.Color.white);

            string anchorType = json.GetValueOrDefault<JSONString>("anchorType", AnchorType.TopLeft.ToString());
            Anchor = parseEnumWithException<AnchorType>(anchorType, "anchorType");

            Padding = json.GetValueOrDefault<JSONNode>("padding", new RectOffset(0, 0, 0, 0));

            string childAlignment = json.GetValueOrDefault<JSONString>("childAlignment", "UpperLeft");
            ChildAlignment = parseEnumWithException<TextAnchor>(childAlignment, "childAlignment");

            if (LayoutType == WidgetLayoutType.Grid)
            {
                JSONNode gridLayoutNode = json.GetValueOrDefault<JSONNode>("gridLayout", new JSONObject());

                GridCellSize = gridLayoutNode.GetValueOrDefault<JSONNode>("cellSize", new Vector2(100, 100));

                string gridConstraint = gridLayoutNode.GetValueOrDefault<JSONString>("constraint", "Flexible");
                GridConstraint =
                    parseEnumWithException<GridLayoutGroup.Constraint>(gridConstraint, "gridLayout.constraint");

                GridConstraintCount = gridLayoutNode.GetValueOrDefault<JSONNumber>("constraintCount", 1);

                string gridStartAxis = gridLayoutNode.GetValueOrDefault<JSONString>("startAxis", "Horizontal");
                GridStartAxis = parseEnumWithException<GridLayoutGroup.Axis>(gridStartAxis, "gridLayout.startAxis");

                string gridStartCorner = gridLayoutNode.GetValueOrDefault<JSONString>("startCorner", "UpperLeft");
                GridStartCorner =
                    parseEnumWithException<GridLayoutGroup.Corner>(gridStartCorner, "gridLayout.startCorner");

                GridLayoutSpacing = gridLayoutNode.GetValueOrDefault<JSONNode>("layoutSpacing", Vector2.zero);
            }
            else if (LayoutType == WidgetLayoutType.Horizontal || LayoutType == WidgetLayoutType.Vertical)
            {
                JSONNode layoutNode = json.GetValueOrDefault<JSONNode>("horizontalOrVerticalLayout", new JSONObject());

                LayoutChildControlWidth = layoutNode.GetValueOrDefault<JSONBool>("childControlWidth", true);
                LayoutChildControlHeight = layoutNode.GetValueOrDefault<JSONBool>("childControlHeight", true);
                LayoutChildExpandWidth = layoutNode.GetValueOrDefault<JSONBool>("childExpandWidth", true);
                LayoutChildExpandHeight = layoutNode.GetValueOrDefault<JSONBool>("childExpandHeight", true);
                LayoutSpacing = json.GetValueOrDefault<JSONNumber>("layoutSpacing", 0);
            }

            JSONNode childPopulator = json.GetValueOrDefault<JSONNode>("childPopulator", null);
            if (childPopulator != null)
            {
                ChildPopulatorTypeName = childPopulator.GetValueOrDefault<JSONString>("type", string.Empty);
                ChildPopulatorProperties = childPopulator.GetValueOrDefault<JSONArray>("properties", null);
                ChildPopulatorInstance = loadChildPopulator();
            }
        }

        public TUIStyle(string backgroundGraphicPath,
            bool useStreamingAssets,
            WidgetLayoutType layoutType,
            WidgetBackgroundType backgroundType,
            LayoutElementData layoutElement,
            Vector2 pivot,
            Color color,
            AnchorType anchor,
            RectOffset padding)
        {
            BackgroundGraphicPath = backgroundGraphicPath;
            UseStreamingAssets = useStreamingAssets;
            LayoutType = layoutType;
            BackgroundType = backgroundType;
            LayoutElement = layoutElement;
            Pivot = pivot;
            Color = color;
            Anchor = anchor;
            Padding = padding;
        }

        public static readonly TUIStyle DefaultFallback = new TUIStyle("", true, WidgetLayoutType.None,
            WidgetBackgroundType.Sprite, null, new Vector2(0.5f, 0.5f), UnityEngine.Color.white,
            AnchorType.TopLeft, new RectOffset(0, 0, 0, 0));

        public TUIWidget CreateWidget()
        {
            TUIWidget widget = null;

            // if there is no background graphic
            if (string.IsNullOrEmpty(BackgroundGraphicPath))
            {
                widget = TUIWidget.Create(LayoutType, LayoutElement);
            }
            else
            {
                widget = TUIWidget.Create(LayoutType, BackgroundType, BackgroundGraphicPath, UseStreamingAssets, LayoutElement);
            }

            widget.Color = Color;
            widget.Pivot = Pivot;
            widget.Anchor = Anchor;
            widget.Padding = Padding;

            if (LayoutType != WidgetLayoutType.None)
            {
                widget.Layout.childAlignment = ChildAlignment;
            }

            if (LayoutType == WidgetLayoutType.Grid)
            {
                widget.GridLayout.cellSize = GridCellSize;
                widget.GridLayout.constraint = GridConstraint;
                widget.GridLayout.constraintCount = GridConstraintCount;
                widget.GridLayout.startAxis = GridStartAxis;
                widget.GridLayout.startCorner = GridStartCorner;
                widget.GridLayout.spacing = GridLayoutSpacing;
            }
            else if (LayoutType == WidgetLayoutType.Horizontal || LayoutType == WidgetLayoutType.Vertical)
            {
                widget.HorizontalOrVerticalLayout.childControlWidth = LayoutChildControlWidth;
                widget.HorizontalOrVerticalLayout.childControlHeight = LayoutChildControlHeight;
                widget.HorizontalOrVerticalLayout.childForceExpandWidth = LayoutChildExpandWidth;
                widget.HorizontalOrVerticalLayout.childForceExpandHeight = LayoutChildExpandHeight;
                widget.HorizontalOrVerticalLayout.spacing = LayoutSpacing;
            }

            if (ChildPopulatorInstance != null)
            {
                widget.ChildPopulator = ChildPopulatorInstance;
            }

            return widget;
        }

        private AbstractWidgetChildPopulator loadChildPopulator()
        {
            Type populatorType = Type.GetType(ChildPopulatorTypeName, true);

            // check to see if the type inherits from abstract base class
            if (!populatorType.IsSubclassOf(typeof(AbstractWidgetChildPopulator)))
            {
                throw new ToriiException("In style class '" + _className + "': ChildPopulator " +
                                                    populatorType + " must inherit from " +
                                                    typeof(AbstractWidgetChildPopulator));
            }

            AbstractWidgetChildPopulator populator =
                (AbstractWidgetChildPopulator)Activator.CreateInstance(populatorType, _parentSheet);

            if (ChildPopulatorProperties != null)
            {
                foreach (JSONObject property in ChildPopulatorProperties)
                {
                    setPopulatorProperty(property, populator, populatorType);
                }
            }

            return populator;
        }

        private void setPopulatorProperty(JSONObject property, AbstractWidgetChildPopulator populator, Type populatorType)
        {
            string name = property["name"];
            string typeValue = property["type"];
            JSONNode value = property["value"];

            if (name == null || typeValue == null || value == null)
            {
                throw new ToriiException("In style class '" + _className + "': A property in populator " +
                                                    populatorType + " is malformed!");
            }

            PopulatorPropertyType type;
            if (!EnumUtil.TryParse(typeValue, out type))
            {
                throw new ToriiException("In style class '" + _className + "': Populator property type " +
                                         typeValue + " is not a valid type.");
            }

            // check if the property could not be found
            PropertyInfo prop = populatorType.GetProperty(name);
            if (prop == null)
            {
                Debug.LogWarningFormat(
                    "In style class '" + _className +
                    "': Could not set property {0}, no matching property found in Type {1}", name,
                    populatorType);
                return;
            }

            try
            {
                switch (type)
                {
                    case PopulatorPropertyType.Boolean:
                    {
                        setPopulatorBooleanProperty(populator, prop, value);
                        break;
                    }
                    case PopulatorPropertyType.Float:
                    {
                        setPopulatorFloatProperty(populator, prop, value);
                        break;
                    }
                    case PopulatorPropertyType.Integer:
                    {
                        setPopulatorIntProperty(populator, prop, value);
                        break;
                    }
                    case PopulatorPropertyType.String:
                    {
                        setPopulatorStringProperty(populator, prop, value);
                        break;
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw new ToriiException(
                    "In style class '" + _className + "': Could not set property, non matching type!", e);
            }
            catch (MethodAccessException e)
            {
                throw new ToriiException(
                    "In style class '" + _className + "': Could not set property, property is not public!", e);
            }
            catch (TargetInvocationException e)
            {
                throw new ToriiException(
                    "In style class '" + _className + "': Could not set property!", e);
            }
        }

        private void setPopulatorBooleanProperty(AbstractWidgetChildPopulator populator, PropertyInfo prop,
            JSONNode value)
        {
            if (!value.IsBoolean)
            {
                throw new ArgumentException("Boolean value was not boolean!");
            }

            prop.SetValue(populator, value.AsBool, null);
        }

        private void setPopulatorFloatProperty(AbstractWidgetChildPopulator populator, PropertyInfo prop,
            JSONNode value)
        {
            if (!value.IsNumber)
            {
                throw new ArgumentException("Float value was not number!");
            }

            prop.SetValue(populator, value.AsFloat, null);
        }

        private void setPopulatorIntProperty(AbstractWidgetChildPopulator populator, PropertyInfo prop,
            JSONNode value)
        {
            if (!value.IsNumber)
            {
                throw new ArgumentException("Integer value was not number!");
            }

            prop.SetValue(populator, value.AsInt, null);
        }

        private void setPopulatorStringProperty(AbstractWidgetChildPopulator populator, PropertyInfo prop,
            JSONNode value)
        {
            if (!value.IsString)
            {
                throw new ArgumentException("String value was not string!");
            }

            prop.SetValue(populator, value.Value, null);
        }

        private T parseEnumWithException<T>(string enumValue, string enumKey)
        {
            T enumType;
            if (!EnumUtil.TryParse(enumValue, out enumType))
            {
                throw new ToriiException("In style class '" + _className + "': " + enumKey + " value " +
                                         enumValue + " is invalid.");
            }

            return enumType;
        }*/
    }
}
