using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageDataObject")]
public class StageDataObject : ScriptableObject
{
    [field: SerializeField] public int StageLevel { get; private set; }
    [field: SerializeField] public int StartID { get; private set; }
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public FieldPieceData ClearPieceData { get; private set; }

    public void OnSetUp()
    {
        Debug.Log("LoadingStageData");
    }
}
