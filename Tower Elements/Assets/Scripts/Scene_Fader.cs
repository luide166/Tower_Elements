using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Fader : MonoBehaviour
{
    public Image image;
    [Header("Fade In")]
    [Space(3)]
    public float fadeInTime;
    public AnimationCurve curveIn;
    [Header("Fade Out")]
    [Space(3)]
    public float fadeOutTime;
    public AnimationCurve curveOut;

    private void Awake()
    {
        image.color = new Color(0f, 0f, 0f, 1f);
    }
    public void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = fadeInTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curveIn.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    public void FadeTo(string _scene)
    {
        StartCoroutine(FadeOut(_scene));
    }

    IEnumerator FadeOut(string _scene)
    {
        float t = 0;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float a = curveIn.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        print("terminei o fade, carregando cena");
        SceneManager.LoadScene(_scene);
    }

}
