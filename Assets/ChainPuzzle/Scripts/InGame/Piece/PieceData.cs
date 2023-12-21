using System;
using UnityEngine;

namespace InGame
{
    [Serializable]
    public class PieceData
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string StrView { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }

        public PieceData()
        {

        }
    }
}