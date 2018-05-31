using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Torii.Binding.Attributes;
using Torii.Exceptions;
using Torii.Util;
using UnityEngine;

namespace Torii.Binding
{
    public abstract class AbstractBindBroker
    {
        protected IDataModel _model;
        protected Dictionary<string, AbstractDataBinding> _byModel;
        protected Dictionary<string, AbstractDataBinding> _byView;

        protected void registerModel(IDataModel model)
        {
            _model = model;

            _byModel = new Dictionary<string, AbstractDataBinding>();
            _byView = new Dictionary<string, AbstractDataBinding>();

            _model.OnPropertyChange += handleModelPropertyChange;
        }

        /// <summary>
        /// Called when a property in the model is changed.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private void handleModelPropertyChange(string property, object value)
        {
            _byModel[property].Invoke(DataBindDirection.BinderToBindee);
        }

        public void Bind<TTarget>(Expression<Func<TTarget>> binder, Expression<Func<TTarget>> target, BindingType bindingType)
        {
            string binderName = binder.ToString();
            binderName = binderName.Substring(binderName.LastIndexOf('.') + 1);
            DataBinding<TTarget> binding = new DataBinding<TTarget>(binder, target, bindingType);
            _byModel[binderName] = binding;
        }
    }
}
