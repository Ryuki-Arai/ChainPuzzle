using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/CreatePlayerDataObject")]
public class PlayerDataObject : ScriptableObject
{
    [field: SerializeField] public int StageLevel { get; private set; }
    [field: SerializeField] public int Coin { get; private set; }
    [field: SerializeField] public int Life { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}
