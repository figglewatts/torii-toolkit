using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torii.Binding.Attributes;
using UnityEngine;

namespace Torii.Binding
{
    public class ModelTest : MonoBehaviour, IDataModel
    {
        private float _aFloat;

        public float AFloat
        {
            get { return _aFloat; }
            set
            {
                _aFloat = value;
                NotifyPropertyChange(nameof(AFloat), value);
            }
        }

        public event Action<string, object> OnPropertyChange;

        public void NotifyPropertyChange(string propertyName, object value)
        {
            OnPropertyChange?.Invoke(propertyName, value);
        }
    }
}
