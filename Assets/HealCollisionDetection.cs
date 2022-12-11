using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCollisionDetection : MonoBehaviour
{

    [SerializeField] private HealEntity _currentEntity;
    [SerializeField] private GameObject _healEffect;
    [SerializeField] private float _healEffectDuration = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Entity entity))
        {
            if (entity.IsMine == _currentEntity.IsMine)
            {
                entity.ApplyHealEffect(_currentEntity.HealEffect);
                _healEffect.SetActive(true);
                Invoke(nameof(HideHealEffect), _healEffectDuration);
            }
        }
    }

    private void HideHealEffect()
    {
        _healEffect.SetActive(false);
    }


}
