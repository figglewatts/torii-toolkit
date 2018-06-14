using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torii.Binding
{
    public interface IPropertyWatcher
    {
        event Action<string, IPropertyWatcher> OnPropertyChange;

        Guid GUID { get; }

        void NotifyPropertyChange(string propertyName);
    }
}
