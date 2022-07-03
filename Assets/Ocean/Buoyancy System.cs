using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //adiciona logo rigdbody
public class BuoyancySystem : MonoBehaviour
{
    public float underWaterDrag = 3f;
    public float aiDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float underWaterAngularDrag = 1f;
    public float floatingPower = 15f;
    public float waterHeight = 0f;

    Rigidbody m_RigidBody;
    bool underwater;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float difference = transform.position.y - waterHeight;
        if(difference < 0)
        {
            m_RigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if(!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if(underwater)
        {
            underwater = false;
            SwitchState(false);

        }

    }
    void SwitchState(bool isUnderwater)
    {
        if(isUnderwater)
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
