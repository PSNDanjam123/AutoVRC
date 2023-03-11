
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
        public static void PlayCard(Card Card)
        {
            Card.AddToField();
        }
        public static void BuyCard(Card Card)
        {
            Card.AddToHand();
        }
        public static void SellCard(Card Card)
        {
            Card.AddToShop();
        }
    }

}