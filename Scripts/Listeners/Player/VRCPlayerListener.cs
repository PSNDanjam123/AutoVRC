
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners.Player
{
    public class VRCPlayerListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;
        public Models.Player Player;
        public override void OnPlayerLeft(VRCPlayerApi playerApi)
        {
            MainMenuController.LeaveGameOnDisconnect(Player, playerApi);
        }
    }
}