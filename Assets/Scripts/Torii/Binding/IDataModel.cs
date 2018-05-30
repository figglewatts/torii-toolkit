using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding
{
    public interface IDataModel
    {
        event Action<string, object> OnPropertyChange;

        void NotifyPropertyChange(string propertyName, object value);
    }
}
