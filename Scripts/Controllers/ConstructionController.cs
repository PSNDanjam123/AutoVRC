
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
        public static void PlayCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            if (Card.Player.VRCPlayerId != vRCPlayerApi.playerId)
            {
                return;
            }
            Card.AddToField();
        }
        public static void BuyCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            if (Card.Player.VRCPlayerId != vRCPlayerApi.playerId)
            {
                return;
            }
            Card.AddToHand();
        }
        public static void SellCard(Card Card, VRCPlayerApi vRCPlayerApi)
        {
            if (Card.Player.VRCPlayerId != vRCPlayerApi.playerId)
            {
                return;
            }
            Card.AddToShop();
        }
    }

}