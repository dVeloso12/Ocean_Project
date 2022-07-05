using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float steepness;
    [SerializeField] float wavelenght;

    [SerializeField] float depthBeforeSubmerged = 1f;
    [SerializeField] float cubeVolume = 3f;
    [SerializeField] int floaterCount = 1;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] float waterAngularDrag = 0.5f;

    float CalculateHeight(Vector3 position)
    {
        float time = Time.timeSinceLevelLoad;

        Vector3 currentPosition = WaveController.GerstnerWave(position,direction,steepness,wavelenght,speed,time);

        for (int i = 0; i < 3; i++)
        {
            Vector3 diff = new Vector3(position.x - currentPosition.x, 0, position.z - currentPosition.z);
            currentPosition = WaveController.GerstnerWave(position, direction, steepness, wavelenght, speed, time);

        }
        return currentPosition.y;
    }

    private void Update()
    {
        float y = CalculateHeight(transform.position);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    //private void FixedUpdate()
    //{
    //    rigidbody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

    //    float waveHeight = CalculateHeight(transform.position);
    //    if (transform.position.y < waveHeight)
    //    {
    //        float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * cubeVolume;
    //        rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
    //        rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
    //        rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

    //    }
    //}

}
