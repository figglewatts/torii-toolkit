using UnityEngine;
using UnityEngine.EventSystems;

namespace Torii.UI
{
    public class TUIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Transform Target;

        private bool _isMouseDown = false;
        private Vector3 _startMousePosition;
        private Vector3 _startPosition;

        public void Awake()
        {
            if (!Target)
            {
                Debug.LogWarning("TUIDraggable does not have target set!", this);
            }
        }

        public void OnPointerDown(PointerEventData dt)
        {
            _isMouseDown = true;

            _startPosition = Target.position;
            _startMousePosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData dt)
        {
            _isMouseDown = false;
        }

        void Update()
        {
            if (_isMouseDown)
            {
                Vector3 currentPosition = Input.mousePosition;

                Vector3 diff = currentPosition - _startMousePosition;

                Vector3 pos = _startPosition + diff;

                Target.position = pos;
            }
        }
    }
}
