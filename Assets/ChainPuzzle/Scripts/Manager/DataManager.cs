using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [field: SerializeField] public PieceDataObject PieceDataObject { get; private set; }
    [field: SerializeField] public ChainDataObject ChainDataObject { get; private set; }
    [field: SerializeField] public StageDataObject StageDataObject { get; private set; }

    public void OnSetUp()
    {
        StageDataObject.OnSetUp();
    }

    public void SetStageData(StageDataObject stageDataObject)
    {
        this.StageDataObject = stageDataObject;
    }
}
