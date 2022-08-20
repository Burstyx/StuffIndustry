using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public int crypterMission = 0;
    public GameObject mailLinkToCryterMission;

    public int antivirusMission = 0;
    public GameObject mailLinkToAntivirusMission;

    public bool inMission = false;


    public int antivirusMissionDone = 0;
    public int crypterMissionDone = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.Log("MissionManager instance already created!");
    }
}
