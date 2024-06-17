using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    [SerializeField] private float tiltSpeed;
    [SerializeField] private float maxTilt;
    [SerializeField] private float minTilt;

    private void Update()
    {
        TiltCanon();
        CanonTiltClamp();
    }

    private void TiltCanon()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Rotate(Vector3.right, -tiltSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
            transform.Rotate(Vector3.right, tiltSpeed * Time.deltaTime);        
    }

    private void CanonTiltClamp()
    {
        if (transform.localEulerAngles.x >= 0f && transform.localEulerAngles.x <= 15f || transform.localEulerAngles.x > minTilt)
            transform.localEulerAngles = new(minTilt, 0f, 0f);
        else if (transform.localEulerAngles.x < maxTilt && transform.localEulerAngles.x >= 270f)
            transform.localEulerAngles = new(maxTilt, 0f, 0f);
    }
}
