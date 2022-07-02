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
    [SerializeField] float Height;
    [Tooltip("Change the wave distance between waves")]
    [SerializeField] float WaveDistance;
    [Tooltip("Change the wave speed")]
    [SerializeField] float WaveSpeed;
    [Tooltip("Change the wave inclination")]
    [SerializeField] float WaveInclination;


    float lastHeight,LastWaveDistance,LastWaveSpeed,LastWaveInclination;
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
        Height = OceanMaterial.GetFloat("_Amplitude");
        WaveDistance = OceanMaterial.GetFloat("_Frequency");
        WaveSpeed = OceanMaterial.GetFloat("_Speed");
        WaveInclination = OceanMaterial.GetFloat("_Steepness");
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
    #region Change Height Waves
    void Height_Waves()
    {
        if (Height != lastHeight)
        {
            UpdateValues("_Amplitude", Height);
        }
        lastHeight = Height;
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
