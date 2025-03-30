using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloudFadeUI : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            fadeImage.color = new Color(c.r, c.g, c.b, 0f); // Start fully transparent
        }
    }

    public void FadeIn(Action onComplete)
    {
        StartCoroutine(Fade(0f, 1f, onComplete));
    }

    public void FadeOut(Action onComplete)
    {
        StartCoroutine(Fade(1f, 0f, onComplete));
    }

    //actual fade logic
    private IEnumerator Fade(float from, float to, Action onComplete)
    {
        float time = 0f;
        Color baseColor = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, time / fadeDuration);
            fadeImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, to);
        onComplete?.Invoke();
    }
}
