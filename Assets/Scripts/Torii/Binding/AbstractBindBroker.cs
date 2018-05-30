using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Torii.Binding.Attributes;
using Torii.Util;
using UnityEngine;

namespace Torii.Binding
{
    public abstract class AbstractBindBroker<T> where T : IDataModel
    {
        protected IDataModel _model;
        private readonly Dictionary<string, MemberExpression> _bindings;
        private readonly Dictionary<string, PropertyInfo> _properties;

        protected AbstractBindBroker()
        {
            _bindings = new Dictionary<string, MemberExpression>();
            _properties = new Dictionary<string, PropertyInfo>();
        }

        protected void registerModel(IDataModel model)
        {
            _model = model;
            _model.OnPropertyChange += handlePropertyChange;
            bindData();
        }

        private void handlePropertyChange(string property, object value)
        {
            var expr = _bindings[property];
            var fieldInfo = (FieldInfo)expr.Member;
            var obj = ((ConstantExpression)expr.Expression).Value;
            fieldInfo.SetValue(obj, value);
            Debug.LogFormat("Property {0} changed to {1}", property, value);
        }

        private void bindData()
        {
            List<PropertyInfo> propertyInfo = new List<PropertyInfo>(
                typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));

            foreach (var property in propertyInfo)
            {
                // check if the property should be bound
                if (AttributeUtil.HasAttribute<DoNotBindAttribute>(property))
                {
                    continue;
                }

                // check if the property should be bound as a custom name
                string bindingKey = AttributeUtil.GetAttributeValue(property, (BindAsAttribute attr) => attr.PropertyName);
                if (string.IsNullOrEmpty(bindingKey))
                {
                    bindingKey = property.Name;
                }

                _properties[bindingKey] = property;
            }
        }

        public void Bind<TTarget>(string binding, Expression<Func<TTarget>> target)
        {
            _bindings[binding] = (MemberExpression)target.Body;
        }
    }
}
