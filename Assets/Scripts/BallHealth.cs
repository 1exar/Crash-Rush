using System.Collections;
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

    private BallsContainer _balls;
    [SerializeField]
    private Ball _ball;
    [SerializeField]
    private GameObject _damageTextPrefab;
    [SerializeField]
    private Transform _damageTextParent;
    
    private void Awake()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
        _health = _maxHealth;
    }
    
    public void Init(Ball ball, BallsContainer _container)
    {
        _ball = ball;
        _balls = _container;
    }
    
    public void GetDamage(int dmg)
    {
        _health -= dmg;
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
}
