using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxlife = 100;
    [SerializeField] float invulnerableTime;
    private float _currentLife;
    private bool _isDead;
    bool _invulnerable;
    SpriteFlash _flash;
    
    public event Action<float> OnLifeChanged;
    public event Action OnDie;

    public void Awake()
    {
        _flash = GetComponentInChildren<SpriteFlash>();

        ResetValues();
        _isDead = false;
        _invulnerable = false;
    }

    public void GetDamage(float damage)
    {
        if (_invulnerable) return;

        StartCoroutine(Invulnerability(invulnerableTime));

        if (_currentLife > 0)
        {
            //animatorcontroller.SetTrigger("GetHurt");//reproduce la animacion de sufrir da�o
            //CameraShake.Instance.ShakeCamera(1f, 0.5f);
            _currentLife -= damage;
            _flash?.Flash();
        }
        OnLifeChanged?.Invoke(_currentLife);
        if (_currentLife <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        GameManager.instance.LoseGame();
        OnDie?.Invoke();
        _isDead = true;
    }
    public void GetHealing(float amount)
    {

        if (_currentLife == maxlife)
        {
            return;
        }
        _currentLife += amount;
        if (_currentLife > maxlife)
        {
            _currentLife = maxlife;

        }
        OnLifeChanged?.Invoke(_currentLife);
    }
    public void ResetValues()
    {
        _currentLife = maxlife;
        OnLifeChanged?.Invoke(_currentLife);
    }
    public float GetLifePercentage()
    {
        return (float)_currentLife / maxlife;
    }

    IEnumerator Invulnerability(float time)
    {
        _invulnerable = true;
        yield return new WaitForSeconds(time);
        _invulnerable = false;
    }
}
