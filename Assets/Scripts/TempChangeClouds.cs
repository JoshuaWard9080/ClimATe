using System;
using UnityEngine;

public class TempChangeClouds : MonoBehaviour
{
    [SerializeField] TemperatureManager temperatureManager;
    [SerializeField] Transform player;
    float moveSpeed = -0.08f;
    Color warmColour = new Color(81 / 100f, 72 / 100f, 60 / 100f);
    Color coldColour = new Color(180 / 255f, 226 / 255f, 255 / 255f);
    Color freezingColour = new Color(32 / 100f, 50 / 100f, 92 / 100f);
    String currentTemp = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureManager.OnTempChangeToWarm.AddListener(tempChangeToWarm);
        temperatureManager.OnTempChangeToCold.AddListener(tempChangeToCold);
        temperatureManager.OnTempChangeToFreezing.AddListener(tempChangeToFreezing);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            float thetaMultiplier = 0.8f+(27 % (i + 1)) / 23; 
            transform.GetChild(i).transform.position = new Vector3(
                transform.GetChild(i).transform.position.x +moveSpeed,
                player.position.y-6+ 2*thetaMultiplier+((float)(thetaMultiplier*((1/2.0)*Math.Sin((1 / 2.0) * transform.GetChild(i).transform.position.x)))),
                0);


        }


    }

    void tempChangeToWarm()
    {
        if (!currentTemp.Equals("Warm"))
        {
            changeCloudColour(warmColour);
            resetCloudPosition();
            currentTemp = "Warm";
        }
        
    }

    void tempChangeToCold()
    {
        if (!currentTemp.Equals("Cold"))
        {
            changeCloudColour(coldColour);
            resetCloudPosition();
            currentTemp = "Cold";
        }
        
    }

    void tempChangeToFreezing()
    {
        if (!currentTemp.Equals("Freezing"))
        {
            changeCloudColour(freezingColour);
            resetCloudPosition();
            currentTemp = "Freezing";
        }
    }

    void resetCloudPosition()
    {
       float  distanceTravelled = -transform.GetChild(0).transform.position.x+11f;

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
