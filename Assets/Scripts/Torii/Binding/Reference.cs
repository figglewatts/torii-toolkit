using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torii.Binding
{
    public sealed class Reference
    {
        public Func<object> Get { get; private set; }
        public Action<object> Set { get; private set; }

        public Reference(Func<object> getter, Action<object> setter)
        {
            Get = getter;
            Set = setter;
        }
    }
}
