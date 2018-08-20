using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Torii.Editor
{
    public class GUIGradientField
    {
        private static readonly MethodInfo _gradientFieldWithLabel;
        private static readonly MethodInfo _gradientField;

        static GUIGradientField()
        {
            Type editorLayoutType = typeof(EditorGUILayout);

            // use ugly C# reflection magic to steal these methods from the assembly
            _gradientFieldWithLabel = editorLayoutType.GetMethod("GradientField", 
                BindingFlags.NonPublic | BindingFlags.Static,
                null, 
                new []
                {
                    typeof(string), typeof(Gradient), typeof(GUILayoutOption[])
                }, 
                null);
            _gradientField = editorLayoutType.GetMethod("GradientField", 
                BindingFlags.NonPublic | BindingFlags.Static,
                null, 
                new []
                {
                    typeof(Gradient), typeof(GUILayoutOption[])
                }, 
                null);
        }

        public static Gradient GradientField(string label, Gradient gradient, params GUILayoutOption[] options)
        {
            if (gradient == null) gradient = new Gradient();

            gradient = _gradientFieldWithLabel.Invoke(null, new object[] {label, gradient, options}) as Gradient;

            return gradient;
        }

        public static Gradient GradientField(Gradient gradient, params GUILayoutOption[] options)
        {
            if (gradient == null) gradient = new Gradient();

            gradient = _gradientField.Invoke(null, new object[] { gradient, options }) as Gradient;

            return gradient;
        }
    }
}
