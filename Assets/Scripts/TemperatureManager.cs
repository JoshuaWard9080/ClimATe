using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class TemperatureManager : MonoBehaviour
{
    public UnityEvent OnTempChangeToWarm = new UnityEvent();
    public UnityEvent OnTempChangeToCold = new UnityEvent();
    public UnityEvent OnTempChangeToFreezing = new UnityEvent();
    public TemperatureState currTemp;

    //initialize player, platforms, winds, snow pile, icicles, Topi (enemies), and Nitpickers (enemies)
    [SerializeField] private PlayerMovement player;
    [SerializeField] private List<Icicle> icicles; //icicles
    [SerializeField] private List<AreaEffector2D> winds; //winds
    [SerializeField] private List<CrackingPlatform> crackingPlatforms; //platforms
    [SerializeField] private List<FuzzyEnemy> fuzzyEnemies; //fuzzy enemies

    //second enemy
    [SerializeField] private List<Bird_Enemy> birdEnemies;

    [SerializeField] private PilingSnow snowSystem; //snow storm

    private void Start()
    {
        //default state when starting game: this is just to avoid any weird bugs
        OnTempChangeToCold?.Invoke();
    }
    
    public void setTemp(TemperatureState newTemp)
    {
        if (currTemp == newTemp)
        {
            return;
        }

        currTemp = newTemp;
        Debug.Log("Temperature set to: " + newTemp);

        switch (newTemp)
        {
            case TemperatureState.Freezing:
                OnTempChangeToFreezing?.Invoke();
                break;
            case TemperatureState.Cold:
                OnTempChangeToCold?.Invoke();
                break;
            case TemperatureState.Warm:
                OnTempChangeToWarm?.Invoke();
                break;
        }
    }
}

/*
void ApplyFreezingTemp()
{
    //make the player speed slower
    //player.SetMoveSpeed(0.5f);

    //make platforms crack (then break) faster
    foreach (var cp in crackingPlatforms)
    {
        cp.SetCrackSpeed(3f);
        cp.SetMeltSpeed(0f);
    }

    //make the winds stronger
    foreach (var wind in winds)
    {
        //set wind strength and speed
        wind.SetWindStrength(3f);
        wind.SetWindSpeed(3f);
    }

    //make the snow pile faster
    snowSystem.SetSnowSpeed(0.5f);

    //make icicle size larger
    //set icicles to be able to fall
    foreach (var icicle in icicles)
    {
        icicle.CanFall(true);
        icicle.RegenerateSpeed(5f);
    }

    //set fuzzy (enemy) to push blocks and fill in the holes made by the player in the platforms/floors
    foreach (var fuzEnemy in fuzzyEnemies)
    {
        fuzEnemy.PushBlocks(true);
    }

    //set second enemy to not fly, only waddle along walls
    // foreach (var secondEnemy in secondEnemies)
    // {
    //     secondEnemy.FliesInCircle(false);
    //     secondEnemy.Waddle(true);
    // }
}

void ApplyColdTemp()
{
    //player speed remains the same, so standard speed
    //player.SetMoveSpeed(1f);

    //make platforms crack (then break) at standard speed
    foreach (var cp in crackingPlatforms)
    {
        //set crack speed
        cp.SetCrackSpeed(1f);
        cp.SetMeltSpeed(0f);
    }

    //winds are moderate, can push the player if they stand still for too long
    foreach (var wind in winds)
    {
        //set wind strength and speed
        wind.SetWindStrength(1f);
        wind.SetWindSpeed(1f);
    }

    //snow pile rises at standard speed
    snowSystem.SetSnowSpeed(0.1f);

    //icicles fall and regenerate normally
    foreach (var icicle in icicles)
    {
        icicle.CanFall(true);
        icicle.RegenerateSpeed(1f);
    }

    //set fuzzy (enemy) to push blocks and fill in the holes made by the player in the platforms/floors
    foreach (var fuzEnemy in fuzzyEnemies)
    {
        fuzEnemy.PushBlocks(true);
    }

    //set second enemy to fly in straight lines
    // foreach (var secondEnemy in secondEnemies)
    // {
    //     secondEnemy.FliesInLine(true);
    //     secondEnemy.FliesInCircle(false);
    //     secondEnemy.Waddle(false);
    // }
}

void ApplyWarmTemp()
{
    //make the player speed faster
    //player.SetMoveSpeed(3f);

    //make platforms crack AND melt faster
    foreach (var cp in crackingPlatforms)
    {
        //set crack speed
        cp.SetCrackSpeed(3f);
        cp.SetMeltSpeed(3f);
    }

    //no snowstorms, so set them to zero?
    foreach (var wind in winds)
    {
        //set wind strength and speed
        wind.SetWindStrength(0f);
        wind.SetWindSpeed(0f);
    }

    //make the snow pile at a slower pace
    snowSystem.SetSnowSpeed(0.05f);

    //make icicles smaller but they fall and regenerate quickly
    foreach (var icicle in icicles)
    {
        icicle.CanFall(true);
        icicle.RegenerateSpeed(3f);
    }

    //set fuzzy (enemy) to NOT push blocks
    foreach (var fuzEnemy in fuzzyEnemies)
    {
        fuzEnemy.PushBlocks(false);
    }

    //set second enemy to fly in circles
    // foreach (var secondEnemy in secondEnemies)
    // {
    //     secondEnemy.FliesInLine(false);
    //     secondEnemy.FliesInCircle(true);
    //     secondEnemy.Waddle(false);
    // }
}
}*/
