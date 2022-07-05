using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManeger : MonoBehaviour
{
    public static WaveManeger instace;

    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequencia = 2f;
    [SerializeField] float speed = 1f;
    [SerializeField] float offset = 0f;




    private void Awake()
    {
        if(instace == null)
        {
            instace = this;
        }
        else if (instace != null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;   
    }
    public float GetHeight(float _x)
    {
        Vector2 waveDirection = new Vector2(1f, 1f);
        waveDirection.Normalize();
        float fristwave = frequencia * waveDirection.x * _x + (offset);

        return amplitude * Mathf.Sin(fristwave);
    }


}
