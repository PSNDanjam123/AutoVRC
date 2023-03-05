
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Menu.MainMenu
{
    public class JoinGameButtonListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;
        public Models.Player Player;

        public override void Interact()
        {
            MainMenuController.JoinGame(Player, Networking.LocalPlayer);
        }

        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
            Subscribe(Player);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Player.CanJoinGame());
        }
    }
}
