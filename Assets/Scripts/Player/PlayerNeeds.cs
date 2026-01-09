using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour, IDamagable
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

    public UnityEvent onTakeDamage;

    private bool _isDead;

    void Start()
    {
        _isDead = false;

        // set the start values (se startValue non impostato <=0, fallback a maxValue)
        health.curValue = (health.startValue > 0f) ? health.startValue : health.maxValue;
        hunger.curValue = (hunger.startValue > 0f) ? hunger.startValue : hunger.maxValue;
        thirst.curValue = (thirst.startValue > 0f) ? thirst.startValue : thirst.maxValue;
        sleep.curValue = (sleep.startValue > 0f) ? sleep.startValue : sleep.maxValue;

        // clamp iniziale per sicurezza (evita valori fuori range se Inspector è settato male)
        health.ClampToRange();
        hunger.ClampToRange();
        thirst.ClampToRange();
        sleep.ClampToRange();

        // se dopo l'inizializzazione l'health è zero o negativa, riparazione e warning
        if (health.curValue <= 0f)
        {
            Debug.LogWarning("Health startValue <= 0, impostato automaticamente a maxValue.");
            health.curValue = health.maxValue;
        }

        // update UI subito (così non resta vuota fino al primo frame)
        UpdateUI();
    }

    void Update()
    {
        // se morto, non continuare a far decadere/aggiornare UI ogni frame
        if (_isDead) return;

        // decay needs over time
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);

        // decay health over time if no hunger or thirst
        // uso <= 0 per evitare problemi di float equality
        if (hunger.curValue <= 0.0f)
            health.Subtract(noHungerHealthDecay * Time.deltaTime);

        if (thirst.curValue <= 0.0f)
            health.Subtract(noThirstHealthDecay * Time.deltaTime);

        // check if player is dead (una sola volta)
        if (health.curValue <= 0.0f)
        {
            _isDead = true;
            Die();
            UpdateUI(); // aggiorna UI finale (barra a 0 ecc.)
            return;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        // update UI bars (con null-check, evita spam di errori)
        if (health.uiBar) health.uiBar.fillAmount = health.GetPercentage();
        if (hunger.uiBar) hunger.uiBar.fillAmount = hunger.GetPercentage();
        if (thirst.uiBar) thirst.uiBar.fillAmount = thirst.GetPercentage();
        if (sleep.uiBar) sleep.uiBar.fillAmount = sleep.GetPercentage();
    }

    // adds to the player's HEALTH
    public void Heal(float amount)
    {
        if (_isDead) return;
        health.Add(amount);
        UpdateUI();
    }

    // adds to the player's HUNGER
    public void Eat(float amount)
    {
        if (_isDead) return;
        hunger.Add(amount);
        UpdateUI();
    }

    // adds to the player's THIRST
    public void Drink(float amount)
    {
        if (_isDead) return;
        thirst.Add(amount);
        UpdateUI();
    }

    // subtracts from the player's SLEEP
    public void Sleep(float amount)
    {
        if (_isDead) return;
        sleep.Subtract(amount);
        UpdateUI();
    }

    // called when the player takes physical damage (fire, enemy, etc)
    public void TakePhysicalDamage(int amount)
    {
        if (_isDead) return;

        health.Subtract(amount);
        onTakeDamage?.Invoke();

        if (health.curValue <= 0.0f)
        {
            _isDead = true;
            Die();
        }

        UpdateUI();
    }

    // called when the player's health reaches 0
    public void Die()
    {
        Debug.Log("Player is dead");
    }
}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float curValue;

    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    // add to the need
    public void Add(float amount)
    {
        // evita NaN/Infinity e clamp corretto anche se maxValue è 0
        if (maxValue <= 0.00001f)
        {
            curValue = 0f;
            return;
        }

        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // subtract from the need
    public void Subtract(float amount)
    {
        // se maxValue è invalido, comunque manteniamo curValue a 0
        if (maxValue <= 0.00001f)
        {
            curValue = 0f;
            return;
        }

        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // utility: clamp curValue dentro range valido
    public void ClampToRange()
    {
        if (maxValue <= 0.00001f)
        {
            curValue = 0f;
            return;
        }

        curValue = Mathf.Clamp(curValue, 0f, maxValue);
    }

    // return the percentage value (0.0 - 1.0)
    public float GetPercentage()
    {
        // evita NaN/Infinity se maxValue è 0 o negativo
        if (maxValue <= 0.00001f) return 0f;

        // clamp di sicurezza: se curValue sballa, non manda la UI fuori range
        float safe = Mathf.Clamp(curValue, 0f, maxValue);
        return safe / maxValue;
    }
}

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

