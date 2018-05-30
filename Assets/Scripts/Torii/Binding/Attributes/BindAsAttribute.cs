using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BindAsAttribute : Attribute
    {
        public string PropertyName;

        public BindAsAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
