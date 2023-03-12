
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;
using AutoVRC.Listeners;

namespace AutoVRC.Controllers
{

    public class GameMasterController : Controller
    {
        public static void ToggleGameTickListener(GameMaster GameMaster, GameTickListener gameTickListener)
        {
            gameTickListener.Owner = (GameMaster.VRCPlayerId == Networking.LocalPlayer.playerId);
        }

        public static void HandleTick(GameMaster GameMaster, GameTickListener gameTickListener)
        {
            gameTickListener.HandlingTick = true;
            gameTickListener.HandlingTick = false;
        }
    }

}