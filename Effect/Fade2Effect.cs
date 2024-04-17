using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeOutIn, FadeLoop };

public class Fade2Effect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime = 0f;
    [SerializeField]
    private AnimationCurve fadeCurve;
    [SerializeField]
    private Image image; // 페이드 효과에 사용되는 이미지
    private FadeState fadeState; //페이드 효과 상태

    private void Awake()
    {
        OnFade("FadeIn");
    }

    public void EffectStart(string state)
    {
        OnFade(state);
    }

    public void OnFade(string fadeState)
    {
        switch (fadeState)
        {
            case "FadeIn":
                StartCoroutine(Fade(1, 0));
                break;
            case "FadeOut":
                StartCoroutine(Fade(0, 1));
                break;
            case "FadeInOut":
            case "FadeOutIn":
            case "FadeLoop":
                StartCoroutine(FadeInOut());
                break;

        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));

            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }
    }
    private IEnumerator FadeOutIn()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1));

            yield return StartCoroutine(Fade(1, 0));

            if (fadeState == FadeState.FadeOutIn)
            {
                break;
            }
        }
    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }
}
