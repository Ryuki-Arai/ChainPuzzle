using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/CreatePieceDataObject")]
public class PieceDataObject : ScriptableObject
{
    [field: SerializeField] public PieceData[] DataArr { get; private set; }

    public int GetPieceDataIndex(int id)
    {
        for(var i = 0; i < DataArr.Length; i++)
        {
            if(DataArr[i].ID == id)
            {
                return i;
            }
        }

        Debug.LogError($"ID��{id}�̃s�[�X��������܂���ł���");
        return -1;
    }
}
