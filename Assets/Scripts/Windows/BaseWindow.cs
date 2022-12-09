using DG.Tweening;
using UnityEngine;

namespace Windows
{
    public class BaseWindow : MonoBehaviour, IWindow
    {
        public virtual void Close()
        {
            Destroy(gameObject);
        }

        public virtual void Show()
        {
            transform.localScale = Vector3.one * 0.8f;
            transform.DOScale(1, 0.5f);
        }
    }
}
