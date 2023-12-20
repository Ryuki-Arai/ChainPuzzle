using System;
using UnityEngine;

namespace InGame
{
    [Serializable]
    public class PieceData
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public PieceDigitType DigitType { get; private set; }
        [field: SerializeField] public string StrView { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }

        public PieceData()
        {

        }

        private void SetStrView()
        {
            switch (DigitType)
            {
                case PieceDigitType.None:
                    break;
                case PieceDigitType.Million:
                    break;
                case PieceDigitType.Bilion:
                    break;
                case PieceDigitType.Trillion:
                    break;
                case PieceDigitType.Quadrillion:
                    break;
                case PieceDigitType.Quintillion:
                    break;
                case PieceDigitType.Sextillion:
                    break;
                case PieceDigitType.Septillion:
                    break;
                case PieceDigitType.Octillion:
                    break;
                case PieceDigitType.Nonillion:
                    break;
                case PieceDigitType.Decillion:
                    break;
                case PieceDigitType.Undecillion:
                    break;
            }
        }
    }
}