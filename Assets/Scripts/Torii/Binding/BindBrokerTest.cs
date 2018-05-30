using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding
{
    public class BindBrokerTest : AbstractBindBroker<ModelTest>
    {
        public BindBrokerTest(ModelTest test)
        {
            registerModel(test);
        }
    }
}
