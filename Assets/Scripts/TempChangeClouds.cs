using System;
using UnityEngine;

public class TempChangeClouds : MonoBehaviour
{
    [SerializeField] TemperatureManager temperatureManager;
    [SerializeField] Transform player;
    float moveSpeed = -0.013f;
    Color warmColour = new Color(81 / 100f, 72 / 100f, 60 / 100f);
    Color coldColour = new Color(180 / 255f, 226 / 255f, 255 / 255f);
    Color freezingColour = new Color(16 / 100f, 42 / 100f, 100 / 100f);
    Boolean hasTempChanged = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
        hasTempChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTempChanged) return;

        for (int i = 0; i < transform.childCount; i++)
        {
            float thetaMultiplier = 0.8f+(27 % (i + 1)) / 27; 
            transform.GetChild(i).transform.position = new Vector3(
                transform.GetChild(i).transform.position.x +moveSpeed,
                player.position.y+ 2*thetaMultiplier+((float)(thetaMultiplier*((1/2.0)*Math.Sin((1 / 2.0) * transform.GetChild(i).transform.position.x)))),
                0);


        }


    }

    void tempChangeToWarm()
    {
        changeCloudColour(warmColour);
        resetCloudPosition();
        hasTempChanged = true;
    }

    void tempChangeToCold()
    {
        changeCloudColour(coldColour);
        resetCloudPosition();
        hasTempChanged = true;
    }

    void tempChangeToFreezing()
    {
        changeCloudColour(freezingColour);
        resetCloudPosition();
        hasTempChanged = true;
    }

    void resetCloudPosition()
    {
       float  distanceTravelled = -transform.GetChild(0).transform.position.x-11f;

        for (int i = 0; i < transform.childCount; i++)
        {
            
            transform.GetChild(i).transform.position += new Vector3(
                distanceTravelled,
                0,  
                0);


        }

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
