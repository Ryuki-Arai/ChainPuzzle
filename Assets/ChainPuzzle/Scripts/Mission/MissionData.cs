using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MissionData
{
    public PieceData PieceData { get; private set; }

    public int Count { get; private set; }

    public MissionData(PieceData data, int count)
    {
        PieceData = data;
        Count = count;
    }

    public void MinusCount(int count)
    {
        Count -= count;
    }

    public void PulsCount(int count)
    {
        Count += count;
    }
}
