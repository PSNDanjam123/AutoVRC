
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Menu.MainMenu
{

    public class HostGameButtonListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;
        public Models.Player Player;

        public override void Interact()
        {
            MainMenuController.HostGame(Player, Networking.LocalPlayer);
        }

        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Player.CanHostGame());
        }
    }
}