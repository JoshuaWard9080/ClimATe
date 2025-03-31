using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

//used to ensure that if there is more than one text field we consider that, otherwise we dont
//VictoryScene was causing issues due to the scroll behavior, where all texts were being considered
//Additionally, TextAnimation script has caused problems all throughout the scenes so this is a better solution overall
public enum TextAnimationMode
{
    ManualSingleText,
    MultiStatsText
}

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextAnimationMode mode;
    [SerializeField] private TextMeshProUGUI manualText;
    [SerializeField] TextMeshProUGUI[] texts;

    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenTexts = 0.5f;
    [SerializeField] AudioSource typingAudio;

    //victory scene fields
    [SerializeField] private int blinkCount = 5;
    [SerializeField] private float blinkInterval = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     foreach (TextMeshProUGUI text in texts)
    //     {
    //         text.maxVisibleCharacters = 0;
    //     }

    //     StartCoroutine(AnimateTexts());
    // }

    //set the textx
    public void SetTexts(TextMeshProUGUI[] newTexts)
    {
        texts = newTexts;
    }

    public IEnumerator StartAnimation()
    {
        if (mode == TextAnimationMode.ManualSingleText)
        {
            if (manualText != null)
            {
                manualText.maxVisibleCharacters = 0;
                yield return StartCoroutine(TypeText(manualText));
                yield return StartCoroutine(BlinkText(manualText));
                yield break;
            }
            else
            {
                Debug.Log("manualText is null in StartAnimation of LevelCompletetextAnimation");
                yield break;
            }
        }

        if (texts == null || texts.Length == 0)
        {
            Debug.Log("textAnimation script has no texts to type out/animate");
        }

        foreach (var text in texts)
        {
            text.maxVisibleCharacters = 0;
        }

        yield return StartCoroutine(AnimateTexts());
    }

    private IEnumerator AnimateTexts()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        for (int i = 0; i < texts.Length; i++)
        {
            if (i == texts.Length - 1 && currentScene.name == "VictoryScene")
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

        textMesh.text = fullText;
        yield return null;

        textMesh.ForceMeshUpdate();

        int totalCharacters = textMesh.textInfo.characterCount;
        int visibleCharacters = 0;


        while (visibleCharacters <= totalCharacters)
        {
            textMesh.maxVisibleCharacters = visibleCharacters;

            if (typingAudio != null)
            {
                typingAudio.PlayOneShot(typingAudio.clip); //plays the audio only once, so no looping can occur
            }

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
