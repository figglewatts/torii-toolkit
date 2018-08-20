using UnityEditor;
using UnityEngine;

namespace Torii.UI.Editor
{
    public class TUIGameObjectCreation : MonoBehaviour
    {
        [MenuItem("GameObject/TUI/Window", false, 1)]
        static void CreateTUIWindow(MenuCommand menuCommand)
        {
            Debug.Log("Creating TUI window");
        }
    }
}
