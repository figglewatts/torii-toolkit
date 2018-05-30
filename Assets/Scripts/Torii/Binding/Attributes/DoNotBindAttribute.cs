using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotBindAttribute : Attribute
    {
    }
}
