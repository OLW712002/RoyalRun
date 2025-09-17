using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FadeObject : MonoBehaviour
{
    public enum ComponentType { MeshRenderer, TMP_Text}

    [SerializeField] float fadeDuration = 1f;
    [SerializeField] ComponentType selectedType;

    MeshRenderer meshRenderer;
    TMP_Text tmpText;

    bool isFading = false;
    //string[] properties = new string[] {"_FaceColor", "_OutlineColor", "_UnderlayColor" };

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (transform.position.z < 10 && !isFading)
        {
            Debug.Log("Fading");
            isFading = true;
            StartCoroutine(FadeOut(selectedType));
        }
        
    }

    IEnumerator FadeOut(ComponentType componentType)
    {
        Color startColor;
        Color targetColor;
        float elapsedTime = 0f;
        switch (componentType)
        {
            case ComponentType.MeshRenderer:
                startColor = meshRenderer.material.color;
                targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
                while (elapsedTime < fadeDuration)
                {
                    meshRenderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                meshRenderer.enabled = false;
                break;
            case ComponentType.TMP_Text:

                Material mat = tmpText.fontMaterial;
                string[] properties = new string[] { "_FaceColor", "_OutlineColor", "_UnderlayColor" };
                Dictionary<string, Color> startColors = new();
                Dictionary<string, Color> targetColors = new();

                foreach(string property in properties)
                {
                    if (mat.HasProperty(property))
                    {
                        Color c = mat.GetColor(property);
                        startColors[property] = c;
                        targetColors[property] = new Color(c.r, c.g, c.b, 0f);
                    }
                }

                while (elapsedTime < fadeDuration)
                {
                    foreach(string property in startColors.Keys)
                    {
                        Color lerpedColor = Color.Lerp(startColors[property], targetColors[property], elapsedTime / fadeDuration);
                        mat.SetColor(property, lerpedColor);
                    }
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }


                //startColor = tmpText.material.color;
                //targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
                //while (elapsedTime < fadeDuration)
                //{
                //    tmpText.material.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
                //    elapsedTime += Time.deltaTime;
                //    yield return null;
                //}

                break;
        }
        
    }
}
