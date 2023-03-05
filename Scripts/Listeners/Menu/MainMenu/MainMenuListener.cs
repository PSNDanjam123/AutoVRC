
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Menu.MainMenu
{
    public class MainMenuListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;
        public override void OnBootstrap()
        {
            Subscribe(Player);
            Subscribe(Player.GameMaster);
        }
        public override void OnModelSync()
        {
            gameObject.SetActive(!Player.GameMaster.GameInProgress || !Player.InGame);
        }


    }
}