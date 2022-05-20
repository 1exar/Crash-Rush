using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallHealth : MonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private int _maxHealth;

    private int _health;

    private BallsContainer_old _balls;
    [SerializeField]
    private Ball _ball;
    [SerializeField]
    private GameObject _damageTextPrefab;
    [SerializeField]
    private Transform _damageTextParent;

    private int _beforePreviewDamage;
    
    private void Awake()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
        _health = _maxHealth;
        _beforePreviewDamage = _maxHealth;
    }
    
    public void Init(Ball ball, BallsContainer_old _container)
    {
        _ball = ball;
        _balls = _container;
    }
    
    public void GetDamage(int dmg)
    {
        _health -= dmg;
        _beforePreviewDamage = _health;
        ShowDamage(dmg);
        if (_health <= 0)
        {
            StartCoroutine(Dead());
        }
        else
        {
            _healthSlider.value = _health;
        }
    }

    private IEnumerator Dead()
    {
        _healthSlider.value = 0;
        _balls.RemoveBall(_ball);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void ShowDamage(int damage)
    {
        DamageText dmgText = Instantiate(_damageTextPrefab, _damageTextParent).GetComponent<DamageText>();
        dmgText.Init(damage);
    }

    public void PreviewDamage(int dmg)
    {
        CancelInvoke("CancelPreview");
        _beforePreviewDamage = _health;
        _healthSlider.value = _health - dmg;
        Invoke(nameof(CancelPreview), .5f);
    }

    public void CancelPreview()
    {
        _healthSlider.value = _beforePreviewDamage;
    }
    
}
