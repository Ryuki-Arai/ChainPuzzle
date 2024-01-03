using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class MissionPiece : MonoBehaviour
    {
        [SerializeField] private TMP_Text pieceText;
        [SerializeField] private Image image;

        const float Move_Duration = 1f;

        public void SetUp(PieceData data)
        {
            pieceText.text = data.StrView;
            image.color = data.Material.color;
        }

        public void PlayMoving(Vector2 pos)
        {
            DOTween.Sequence()
                .Append(transform.DOMove(pos, Move_Duration))
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}
