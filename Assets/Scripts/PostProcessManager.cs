using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] Volume globalVolume;
    [SerializeField] Volume glitchVolume;
    ChromaticAberration chromaticAberration;
    Vignette vignette;


    [SerializeField] AnimationCurve chromaticAberrationCurve;
    [SerializeField] AnimationCurve vignetteCurve;
    [SerializeField] ColorParameter vignetteColor;
    [SerializeField] ColorParameter defaultVignetteColor;
    float defaultVignetteIntensity;

    [SerializeField] float shootEffectDuration;
    [SerializeField] float damageEffectDuration;

    [Header("Glitch Settings")]
    AnalogGlitchVolume analogGlitchVolume;
    DigitalGlitchVolume digitalGlitchVolume;
    [SerializeField] float minScanLineJitter;
    [SerializeField] float maxScanLineJitter;
    float scanLineJitterAmount;

    [SerializeField] float minColorDrift;
    [SerializeField] float maxColorDrift;
    float colorDriftAmount;
    private void OnEnable()
    {
        GameManager.instance.OnCurrentBugsModified += HandleBugsAmount;
    }
    void Start()
    {
        globalVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        globalVolume.profile.TryGet<Vignette>(out vignette);
        defaultVignetteIntensity = vignette.intensity.value;
        //defaultVignetteColor = (ColorParameter)vignette.color;

        glitchVolume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
        glitchVolume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);

        HandleBugsAmount(0);

    }
    
    public void HandleBugsAmount(int amount)
    {
        if(amount <= 0)
        {
            glitchVolume.enabled = false;
        }
        else if(amount > 0 && amount < 3)
        {
            glitchVolume.enabled = true;
            digitalGlitchVolume.active = false;
            analogGlitchVolume.verticalJump.value = 0f;
        }
        else if(amount > 3)
        {
            digitalGlitchVolume.active = true;
            if(amount == 4)
            {
                analogGlitchVolume.verticalJump.value = 0.03f;
            }
        }

        float diff = GameManager.instance.MaxBugsToLose - amount;

        scanLineJitterAmount = math.remap(0f, GameManager.instance.MaxBugsToLose, minScanLineJitter, maxScanLineJitter, amount);

        colorDriftAmount = math.remap(0f, GameManager.instance.MaxBugsToLose, minColorDrift, maxColorDrift, amount);

        analogGlitchVolume.scanLineJitter.value = scanLineJitterAmount;
        analogGlitchVolume.colorDrift.value = colorDriftAmount;

        /*if(amount > GameManager.instance.MaxBugsToLose)
        {
            
        }*/
    }
    
    
    // TODO: Implementar bien
    /*
    public void ShootEffect()
    {
        StartCoroutine(AnimateShootEffect());
    }

    public void DamageEffect(float healthLeft)
    {
        float diff = 100f - healthLeft;

        float intensity = Unity.Mathematics.math.remap(0f, 100f, 0.25f, 0.35f, diff);


        StartCoroutine(AnimateDamageEffect(intensity));
    }

    IEnumerator AnimateDamageEffect(float intensity)
    {
        float time = 0f;
        float duration = damageEffectDuration;
        vignette.color.SetValue(vignetteColor);
        vignette.intensity.value = intensity;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        vignette.color.SetValue(defaultVignetteColor);
        //vignette.intensity.value = defaultVignetteIntensity;
    }

    IEnumerator AnimateShootEffect()
    {
        float time = 0f;
        float duration = shootEffectDuration;
        while (time < duration)
        {
            chromaticAberration.intensity.value = chromaticAberrationCurve.Evaluate(time);
            //vignette.intensity.value = vignetteCurve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;
        }
        chromaticAberration.intensity.value = chromaticAberrationCurve.keys[chromaticAberrationCurve.length - 1].value;
        //vignette.intensity.value = vignetteCurve.keys[vignetteCurve.length - 1].value;
    }
    */
}
