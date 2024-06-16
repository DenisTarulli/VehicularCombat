using UnityEngine;

public class TankTowerSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    private void Update()
    {
        SpinTower();   
    }

    private void SpinTower()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -spinSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
