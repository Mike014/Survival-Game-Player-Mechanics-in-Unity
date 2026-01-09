using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultipler;

    void Start ()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    void Update ()
    {
        // increment time
        time += timeRate * Time.deltaTime;

        if(time >= 1.0f)
            time = 0.0f;

        // light rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        // light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        // change colors
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        // enable / disable the sun
        if(sun.intensity == 0 && sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(false);
        else if(sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(true);

        // enable / disable the moon
        if(moon.intensity == 0 && moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(false);
        else if(moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(true);

        // lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultipler.Evaluate(time);
    }
}

// ────────────────────────────────────────────────────────────────────────────
// COMPONENTI / DATI DEL SOLE (Sun)
// ────────────────────────────────────────────────────────────────────────────
// public Light sun;
// - Riferimento al componente Light del “Sole” in scena.
// - IMPORTANTISSIMO: devi trascinare qui la Directional Light del sole nell’Inspector.
// - Se è null, non puoi modificare né intensità né colore.

// public Gradient sunColor;
// - Gradiente colore in base a “time”.
// - Esempio tipico:
//   - alba: arancio/rosa
//   - mezzogiorno: bianco/giallo
//   - tramonto: rosso/viola
//   - notte: blu scuro

// public AnimationCurve sunIntensity;
// - Curva che controlla l’intensità della luce in base a “time”.
// - Tipicamente:
//   - notte: 0
//   - alba: sale
//   - mezzogiorno: picco
//   - tramonto: scende
//   - notte: torna a 0
