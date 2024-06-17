using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;

    [SerializeField]
    Rigidbody objectToThrow;

    [SerializeField, Range(0.0f, 50.0f)]
    float force;

    [SerializeField]
    Transform StartPosition;

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;
    }

    private void Update()
    {
        Predict();

        if (Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
    }

    private void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    private void ThrowObject()
    {
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }
}
