using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FadeBlack : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas;
    private Tween fadeTween;

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            fadeCanvas.interactable = true;
            fadeCanvas.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            fadeCanvas.interactable = false;
            fadeCanvas.blocksRaycasts = false;
        });
    }
    
    [ContextMenu("Fade")]
    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = fadeCanvas.DOFade(endValue, 1);
        fadeTween.onComplete += onEnd;
    }

    public IEnumerator FadeFull()
    {
        yield return new WaitForSeconds(1f);
        FadeIn(2);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.UpdateGameState(GameState.Event);
        yield return new WaitForSeconds(4f);
        FadeOut(1f);
        yield return new WaitForSeconds(1f);
        yield return null;
    }
}
