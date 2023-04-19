using UnityEngine;
using UnityEngine.UI;

public class MapPreviewObject : MonoBehaviour
{
    [SerializeField] private Image mapPreviewImage;

    public void SetMapPreviewImage(Sprite newImage)
    {
        mapPreviewImage.sprite = newImage;
    }
}
