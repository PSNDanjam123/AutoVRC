
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;

namespace AutoVRC.Controllers
{

    public class ConstructionController : Controller
    {
        public static void RefreshShop(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            byte price = 1;
            if (Player.VRCPlayerId != vRCPlayerApi.displayName || Player.Coins < price)
            {
                return;
            }
            Player.SetOwner();
            Player.Coins -= price;
            Player.WaitingOnGameMaster = true;
            Player.Sync();
        }
        public static void PlayCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            if (Card.Player.VRCPlayerId != vRCPlayerApi.displayName)
            {
                return;
            }
            Card.AddToField();
        }
        public static void BuyCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            byte price = 3;
            if (Card.Player.VRCPlayerId != vRCPlayerApi.displayName || Card.Player.Coins < price)
            {
                return;
            }
            Card.Player.SetOwner();
            Card.Player.Coins -= price;
            Card.AddToHand();
            Card.Player.Sync();
        }
        public static void SellCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            byte value = 1;
            if (Card.Player.VRCPlayerId != vRCPlayerApi.displayName)
            {
                return;
            }
            Card.Player.SetOwner();
            Card.Player.Coins += value;
            Card.AddToShop();
            Card.Player.Sync();
        }
        public static void MoveLeft(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            if (Card.Player.VRCPlayerId != vRCPlayerApi.displayName)
            {
                return;
            }
            Card.MoveLeft();
        }
    }

}