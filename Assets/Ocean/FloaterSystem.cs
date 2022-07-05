using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloaterSystem : MonoBehaviour
{
    public Transform[] listPoints;
    public float underWaterDrag = 3f;
    public float aiDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float underWaterAngularDrag = 1f;
    public float floatingPower = 15f;
    public float waterHeight = 0f;

    Rigidbody m_RigidBody;
    bool underwater;
    int floatersUnderwater;
    Vector3 Position;
    [SerializeField] WaveMeController cont;
    [SerializeField] float offset = 0f;

    float gameTime;
    float waveHeight;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        
        
    }
    private void Awake()
    {
        gameTime += Time.time;
    }
    private void Update()
    {
        gameTime += Time.time;
        
        Position = transform.position;
        waveHeight = cont.GetHeightME(new Vector2(Position.x, Position.z));
        //Debug.Log(waveHeight);
        transform.localPosition = new Vector3(transform.localPosition.x,waveHeight,transform.localPosition.z);

    }
    private void FixedUpdate()
    {

      //  SimpleObj_waves();
        
    }
    void SimpleObj_waves()
    {
        //float difference = transform.position.y - waterHeight;  
        float difference = transform.position.y - waveHeight;
        if (difference < waveHeight)
        {
            m_RigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if (!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if (underwater)
        {
            underwater = false;
            SwitchState(false);

        }
    }
    void SimpleObj()
    {
        //float difference = transform.position.y - waterHeight;
        float difference = transform.position.y - waveHeight;
        if (difference < 0)
        {
            m_RigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if (!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if (underwater)
        {
            underwater = false;
            SwitchState(false);

        }
    }
    void ComplexObj()
    {
        floatersUnderwater = 0;
        for(int i = 0; i < listPoints.Length; i++)
        {
            float difference = listPoints[i].position.y - waterHeight;
            if (difference < 0)
            {
                m_RigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), listPoints[i].position, ForceMode.Force);
                floatersUnderwater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }
     
        if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(false);

        }
    }
    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_RigidBody.drag = underWaterDrag;
            m_RigidBody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_RigidBody.drag = aiDrag;
            m_RigidBody.angularDrag = airAngularDrag;
        }
    }
}
