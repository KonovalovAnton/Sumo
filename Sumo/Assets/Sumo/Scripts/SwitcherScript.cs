using UnityEngine;
using System.Collections;

public class SwitcherScript : MonoBehaviour
{

    public GameObject fader;
    private SpriteRenderer faderRenderer;

    // Use this for initialization
    void Start()
    {
        faderRenderer = fader.GetComponent<SpriteRenderer>();
        print(faderRenderer.material.color.a);
        StartCoroutine(FadeOut(0.0f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeOut(float aValue, float aTime)
    {
        yield return new WaitForSeconds(10.0f);

        float alpha = faderRenderer.material.color.a;

        for (float t = 0.0f; t < 2.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            faderRenderer.material.color = newColor;
            yield return null;
        }
    }
}