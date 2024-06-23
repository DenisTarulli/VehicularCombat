using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsOver;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(Instance);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
