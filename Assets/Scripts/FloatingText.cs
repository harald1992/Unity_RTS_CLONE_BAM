using System.Collections;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 0.5f;
    public float speed = 0.5f;

    Vector3 startPosition;
    Vector3 destination;
    TextMesh textMesh;

    void Start()
    {
        startPosition = gameObject.transform.position;
        destination = startPosition + new Vector3(0, 1, 0);
        textMesh = gameObject.GetComponent<TextMesh>();
        StartCoroutine(FadeMaterialToTargetAlpha());
        Debug.Log(gameObject.transform.position);
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);


    }

    // void ChangeTextOpacityValue(float alphaValue)
    // {
    //     if (textMesh != null)
    //     {
    //         Color textColor = textMesh.color;
    //         textColor.a = alphaValue;
    //         textMesh.color = textColor;
    //     }
    // }

    IEnumerator FadeMaterialToTargetAlpha()
    {
        float currentAlpha = textMesh.color.a;
        float elapsedTime = 0.0f;

        while (elapsedTime < destroyTime)
        {
            float newAlpha = Mathf.Lerp(currentAlpha, 0, elapsedTime / destroyTime);

            Color newColor = textMesh.color;
            newColor.a = newAlpha;
            textMesh.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Color finalColor = textMesh.color;
        finalColor.a = 0;
        textMesh.color = finalColor;
    }
}

