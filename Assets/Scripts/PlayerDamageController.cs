using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public Color tintColor;
    public float tintFactor;
    public float fullTintDuration;
    public float fadingTintDuration;
    public GameObject fadeSpace;

    private Material fadeMat;
    private MeshRenderer fadeRenderer;

    private bool isFading = false;

    public float currentAlpha { get; private set; }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Laser")
        {
            Debug.Log("Collided");
            StartCoroutine(Fade(tintFactor, 0));
        }
    }

    void Start()
    {
        currentAlpha = 0;

        fadeRenderer = fadeSpace.GetComponent<MeshRenderer>();
        fadeMat = fadeRenderer.material;

        SetMaterialAlpha();
    }


    /// <summary>
    /// Fades alpha from 1.0 to 0.0
    /// </summary>
    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadingTintDuration + fullTintDuration)
        {
            elapsedTime += Time.deltaTime;
            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01((elapsedTime - fullTintDuration) / fadingTintDuration));
            SetMaterialAlpha();
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Update material alpha. UI fade and the current fade due to fade in/out animations (or explicit control)
    /// both affect the fade. (The max is taken)
    /// </summary>
    private void SetMaterialAlpha()
    {
        Color color = tintColor;
        color.a = currentAlpha;
        isFading = color.a > 0;
        if (fadeMat != null)
        {
            fadeMat.color = color;
            fadeMat.renderQueue = 5000;
            fadeRenderer.material = fadeMat;
            fadeRenderer.enabled = isFading;
        }
    }
}
