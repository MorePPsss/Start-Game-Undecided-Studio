using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText; // Reference to the UI Text component for subtitles
    public CanvasGroup subtitleCanvasGroup; // Reference to the CanvasGroup for fading effects

    public IEnumerator ShowSubtitle(string message, float duration)
    {
        // Set the subtitle text
        subtitleText.text = message;

        // Fade in
        yield return FadeCanvasGroup(0, 1, 0.5f);

        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Fade out
        yield return FadeCanvasGroup(1, 0, 0.5f);

        // Clear the subtitle text
        subtitleText.text = "";
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            subtitleCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        subtitleCanvasGroup.alpha = endAlpha;
    }
}
