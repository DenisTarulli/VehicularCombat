using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class CarController : MonoBehaviour
{
    [Header("Car values")]
    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steeringRange;
    [SerializeField] private float steeringRangeAtMaxSpeed;
    [SerializeField] private float centreOfGravityOffset;

    WheelController[] wheels;
    private Rigidbody rigidBody;
    private float gasInput;
    private float steerInput;

    

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        wheels = GetComponentsInChildren<WheelController>();
    }

    private void Update()
    {
        GetInputs();

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);

        float speedFactor = Mathf.InverseLerp(0f, maxSpeed, forwardSpeed);

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0f, speedFactor);

        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(gasInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
                wheel.wheelCollider.steerAngle = steerInput * currentSteerRange;

            if (isAccelerating)
            {
                if (wheel.motorized)
                    wheel.wheelCollider.motorTorque = gasInput * currentMotorTorque;

                wheel.wheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = Mathf.Abs(gasInput) * brakeTorque;
                wheel.wheelCollider.motorTorque = 0f;
            }

        }
    }

    private void GetInputs()
    {
        gasInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

}


