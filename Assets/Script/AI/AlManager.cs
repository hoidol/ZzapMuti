using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


}
