using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Transform wheelModel;

    [HideInInspector] public WheelCollider wheelCollider;

    public bool steerable;
    public bool motorized;

    private Vector3 position;
    private Quaternion rotation;

    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelModel.SetPositionAndRotation(position, rotation);
    }
}
