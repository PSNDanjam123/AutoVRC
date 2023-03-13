
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Card
{
    public class PlayListener : Listener
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
            ConstructionController.PlayCard(Card, Networking.LocalPlayer);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(!Card.Player.WaitingOnGameMaster && Card.InHand() && Card.Field.GetCount() < Card.Field.MaxSize);
        }

    }

}