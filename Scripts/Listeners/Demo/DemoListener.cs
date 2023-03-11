
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners
{

    public class DemoListener : Listener
    {
        [Header("Models")]
        public Card Card;
        private int test = 0;
        public override void Interact()
        {
            if (Card.InHand())
            {
                ConstructionController.PlayCard(Card);
            }
            else if (Card.InField())
            {
                ConstructionController.SellCard(Card);
            }
            else if (Card.InShop())
            {
                ConstructionController.BuyCard(Card);
            }
        }
    }

}