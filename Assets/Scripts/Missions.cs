using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Missions : MonoBehaviour
{
    public static Missions Instance { get; private set; }
    public MissionStruct[] missionsSaved;
    
    [Serializable]
    public struct MissionStruct
    {
        public string explanation;
        public int completed;
        public int required;
        public int gold;
    }

    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
