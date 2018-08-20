using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Torii.Util
{
    public class PropertyField
    {
        private static readonly Dictionary<Type, SerializedPropertyType> _serializedPropertyTypeMap =
            new Dictionary<Type, SerializedPropertyType>()
            {
                {typeof(int), SerializedPropertyType.Integer},
                {typeof(bool), SerializedPropertyType.Boolean},
                {typeof(float), SerializedPropertyType.Float},
                {typeof(string), SerializedPropertyType.String},
                {typeof(Color), SerializedPropertyType.Color},
                {typeof(LayerMask), SerializedPropertyType.LayerMask},
                {typeof(Vector2), SerializedPropertyType.Vector2},
                {typeof(Vector3), SerializedPropertyType.Vector3},
                {typeof(Vector4), SerializedPropertyType.Vector4},
                {typeof(Rect), SerializedPropertyType.Rect},
                {typeof(AnimationCurve), SerializedPropertyType.AnimationCurve},
                {typeof(Bounds), SerializedPropertyType.Bounds},
                {typeof(Gradient), SerializedPropertyType.Gradient},
                {typeof(Quaternion), SerializedPropertyType.Quaternion}
            };
        
        private UnityEngine.Object _instance;
        private PropertyInfo _info;

        private MethodInfo _getter;
        private MethodInfo _setter;

        public SerializedPropertyType Type { get; }

        public string Name => ObjectNames.NicifyVariableName(_info.Name);

        public Object Value
        {
            get { return _getter.Invoke(_instance, null); }
            set { _setter.Invoke(_instance, new[] {value}); }
        }

        public UnityEngine.Object Instance => _instance;

        public PropertyField(Object instance, PropertyInfo info, SerializedPropertyType type)
        {
            _instance = instance as UnityEngine.Object;
            _info = info;
            Type = type;

            _getter = _info.GetGetMethod();
            _setter = _info.GetSetMethod();
        }

        public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType)
        {
            propertyType = SerializedPropertyType.Generic;
            System.Type type = info.PropertyType;

            if (_serializedPropertyTypeMap.ContainsKey(type))
            {
                propertyType = _serializedPropertyTypeMap[type];
                return true;
            }

            if (type.IsEnum)
            {
                propertyType = SerializedPropertyType.Enum;
                return true;
            }

            if (type.IsClass)
            {
                propertyType = SerializedPropertyType.ObjectReference;
                return true;
            }

            return false;
        }
    }
}
