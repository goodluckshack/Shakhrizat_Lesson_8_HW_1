using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private Slider[] _healthBar;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _fillSpeed = 10;
    private float _healthPercents;

    // Start is called before the first frame update
    void Start()
    {
        _healthPercents = _health / 100;
    }


    public void MakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
        }

        _healthPercents = _health / 100;
    }


    // Update is called once per frame
    void Update()
    {
        foreach (var item in _healthBar)
        {
            item.value = Mathf.Lerp(item.value, _healthPercents, Time.deltaTime * _fillSpeed);
        }

    }
}
