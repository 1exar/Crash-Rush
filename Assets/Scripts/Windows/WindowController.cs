using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Windows
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private List<BaseWindow> _windowsPrefabs = new List<BaseWindow>();
        [SerializeField] private RectTransform _canvasParent;
        
        private static WindowController Instance;
        private static BaseWindow currentWindow;
        
        private static List<BaseWindow> windows
        {
            get
            {
                return Instance._windowsPrefabs;
            }
            set{}
        }

        private static RectTransform canvasParent => Instance._canvasParent;
        
        private void Awake() => Instance = this;

        private void Start()
        {
            ShowWindow(typeof(CardsControllerWindow));
        }

        public static void ShowWindow(Type window)
        {
            if (currentWindow)
                CloseCurrentWindow();
            var windowPrefab = windows.Where(w => w.GetType() == window).First();
            if (windowPrefab)
            {
                currentWindow = Instantiate(windowPrefab.gameObject, canvasParent.gameObject.transform)
                    .GetComponent<BaseWindow>();
                currentWindow.Show();
            }
        }

        public static void CloseWindow(Type window)
        {
            if (currentWindow.GetType() == window)
            {
                CloseCurrentWindow();
            }
        }

        public static void CloseCurrentWindow()
        {
            currentWindow.Close();
        }
        
    }

}
