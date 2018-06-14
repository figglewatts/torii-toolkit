using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torii.Binding
{
    public abstract class AbstractDataBinding
    {
        protected Delegate _bind;
        public string TargetReference { get; }

        public abstract void Invoke();

        protected AbstractDataBinding(string targetReference)
        {
            TargetReference = targetReference;
        }
    }
}
