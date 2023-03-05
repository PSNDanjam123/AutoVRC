
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Menu.MainMenu
{

    public class LeaveGameButtonListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;
        public override void Interact()
        {
            MainMenuController.LeaveGame(Player, Networking.LocalPlayer);
        }
        public override void OnBootstrap()
        {
            Subscribe(Player);
            Subscribe(Player.GameMaster);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(Player.CanLeaveGame(Networking.LocalPlayer));
            var pos = gameObject.GetComponent<RectTransform>().localPosition;
            pos.y = 0;
            if (Player.CanStartGame(Networking.LocalPlayer))
            {
                pos.y = -15;
            }
            gameObject.GetComponent<RectTransform>().localPosition = pos;
        }
    }

}