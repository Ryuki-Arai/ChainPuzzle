using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField] PlayerPrecenter player;
    [SerializeField] PieceControlFactory factory;

    public void SetUp()
    {
        player.OnInitialized();
        factory.OnInitialized();
    }

    public void OnUpdate()
    {
        player.OnUpdate();
        factory.OnUpdate();
    }
}
