using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [Header("Basic Parameters")]
    [Tooltip("List of Ocean Planes")]
    [SerializeField] List<GameObject> OceanList;
    [Tooltip("List of Ocean Renders")]
    [SerializeField] List<Renderer> MaterialList;
    [Tooltip("Ocean Material to get the values")]
    [SerializeField] Material OceanMaterial;

    [Header("Ocean Atributes")]
    [Tooltip("Change height values")]
    [SerializeField] float Amplitude;
    [Tooltip("Change the wave distance between waves")]
    [SerializeField] float WaveDistance;
    [Tooltip("Change the wave speed")]
    [SerializeField] float WaveSpeed;
    [Tooltip("Change the wave inclination")]
    [SerializeField] float WaveInclination;


    float lastHeight,LastWaveDistance,LastWaveSpeed,LastWaveInclination;
    float Frequencia, speed;
    float secondFreq, secondSpeed;
    Vector2 waveDirection,secondWaveDirection;

    private void Start() => getMaterials();
    void getMaterials()
    {
        foreach(GameObject water in OceanList)
        {
            MaterialList.Add(water.GetComponent<Renderer>());
        }
        setValues();
    }
    void setValues()
    {
        Amplitude = OceanMaterial.GetFloat("_Amplitude");
        WaveDistance = OceanMaterial.GetFloat("_Frequency");
        WaveSpeed = OceanMaterial.GetFloat("_Speed");
        WaveInclination = OceanMaterial.GetFloat("_Steepness");

        Frequencia = OceanMaterial.GetFloat("_Frequency");
        speed = OceanMaterial.GetFloat("_Speed");
        waveDirection = OceanMaterial.GetVector("_Direction");

        secondFreq = OceanMaterial.GetFloat("_Secondary_Frequency");
        secondSpeed = OceanMaterial.GetFloat("_Secondary_Speed");
        secondWaveDirection = OceanMaterial.GetVector("_Secondary_Direction");
    }

    void Update()
    {
        Height_Waves();
        Wave_Distance();
        Speed_Waves();
        Wave_Inclination();
    }
    void UpdateValues(string valueName,float value)
    {
        foreach (Renderer water in MaterialList)
        {
            water.material.SetFloat(valueName, value);
        }
    }
    public float getWaveHeight(float x,float time)
    {
        waveDirection.Normalize();
        secondWaveDirection.Normalize();
        waveDirection = -waveDirection;
        secondWaveDirection = -secondWaveDirection;
        

        float fristwave = Frequencia * waveDirection.x * x + (time * speed);

        float secondwave = secondWaveDirection.x * secondFreq * x +(time * secondSpeed);

       return Amplitude *(Mathf.Sin(fristwave)+Mathf.Sin(secondwave));
    }
    #region Change Height Waves
    void Height_Waves()
    {
        if (Amplitude != lastHeight)
        {
            UpdateValues("_Amplitude", Amplitude);
        }
        lastHeight = Amplitude;
    }
    #endregion
    #region Change Wave Distance
    void Wave_Distance()
    {
        if (WaveDistance != LastWaveDistance)
        {
            UpdateValues("_Frequency", WaveDistance);
        }
        LastWaveDistance = WaveDistance;

    }
    #endregion
    #region Change Wave Speed
    void Speed_Waves()
    {
        if (WaveSpeed != LastWaveSpeed)
        {
            UpdateValues("_Speed", WaveSpeed);
        }
        LastWaveSpeed = WaveSpeed;
    }
    #endregion
    #region Change Wave Inclination
    void Wave_Inclination()
    {
        if (WaveInclination != LastWaveInclination)
        {
            UpdateValues("_Steepness", WaveInclination);
        }
        LastWaveInclination = WaveInclination;
    }
    #endregion


}
