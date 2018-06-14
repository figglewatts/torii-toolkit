using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torii.Binding
{
    public class ViewTest : IPropertyWatcher
    {
        private float _anotherOne;

        public float AnotherOne
        {
            get { return _anotherOne; }
            set
            {
                _anotherOne = value;
                NotifyPropertyChange(nameof(AnotherOne));
            }
        }

        public event Action<string, IPropertyWatcher> OnPropertyChange;
        public Guid GUID { get; }

        public void NotifyPropertyChange(string propertyName)
        {
            OnPropertyChange?.Invoke(propertyName, this);
        }

        public ViewTest(float val)
        {
            GUID = Guid.NewGuid();
            _anotherOne = val;
        }
    }
}
