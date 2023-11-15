using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChainData", menuName = "ScriptableObjects/CreateChainDataObject")]
public class ChainDataObject : ScriptableObject
{
    [field: SerializeField] public int MinLength { get; private set; }
    [field: SerializeField] public float ChainDistance { get; private set; }
}
