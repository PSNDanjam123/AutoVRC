﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Card
{
    public class MoveLeftListener : Listener
    {
        [Header("Models")]
        public Models.Card Card;

        public override void OnBootstrap()
        {
            Subscribe(Card);
            foreach (var group in Card.Player.CardGroups)
            {
                Subscribe(group);
            }
        }

        public override void Interact()
        {
            ConstructionController.MoveLeft(Card, Networking.LocalPlayer);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Card.Player.Health > 0 && !Card.Player.WaitingOnGameMaster && !Card.InShop() && Card.GetCardGroupPosition() > 0);
        }

    }

}