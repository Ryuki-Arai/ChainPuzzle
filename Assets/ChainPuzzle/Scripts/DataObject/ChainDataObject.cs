using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChainData", menuName = "ScriptableObjects/CreateChainDataObject")]
public class ChainDataObject : ScriptableObject
{
    [field: SerializeField] public float ChainDistance { get; private set; }
    [field: SerializeField] public int MinChainLength { get; private set; }
    [field: SerializeField] public int SkipChainLength { get; private set; }
}
