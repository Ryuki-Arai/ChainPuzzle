using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/CreatePieceDataObject")]
public class PieceDataObject : ScriptableObject
{
    [field: SerializeField] public PieceData[] DataArr { get; private set; }

    public int GetPieceDataIndex(int id, PieceDigitType type)
    {
        for(var i = 0; i < DataArr.Length; i++)
        {
            if(DataArr[i].ID == id && DataArr[i].DigitType == type)
            {
                return i;
            }
        }

        Debug.LogError($"ID‚ª{id}‚ÅType‚ª{type}‚Ìƒs[ƒX‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½");
        return -1;
    }
}
