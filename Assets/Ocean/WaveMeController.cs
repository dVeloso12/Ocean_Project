using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMeController : MonoBehaviour
{
    [SerializeField] Vector2 Direction;
    [SerializeField] float WaveNumber;
    [SerializeField] float amplitude;
    [SerializeField] float speed;
    [SerializeField] float WaveLenght;
    [SerializeField] float Steepness;
    [SerializeField] float offset;
    [SerializeField] float h;


    public float timer;


    public float GetHeightME(Vector2 Position)
    {
        //  float phase_constant = speed * 2 * (Mathf.PI / WaveLenght);
        float phase_constant = speed*((2 * Mathf.PI) / WaveLenght);

        timer = Time.timeSinceLevelLoad;
        Vector2 normalizedDirection = Direction.normalized;
        float dots = Vector2.Dot(normalizedDirection, Position);

        h = -(amplitude * Mathf.Sin(WaveNumber * dots + phase_constant * timer));
      //h += offset;
        Debug.Log(h);
        return h;

    }
}
