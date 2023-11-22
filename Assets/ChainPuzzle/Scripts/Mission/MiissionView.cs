using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiissionView : MonoBehaviour
{
    [SerializeField] Mission missionPrefab;
    [SerializeField] Transform missionRoot;

    public void SetUp(MissionData[] dataArr)
    {
        SetMission(dataArr);
    }

    private void SetMission(MissionData[] dataArr)
    {
        for (int i = 0; i < dataArr.Length; i++)
        {
            var data = dataArr[i];
            var mission = Instantiate(missionPrefab, missionRoot);
            mission.SetUp(data);
        }
    }
}
