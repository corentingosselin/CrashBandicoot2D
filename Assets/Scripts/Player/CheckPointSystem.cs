using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{

    private static CheckPointSystem instance;
    
    public List<Vector3> checkpointsUnlocked = new List<Vector3>();
    public int lastIdentifier = -1;
    
    public int lifeAmount = 0;
    public int wumpaFruitAmount = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 position, int identifier)
    {
        this.lastIdentifier = identifier;
        checkpointsUnlocked.Add(position);
        
        foreach (CheckPointBox checkPointBox in FindObjectsOfType<CheckPointBox>())
        {
            if (checkPointBox.identifier < lastIdentifier)
            {
                Destroy(checkPointBox.gameObject);
            }
        }
    }

    public void ResetCheckPoint()
    {
        checkpointsUnlocked.Clear();
        lastIdentifier = -1;

    }
    
    
}