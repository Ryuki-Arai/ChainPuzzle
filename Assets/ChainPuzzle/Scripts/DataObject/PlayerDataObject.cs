using System;
using UnityEngine;

[Serializable]
public class PlayerDataObject
{
    [field: SerializeField] public int StageLevel { get; private set; }
    [field: SerializeField] public int Coin { get; private set; }
    [field: SerializeField] public int Life { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public void LevelUp()
    {
        StageLevel++;
    }
}
