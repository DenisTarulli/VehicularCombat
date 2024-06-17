using System.Collections;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float wheelRotationSpeed;
    [SerializeField] private float wheelTurnRotationSpeed;
    [SerializeField] private float recoveryCooldown;
    [SerializeField] private float groundcheckRayMaxDistance;
    [SerializeField] private GameObject[] leftWheels;
    [SerializeField] private GameObject[] rightWheels;
    [SerializeField] private Vector3 boxSize;

    private Rigidbody rigidBody;
    private float moveInput;
    private float rotationInput;
    private bool canRecover;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        canRecover = true;
    }

    private void Update()
    {
        GetInputs();
        RotateWheels(moveInput, rotationInput);

        if (Input.GetKeyDown(KeyCode.R) && canRecover)
            Recover();
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            MoveTank(moveInput);
            RotateTank(rotationInput);
        }
    }

    private void MoveTank(float input)
    {
        Vector3 moveDirection = input * moveSpeed * Time.fixedDeltaTime * transform.forward;
        rigidBody.MovePosition(rigidBody.position + moveDirection);
    }

    private void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);

        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private void RotateWheels(float moveInput, float rotationInput)
    {
        float wheelMoveRotation = moveInput * wheelRotationSpeed * Time.deltaTime;        
        float wheelTurnRotation = rotationInput * wheelTurnRotationSpeed * Time.deltaTime;        

        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                if (wheelMoveRotation != 0f)
                    wheel.transform.Rotate(wheelMoveRotation, 0f, 0f);
                else
                    wheel.transform.Rotate(wheelTurnRotation, 0f, 0f);
            }
        }

        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                if (wheelMoveRotation != 0f)
                    wheel.transform.Rotate(wheelMoveRotation, 0f, 0f);
                else
                    wheel.transform.Rotate(-wheelTurnRotation, 0f, 0f);
            }
        }
    }

    private void Recover()
    {
        if (IsGrounded()) return;

        StartCoroutine(nameof(RecoverCooldown));
        transform.eulerAngles = new(0f, transform.eulerAngles.y, 0f);
    }
    
    private IEnumerator RecoverCooldown()
    {
        canRecover = false;

        yield return new WaitForSeconds(recoveryCooldown);

        canRecover = true;
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, groundcheckRayMaxDistance))
            return true;
        else
            return false;
    }
}
