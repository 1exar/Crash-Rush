using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerViewer : MonoBehaviour
{
    [SerializeField] private GameObject attackPowerGameObject;
    [SerializeField] private Transform attackPowerTransform;
    [SerializeField] private SpriteRenderer attackPowerSprite;

    public void EnablePreview()
    {
        attackPowerGameObject.SetActive(true);
    }

    public void DisablePreview()
    {
        attackPowerGameObject.SetActive(false);
    }

    public void SetPreview(float attackPower)
    {
        if (attackPower < 1)
        {
            attackPowerSprite.size = new Vector2(8, 10);
            attackPowerTransform.localPosition = new Vector3(0, 0, 1);
        }
        else if (attackPower < 10)
        {
            attackPowerSprite.size = new Vector2(12.8f, 10);
            attackPowerTransform.localPosition = new Vector3(0, 0, 1.2f);
        }
        else if (attackPower < 20)
        {
            attackPowerSprite.size = new Vector2(17.4f, 10);
            attackPowerTransform.localPosition = new Vector3(0, 0, 1.4f);
        }
        else if (attackPower < 30)
        {
            attackPowerSprite.size = new Vector2(22.4f, 10);
            attackPowerTransform.localPosition = new Vector3(0, 0, 1.6f);
        }
        else
        {
            attackPowerSprite.size = new Vector2(27f, 10);
            attackPowerTransform.localPosition = new Vector3(0, 0, 1.8f);
        }
    }
}
