using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Torii.Util;
using UnityEditor;
using UnityEngine;

namespace Torii.Editor
{
    public static class ExposeProperties
    {
        public static void Expose(PropertyField[] properties)
        {
            GUILayoutOption[] emptyOptions = new GUILayoutOption[0];

            EditorGUILayout.BeginVertical(emptyOptions);

            foreach (PropertyField field in properties)
            {
                EditorGUILayout.BeginHorizontal(emptyOptions);

                drawProperty(field, emptyOptions);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        public static PropertyField[] GetProperties(UnityEngine.Object obj)
        {
            List<PropertyField> fields = new List<PropertyField>();

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo info in propertyInfos)
            {
                if (!(info.CanRead && info.CanWrite)) continue;

                if (!AttributeUtil.HasAttribute<ExposePropertyAttribute>(info)) continue;

                SerializedPropertyType type;
                if (PropertyField.GetPropertyType(info, out type))
                {
                    PropertyField field = new PropertyField(obj, info, type);
                    fields.Add(field);
                }
            }

            return fields.ToArray();
        }

        private static void drawProperty(PropertyField field, GUILayoutOption[] options)
        {
            switch (field.Type)
            {
                case SerializedPropertyType.Integer:
                {
                    field.Value = EditorGUILayout.IntField(field.Name, (int)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Float:
                {
                    field.Value = EditorGUILayout.FloatField(field.Name, (float)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Boolean:
                {
                    field.Value = EditorGUILayout.Toggle(field.Name, (bool) field.Value, options);
                    break;
                }
                case SerializedPropertyType.String:
                {
                    field.Value = EditorGUILayout.TextField(field.Name, (string) field.Value, options);
                    break;
                }
                case SerializedPropertyType.Color:
                {
                    field.Value = EditorGUILayout.ColorField(field.Name, (Color) field.Value, options);
                    break;
                }
                case SerializedPropertyType.ObjectReference:
                {
                    field.Value = EditorGUILayout.ObjectField(field.Name, (UnityEngine.Object) field.Value,
                        field.GetType(), true, options);
                    break;
                }
                case SerializedPropertyType.LayerMask:
                {
                    field.Value = EditorGUILayout.LayerField(field.Name, (LayerMask)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Enum:
                {
                    field.Value = EditorGUILayout.EnumPopup(field.Name, (Enum)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Vector2:
                {
                    field.Value = EditorGUILayout.Vector2Field(field.Name, (Vector2)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Vector3:
                {
                    field.Value = EditorGUILayout.Vector3Field(field.Name, (Vector3)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Vector4:
                {
                    field.Value = EditorGUILayout.Vector4Field(field.Name, (Vector4)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Rect:
                {
                    field.Value = EditorGUILayout.RectField(field.Name, (Rect) field.Value, options);
                    break;
                }
                case SerializedPropertyType.AnimationCurve:
                {
                    field.Value = EditorGUILayout.CurveField(field.Name, (AnimationCurve)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Bounds:
                {
                    field.Value = EditorGUILayout.BoundsField(field.Name, (Bounds)field.Value, options);
                    break;
                }
                case SerializedPropertyType.Gradient:
                {
                    field.Value = GUIGradientField.GradientField(field.Name, (Gradient) field.Value, options);
                    break;
                }
                case SerializedPropertyType.Quaternion:
                {
                    Quaternion quat = (Quaternion)field.Value;
                    field.Value = EditorGUILayout.Vector4Field(field.Name, new Vector4(quat.x, quat.y, quat.z, quat.w), options);
                    break;
                }
            }
        }
    }
}
