using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiissionView : MonoBehaviour
{
    [SerializeField] Mission missionPrefab;
    [SerializeField] Transform missionRoot;

    private List<Mission> dataList;

    public void SetUp(MissionData[] dataArr)
    {
        dataList = new List<Mission>();
        SetMission(dataArr);
    }

    public void UpdateMission(MissionData data)
    {
        var mission = TrySarchData(data);
        if (mission != null)
        {
            mission.UpdateData(data);
        }
    }

    private Mission TrySarchData(MissionData data)
    {
        foreach(var mission in dataList)
        {
            if(mission.Data == data)
            {
                return mission;
            }

        }
        return null;
    }

    private void SetMission(MissionData[] dataArr)
    {
        for (int i = 0; i < dataArr.Length; i++)
        {
            var data = dataArr[i];
            var mission = Instantiate(missionPrefab, missionRoot);
            mission.SetUp(data);
            dataList.Add(mission);
        }
    }
}
