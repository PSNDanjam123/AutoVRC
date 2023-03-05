
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;

namespace AutoVRC.Controllers
{

    public class MainMenuController : Controller
    {
        public static void HostGame(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            if (!Player.CanHostGame())
            {
                return;
            }
            Player.JoinGame(vRCPlayerApi);
        }

        public static void JoinGame(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            if (!Player.CanJoinGame())
            {
                return;
            }
            Player.JoinGame(vRCPlayerApi);
        }

        public static void StartGame(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            if (!Player.CanStartGame(vRCPlayerApi))
            {
                return;
            }
            Player.StartGame();
        }

        public static void LeaveGame(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            if (!Player.CanLeaveGame(vRCPlayerApi))
            {
                return;
            }
            Player.LeaveGame(vRCPlayerApi);
        }

        public static void LeaveGameOnDisconnect(Player Player, VRCPlayerApi vRCPlayerApi)
        {
            if (!Player.CanLeaveOnDisconnect(vRCPlayerApi))
            {
                return;
            }
            Player.LeaveGame(vRCPlayerApi);
        }
    }

}