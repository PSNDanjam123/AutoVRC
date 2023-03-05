
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Menu.MainMenu
{
    public class StartGameButtonListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;

        public override void Interact()
        {
            MainMenuController.StartGame(Player, Networking.LocalPlayer);
        }

        public override void OnBootstrap()
        {
            Subscribe(Player);
            Subscribe(Player.GameMaster);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Player.CanStartGame(Networking.LocalPlayer));
        }
    }
}