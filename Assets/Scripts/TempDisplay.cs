using UnityEngine;
using UnityEngine.UI;

public class TempDisplay : MonoBehaviour
{

    [SerializeField] TemperatureManager temperatureManager;

    [SerializeField] Sprite coldTemp;
    [SerializeField] Sprite warmTemp;
    [SerializeField] Sprite freezingTemp;

    Image imageComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        imageComponent = GetComponent<Image>();
    }

    void tempChangeToFreezing()
    {
        changeSprite(freezingTemp);
    }

    void tempChangeToCold()
    {
        changeSprite(coldTemp);
    }

    void tempChangeToWarm()
    {
        changeSprite(warmTemp);
    }

    void changeSprite(Sprite newSprite)
    {
        imageComponent.sprite = newSprite;
    }
}
