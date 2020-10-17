using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwap : MonoBehaviour
{
    [SerializeField] private GameObject pageParent;

    [SerializeField] private GameObject shopPage;
    [SerializeField] private GameObject deckPage;
    [SerializeField] private GameObject multiePage;
    [SerializeField] private GameObject stagePage;

    public void StageSwap(EnumInfo.LobbyPageType page)
    {
        pageParent.transform.localPosition=new Vector2((int)page*-Screen.width,0);
    }

    public void SwapShop()
    {
        StageSwap(EnumInfo.LobbyPageType.SHOP);
    }
    public void SwapDeck()
    {
        StageSwap(EnumInfo.LobbyPageType.DECK);
    }
    public void SwapMultie()
    {
        StageSwap(EnumInfo.LobbyPageType.MULTIE);
    }
    public void SwapStage()
    {
        StageSwap(EnumInfo.LobbyPageType.STAGE);
    }
}
