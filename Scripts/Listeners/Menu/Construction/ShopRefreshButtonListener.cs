
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Menu.Construction
{
    public class ShopRefreshButtonListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;

        public override void OnBootstrap()
        {
            Subscribe(Player);
            Subscribe(Player.GameMaster);
        }

        public override void Interact()
        {
            ConstructionController.RefreshShop(Player, Networking.LocalPlayer);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Player.InGame && Player.GameMaster.GameInProgress && !Player.WaitingOnShopRefresh);
        }

    }

}