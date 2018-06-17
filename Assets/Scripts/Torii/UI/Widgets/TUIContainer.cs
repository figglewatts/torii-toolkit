using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI.Widgets
{
    public enum ContainerLayoutType
    {
        Grid,
        Horizontal,
        Vertical,
        None
    }
    
    public class TUIContainer : TUIWidget
    {
        public LayoutElement Element { get; protected set; }

        public ContainerLayoutType LayoutType { get; protected set; }

        public AbstractWidgetChildPopulator ChildPopulator { get; set; }

        public LayoutGroup Layout
        {
            get
            {
                switch (LayoutType)
                {
                    case ContainerLayoutType.Grid:
                        return GetComponent<GridLayoutGroup>();
                    case ContainerLayoutType.Horizontal:
                        return GetComponent<HorizontalLayoutGroup>();
                    case ContainerLayoutType.Vertical:
                        return GetComponent<VerticalLayoutGroup>();
                    default:
                        throw new InvalidOperationException("Cannot get LayoutGroup if widget has no layout component!");
                }
            }
        }

        public GridLayoutGroup GridLayout
        {
            get
            {
                if (LayoutType == ContainerLayoutType.Grid)
                {
                    return GetComponent<GridLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get GridLayoutGroup if widget does not have one!");
            }
        }

        public HorizontalOrVerticalLayoutGroup HorizontalOrVerticalLayout
        {
            get
            {
                if (LayoutType == ContainerLayoutType.Horizontal || LayoutType == ContainerLayoutType.Vertical)
                {
                    return GetComponent<HorizontalOrVerticalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get HorizontalOrVerticalLayoutGroup if widget does not have one!");
            }
        }

        public HorizontalLayoutGroup HorizontalLayout
        {
            get
            {
                if (LayoutType == ContainerLayoutType.Horizontal)
                {
                    return GetComponent<HorizontalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get HorizontalLayoutGroup if widget does not have one!");
            }
        }

        public VerticalLayoutGroup VerticalLayout
        {
            get
            {
                if (LayoutType == ContainerLayoutType.Vertical)
                {
                    return GetComponent<VerticalLayoutGroup>();
                }

                throw new InvalidOperationException("Cannot get VerticalLayoutGroup if widget does not have one!");
            }
        }

        public TextAnchor ChildAlignment
        {
            get { return Layout.childAlignment; }
            set { Layout.childAlignment = value; }
        }

        public RectOffset Padding
        {
            get { return LayoutType == ContainerLayoutType.None ? null : Layout.padding; }
            set
            {
                if (LayoutType == ContainerLayoutType.None) return;
                Layout.padding = value;
            }
        }

        public static TUIContainer Create(ContainerLayoutType layout, LayoutElementData element = null)
        {
            TUIContainer container = createBaseWidget<TUIContainer>(element);

            switch (layout)
            {
                case ContainerLayoutType.Grid:
                {
                    container.gameObject.AddComponent<GridLayoutGroup>();
                    break;
                }
                case ContainerLayoutType.Horizontal:
                {
                    container.gameObject.AddComponent<HorizontalLayoutGroup>();
                    break;
                }
                case ContainerLayoutType.Vertical:
                {
                    container.gameObject.AddComponent<VerticalLayoutGroup>();
                    break;
                }
                case ContainerLayoutType.None:
                {
                    // intentionally empty
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(layout), layout, "Layout type not found!");
                }
            }

            container.LayoutType = layout;
            return container;
        }

        public virtual void PopulateChildren()
        {
            if (ChildPopulator == null)
            {
                throw new InvalidOperationException("Cannot populate if widget does not have ChildPopulator!");
            }

            TUIWidget[] children = ChildPopulator.CreateChildren();
            foreach (TUIWidget child in children)
            {
                AddChild(child);
            }
        }
    }
}
