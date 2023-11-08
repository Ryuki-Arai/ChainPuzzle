using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrecenter : MonoBehaviour
{
    [SerializeField] private PlayerView view;
    private PlayerModel model;

    [SerializeField] List<Piece> chain;

    public void OnInitialized()
    {
        model = new PlayerModel() ;
        view.SetUp();
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            model.GetPieceItem();
            view.SetLine(model.ChainPoint);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            view.DesableChain(model.PieceChain);
            model.RemoveChain();
        }
        chain = model.PieceChain;
    }
}
