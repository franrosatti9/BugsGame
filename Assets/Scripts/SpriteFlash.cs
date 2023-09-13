using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] float duration;

    SpriteRenderer _spriteRenderer;
    Material _originalMaterial;
    Coroutine _flashRoutine;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    public void Flash()
    {
        if(_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        _spriteRenderer.material = _originalMaterial;
        _flashRoutine = null;
    }
}
