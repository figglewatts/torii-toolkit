using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding
{
    public class BindBrokerTest : AbstractBindBroker
    {
        public BindBrokerTest(ModelTest test, ViewTest view)
        {
            registerData(test);
            registerData(view);
        }
    }
}
