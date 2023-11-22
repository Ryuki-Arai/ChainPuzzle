using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField] private TMP_Text pieceText;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;

    public MissionData Data { get ; private set; }

    public void SetUp(MissionData data)
    {
        Data = data;
        SetMission();
    }

    private void SetMission()
    {
        Debug.Log($"Data.PieceData.StrView:{Data.PieceData.StrView}");
        Debug.Log($"Data.PieceData.Material.color:{Data.PieceData.Material.color}");
        Debug.Log($"Data.Count:{Data.Count}");
        pieceText.text = Data.PieceData.StrView;
        image.color = Data.PieceData.Material.color;
        countText.text = Data.Count.ToString();
    }
}
