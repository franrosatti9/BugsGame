using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] HealthController _player;
    [SerializeField] GameObject lifebar;
    [SerializeField] float showTime;
    void OnEnable()
    {
        _player.OnLifeChanged += UpdateHealth;
    }

    void Update()
    {
        
    }

    public void UpdateHealth(float newHealth)
    {
        fill.fillAmount = newHealth / 100f;
        StartCoroutine(ShowHealth(showTime));
    }

    IEnumerator ShowHealth(float showTime)
    {
        lifebar.SetActive(true);
        yield return new WaitForSeconds(showTime);
        lifebar.SetActive(false);
    }

    private void OnDisable()
    {
        _player.OnLifeChanged -= UpdateHealth;
    }
}
