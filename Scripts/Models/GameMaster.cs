
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Models
{

    public class GameMaster : Model
    {
        [UdonSynced, Tooltip("Game Host")]
        public int VRCPlayerId;
        [UdonSynced, Tooltip("Has a game been hosted?")]
        public bool GameHosted = false;
        [UdonSynced, Tooltip("Is the game in progress")]
        public bool GameInProgress = false;

        [Header("Models")]
        public Player[] Players;

        public uint PlayersJoinedCount()
        {
            uint count = 0;
            foreach (var Player in Players)
            {
                if (Player.InGame)
                {
                    count++;
                }
            }
            return count;
        }

    }

}