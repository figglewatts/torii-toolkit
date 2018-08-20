using Torii.Editor;
using Torii.Util;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Torii.UI.Editor
{
    [CustomEditor(typeof(TUIWindow))]
    [CanEditMultipleObjects]
    public class TUIWindowEditor : UnityEditor.Editor
    {
        private TUIWindow _instance;
        private PropertyField[] _properties;

        public void OnEnable()
        {
            _instance = target as TUIWindow;
            _properties = ExposeProperties.GetProperties(_instance);
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            ExposeProperties.Expose(_properties);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_instance);
            }
        }
    }
}
