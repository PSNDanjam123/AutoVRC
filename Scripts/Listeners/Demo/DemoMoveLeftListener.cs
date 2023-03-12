
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners
{

    public class DemoMoveLeftListener : Listener
    {
        [Header("Models")]
        public Card Card;
        public override void Interact()
        {
            ConstructionController.MoveLeft(Card, Networking.LocalPlayer);
        }
    }

}