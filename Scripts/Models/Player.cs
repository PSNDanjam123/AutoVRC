
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Models
{
    public class Player : Model
    {
        [UdonSynced]
        public int VRCPlayerId;
        [UdonSynced]
        public string DisplayName;
        [UdonSynced]
        public bool VRCPlayerAttached = false;
        [UdonSynced]
        public bool InGame = false;
        [UdonSynced, Range(0, 99)]
        public byte Health = 30;
        [UdonSynced, Range(1, 7)]
        public byte Rank = 1;
        [UdonSynced, Range(0, 99)]
        public byte Coins = 3;

        [Header("Models")]
        public GameMaster GameMaster;

        public void StartGame()
        {
            GameMaster.SetOwner();
            GameMaster.GameInProgress = true;
            GameMaster.Sync();
        }
        public void JoinGame(VRCPlayerApi vRCPlayerApi)
        {
            SetOwner();
            VRCPlayerId = vRCPlayerApi.playerId;
            DisplayName = vRCPlayerApi.displayName;
            VRCPlayerAttached = true;
            InGame = true;
            Sync();

            if (!GameMaster.GameHosted)
            {
                GameMaster.SetOwner();
                GameMaster.VRCPlayerId = vRCPlayerApi.playerId;
                GameMaster.GameHosted = true;
                GameMaster.Sync();
            }
        }


        public void LeaveGame(VRCPlayerApi playerApi)
        {
            SetOwner();
            VRCPlayerId = 0;
            DisplayName = "No Player";
            VRCPlayerAttached = false;
            InGame = false;
            Sync();

            if (GameMaster.PlayersJoinedCount() == 0)
            {
                GameMaster.SetOwner();
                GameMaster.VRCPlayerId = 0;
                GameMaster.GameHosted = false;
                GameMaster.Sync();
            }
            else if (GameMaster.VRCPlayerId == playerApi.playerId && GameMaster.IsOwner())
            {
                GameMaster.VRCPlayerId = Networking.LocalPlayer.playerId;
                GameMaster.Sync();
            }

        }
        public bool CanJoinGame()
        {
            if (VRCPlayerAttached
                || !GameMaster.GameHosted
                || GameMaster.GameInProgress)
            {
                return false;
            }
            return true;
        }
        public bool CanLeaveGame(VRCPlayerApi vRCPlayerApi)
        {
            if (VRCPlayerId != vRCPlayerApi.playerId
                || !GameMaster.GameHosted
                || GameMaster.GameInProgress)
            {
                return false;
            }
            return true;
        }

        public bool CanLeaveOnDisconnect(VRCPlayerApi playerApi)
        {
            if (!IsOwner()
                || VRCPlayerId != playerApi.playerId
                || !GameMaster.GameHosted
                || GameMaster.GameInProgress)
            {
                return false; // Player Model ownership changed, only update if owner
            }
            return true;
        }
        public bool CanHostGame()
        {
            if (GameMaster.GameHosted
                || GameMaster.GameInProgress
                || GameMaster.PlayersJoinedCount() > 0
                || VRCPlayerId != 0)
            {
                return false;
            }
            return true;
        }

        public bool CanStartGame(VRCPlayerApi playerApi)
        {
            if (VRCPlayerId != playerApi.playerId
                || GameMaster.VRCPlayerId != playerApi.playerId
                || GameMaster.PlayersJoinedCount() == 0
                || !GameMaster.GameHosted
                || GameMaster.GameInProgress)
            {
                return false;
            }
            return true;
        }
    }

}