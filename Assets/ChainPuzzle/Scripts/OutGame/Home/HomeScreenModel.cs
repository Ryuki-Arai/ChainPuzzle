using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class HomeScreenModel
    {
        public int Level => DataSaveUtility.I.Data.PlayerData.StageLevel;
    }
}
