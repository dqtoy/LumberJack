using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinPanelControler : MonoBehaviour
{
    [SerializeField] GameObject button;

    [SerializeField] float fadeinTime;
    Color semiTransparent = new Color(0, 0, 0, 0.5f);

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    public float GetFadeOutTime()
    {
        return fadeinTime;
    }

    private IEnumerator FadeInRoutine()
    {
        Image image = GetComponent<Image>();
        for (float t = 0.01f; t < fadeinTime; t += Time.deltaTime)
        {
            image.color = Color.Lerp(Color.clear, semiTransparent, Mathf.Min(1, t / fadeinTime));
            yield return null;
        }
    }
}
