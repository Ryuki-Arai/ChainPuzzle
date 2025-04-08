using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/CreateSaveDataObject")]
public class SaveDataObject : ScriptableObject
{
    [field: SerializeField] public PlayerDataObject PlayerData { get; private set; }



}
