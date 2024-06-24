using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Transform minimapCamera;
    private GameObject[] icons;

    private void Start()
    {
        icons = GameObject.FindGameObjectsWithTag("Minimap");

        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void Update()
    {
        minimapCamera.eulerAngles = new Vector3(90f, Camera.main.transform.eulerAngles.y, 0f);
    }
}
