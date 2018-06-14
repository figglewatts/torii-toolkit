﻿using System;
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
        protected Dictionary<string, AbstractDataBinding> _bindings;
        private string _lastChangeHandled = "";

        protected AbstractBindBroker()
        {
            _bindings = new Dictionary<string, AbstractDataBinding>();
        }

        protected void registerData(IPropertyWatcher watcher)
        {
            watcher.OnPropertyChange += handleChange;
        }

        private void handleChange(string propertyName, IPropertyWatcher instance)
        {
            AbstractDataBinding binding;
            string propertyReference = makePropertyReference(instance.GUID, propertyName);
            if (_bindings.TryGetValue(propertyReference, out binding))
            {
                // check to see if we're in some kind of change loop
                if (_lastChangeHandled.Equals(binding.TargetReference))
                {
                    // lastChangeHandled stores the property reference of the last bind,
                    // so if this matches the target of the current bind then we don't want
                    // to bind as it would create a loop and a stack overflow
                    _lastChangeHandled = "";
                    return;
                }
                _lastChangeHandled = propertyReference;
                binding.Invoke();
            }
        }

        // a property reference is the GUID of the class instance the property is from coupled with the property name
        // they are separated by a '.' -- a property reference allows the bind broker to recognise which changes come
        // from what instance
        private string makePropertyReference(Guid instance, string propertyName)
        {
            return instance.ToString() + "." + propertyName;
        }
        
        public void Bind<TType>(Expression<Func<TType>> binder, Expression<Func<TType>> bindee, BindingType bindingType)
        {
            var binderMemberExp = (MemberExpression)binder.Body;
            IPropertyWatcher binderInstance = Expression.Lambda<Func<IPropertyWatcher>>(binderMemberExp.Expression).Compile()();
            string binderReference = makePropertyReference(binderInstance.GUID, binderMemberExp.Member.Name);

            var bindeeMemberExp = (MemberExpression)bindee.Body;
            IPropertyWatcher bindeeInstance = Expression.Lambda<Func<IPropertyWatcher>>(bindeeMemberExp.Expression).Compile()();
            string bindeeReference = makePropertyReference(bindeeInstance.GUID, bindeeMemberExp.Member.Name);

            DataBinding<TType> binding = new DataBinding<TType>(binder, bindee, bindeeReference);
            _bindings[binderReference] = binding;

            if (bindingType == BindingType.TwoWay)
            {
                DataBinding<TType> oppositeBinding = new DataBinding<TType>(bindee, binder, binderReference);
                _bindings[bindeeReference] = oppositeBinding;

            }
        }

        public enum BindingType
        {
            OneWay,
            TwoWay
        }
    }
}
