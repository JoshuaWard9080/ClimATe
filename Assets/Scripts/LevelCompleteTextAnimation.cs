using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] texts;

    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenTexts = 0.5f;
    [SerializeField] AudioSource typingAudio;

    //victory scene fields
    [SerializeField] private int blinkCount = 5;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] string victoryStatsScene = "VictoryStatsScene";

    //set the textx
    public void SetTexts(TextMeshProUGUI[] newTexts)
    {
        texts = newTexts;
    }

    public IEnumerator StartAnimation()
    {
        Debug.Log("StartAnimation called with " + texts.Length + " texts");

        yield return null; //break to ensure text has been updated with stats

        foreach (TextMeshProUGUI text in texts)
        {
            text.maxVisibleCharacters = 0;
        }

        StartCoroutine(AnimateTexts());
    }

    private IEnumerator AnimateTexts()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (i == texts.Length - 1 && gameObject.scene.name == "VictoryScene")
            {
                yield return StartCoroutine(TypeText(texts[i]));
                yield return StartCoroutine(BlinkText(texts[i]));
            }
            else
            {
                yield return StartCoroutine(TypeText(texts[i]));
                yield return new WaitForSeconds(timeBetweenTexts);
            }
        }
    }

    private IEnumerator TypeText(TextMeshProUGUI textMesh)
    {
        string fullText = textMesh.text;
        textMesh.ForceMeshUpdate();

        int totalCharacters = textMesh.textInfo.characterCount;
        int visibleCharacters = 0;


        while (visibleCharacters <= totalCharacters)
        {
            textMesh.maxVisibleCharacters = visibleCharacters;
            typingAudio.Play();
            visibleCharacters++;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }

    private IEnumerator BlinkText(TextMeshProUGUI textMesh)
    {
        textMesh.ForceMeshUpdate();
        int totalCharacters = textMesh.textInfo.characterCount;

        if (totalCharacters == 0)
        {
            yield break;
        }

        int finalCharacterIndex = totalCharacters - 1;

        textMesh.maxVisibleCharacters = totalCharacters;

        for (int i = 0; i < blinkCount; i++)
        {
            textMesh.maxVisibleCharacters = finalCharacterIndex;
            yield return new WaitForSeconds(blinkInterval);

            textMesh.maxVisibleCharacters = totalCharacters;
            yield return new WaitForSeconds(blinkInterval);
        }

        FindObjectOfType<VictorySceneManager>()?.BeginScrolling();
    }
}
