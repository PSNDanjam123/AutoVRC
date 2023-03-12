
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Card
{
    public class BuyListener : Listener
    {
        [Header("Models")]
        public Models.Card Card;

        public override void OnBootstrap()
        {
            Subscribe(Card);
            Subscribe(Card.Player);
            foreach (var group in Card.Player.CardGroups)
            {
                Subscribe(group);
            }
        }

        public override void Interact()
        {
            ConstructionController.BuyCard(Card, Networking.LocalPlayer);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(!Card.Player.WaitingOnShopRefresh && Card.InShop() && Card.Hand.GetCount() < 10);
        }

    }

}