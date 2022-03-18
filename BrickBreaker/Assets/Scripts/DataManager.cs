using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private Observation observation;
    private float episodeDuration = 0.0f;
    private int numberOfSteps = 0;
    private float minQValue = Mathf.Infinity;
    private float maxQValue = Mathf.NegativeInfinity;
    private float qValue = 0.0f;
    private int brokenBlocks = 0;
    private int hitCount = 0;

    public DataManager()
    {
        observation = new Observation();
    }

    public void UpdateTimer(float timer)
    {
        episodeDuration += timer;
        numberOfSteps++;
    }

    public void IncreaseHitCount()
    {
        hitCount++;
    }

    public void IncreaseBrokenBlocks()
    {
        brokenBlocks++;
    }

    public void SetMinMaxQValues(float qValue)
    {
        if(qValue > maxQValue)
        {
            maxQValue = qValue;
        }
        if(qValue < minQValue)
        {
            minQValue = qValue;
        }
    }

    public float GetMinQValue()
    {
        return minQValue;
    }

    public float GetMaxQValue()
    {
        return maxQValue;
    }

    public void SetQValue(float q)
    {
        qValue = q;
    }

    public void RegisterObservation()
    {
        observation.episodeDuration = episodeDuration;
        observation.numberOfSteps = numberOfSteps;
        observation.qMin = minQValue;
        observation.qMax = maxQValue;
        observation.qValue = qValue;
        observation.brokenBlocks = brokenBlocks;
        observation.hitCount = hitCount;

        observation.SaveToFile(append:true);
    }

    public void Reset()
    {
        episodeDuration = 0.0f;
        numberOfSteps = 0;
        brokenBlocks = 0;
        hitCount = 0;
    }
}
