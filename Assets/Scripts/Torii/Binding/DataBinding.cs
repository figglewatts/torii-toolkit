using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Torii.Binding
{
    public class DataBinding<T> : AbstractDataBinding
    {
        public DataBinding(Expression<Func<T>> binder, Expression<Func<T>> bindee, BindingType type)
        {
            var bindeeToBinder = Expression.Assign(binder.Body, bindee.Body);
            var bindeeToBinderLambda = Expression.Lambda(bindeeToBinder);
            _setBindeeToBinder = bindeeToBinderLambda.Compile();

            var binderToBindee = Expression.Assign(bindee.Body, binder.Body);
            var binderToBindeeLambda = Expression.Lambda(binderToBindee);
            _setBinderToBindee = binderToBindeeLambda.Compile();

            BindingType = type;
        }

        public override void Invoke(DataBindDirection direction)
        {
            if (direction == DataBindDirection.BindeeToBinder && BindingType == BindingType.OneWay)
            {
                throw new InvalidOperationException("Cannot bind bindee to binder if bind direction is one way!");
            }

            switch (direction)
            {
                case DataBindDirection.BindeeToBinder:
                    _setBindeeToBinder.DynamicInvoke();
                    break;
                case DataBindDirection.BinderToBindee:
                    _setBinderToBindee.DynamicInvoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction,
                        "Value of DataBindDirection enum invalid!");
            }
        }
    }
}
