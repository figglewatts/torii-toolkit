using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torii.Binding
{
    public abstract class AbstractDataBinding
    {
        protected Delegate _setBindeeToBinder;
        protected Delegate _setBinderToBindee;
        public BindingType BindingType { get; protected set; }

        public abstract void Invoke(DataBindDirection direction);
    }

    public enum DataBindDirection
    {
        BindeeToBinder,
        BinderToBindee
    }

    public enum BindingType
    {
        OneWay,
        TwoWay
    }
}
