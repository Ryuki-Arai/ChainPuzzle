using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/CreatePieceDataObject")]
public class PieceDataObject : ScriptableObject
{
    [SerializeField] private PieceData[] dataArr;
    public PieceData[] DataArr => dataArr;

    public int GetPieceDataIndex(int id, PieceDigitType type)
    {
        for(var i = 0; i < dataArr.Length; i++)
        {
            if(dataArr[i].ID == id && dataArr[i].DigitType == type)
            {
                return i;
            }
        }

        Debug.LogError($"ID��{id}��Type��{type.ToString()}�̃s�[�X��������܂���ł���");
        return -1;
    }
}
