using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeGen : MonoBehaviour, IDamagable
{
    [SerializeField] private int _currentMana = 0;

    [SerializeField] private int _hp  = 100;
    [SerializeField] private int _manaPerSec  = 1;
    [SerializeField] private int _maxMana  = 10;

    public int HP
    {
        get { return _hp; }
        private set
        {
            _hp = value;
            if (value < 0)
            {
                _hp = 0;
                Die();
            }
            if (value > 100)
            {
                _hp = 100;
            }
        }
    }

    private void Die()
    {
        if (this?.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }

    public int Mana
    {
        get { return _currentMana; }
        private set
        {
            _currentMana = value;
            if (value < 0)
            {
                _currentMana = 0;
            }
            if (value > 100)
            {
                _currentMana = _maxMana;
            }
        }
    }

    void Update()
    {
    }

    private Coroutine _manaGenerationCoroutine;

    private void Start()
    {
        _manaGenerationCoroutine = StartCoroutine(ManaGenerationCoroutine());
    }

    private void OnDestroy()
    {
        if (_manaGenerationCoroutine != null)
        {
            StopCoroutine(_manaGenerationCoroutine);
        }
    }

    private IEnumerator ManaGenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GenerateMana(_manaPerSec);
        }
    }

    private void GenerateMana(int amount)
    {
        _currentMana += amount;
        if (_currentMana > _maxMana)
        {
            _currentMana = _maxMana;
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
}
