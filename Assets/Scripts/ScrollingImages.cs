using UnityEngine;
using UnityEngine.UI;

public class ScrollingClouds : MonoBehaviour
{
    public RawImage rawImage1;
    public RawImage rawImage2;
    [SerializeField] private float speed;
    private float imageWidth;

    private void Start()
    {
        imageWidth = rawImage1.rectTransform.rect.width;

        rawImage2.rectTransform.anchoredPosition = new Vector2(imageWidth, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmount = speed * Time.deltaTime;

        rawImage1.rectTransform.anchoredPosition -= new Vector2(moveAmount, 0);
        rawImage2.rectTransform.anchoredPosition -= new Vector2(moveAmount, 0);

        if (rawImage1.rectTransform.anchoredPosition.x <= -imageWidth)
        {
            rawImage1.rectTransform.anchoredPosition = new Vector2(rawImage2.rectTransform.anchoredPosition.x + imageWidth, 0);
        }

        if (rawImage2.rectTransform.anchoredPosition.x <= -imageWidth)
        {
            rawImage2.rectTransform.anchoredPosition = new Vector2(rawImage1.rectTransform.anchoredPosition.x + imageWidth, 0);
        }
    }
}
