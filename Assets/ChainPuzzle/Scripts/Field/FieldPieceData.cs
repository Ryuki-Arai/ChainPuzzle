using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FieldPieceData
{
    [field: SerializeField] public int PieceID { get; private set; }
    [field: SerializeField] public PieceDigitType PieceType { get; private set; }
    [field: SerializeField] public int PieceCount { get; private set; }

    public FieldPieceData(int ID, PieceDigitType Type, int Count)
    {
        PieceID = ID;
        PieceType = Type;
        PieceCount = Count;
    }
}
