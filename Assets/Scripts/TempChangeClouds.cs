using System;
using UnityEngine;

public class TempChangeClouds : MonoBehaviour
{
    [SerializeField] TemperatureManager temperatureManager;
    float moveSpeed = 0.003f;
    Color warmColour = new Color(81 / 100f, 72 / 100f, 60 / 100f);
    Color coldColour = new Color(180 / 255f, 226 / 255f, 255 / 255f);
    Color freezingColour = new Color(8 / 100f, 16 / 100f, 100 / 100f);
    Boolean hasTempChanged = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTempChanged) return;


    }

    void tempChangeToWarm()
    {
        changeCloudColour(warmColour);
        hasTempChanged = true;
    }

    void tempChangeToCold()
    {
        changeCloudColour(coldColour);
        hasTempChanged = true;
    }

    void tempChangeToFreezing()
    {
        changeCloudColour(freezingColour);
        hasTempChanged = true;
    }

    void changeCloudColour(Color colour)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = colour;
            }
        }
    }
}
