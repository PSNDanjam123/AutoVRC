﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Models
{
    public class Player : Model
    {
        [UdonSynced, HideInInspector]
        public string VRCPlayerId = null;
        [UdonSynced]
        public bool VRCPlayerAttached = false;
        [UdonSynced]
        public bool InGame = false;
        [UdonSynced, Range(0, 99)]
        public byte Health = 0;
        [UdonSynced, Range(1, 7)]
        public byte Rank = 1;
        [UdonSynced]
        public byte RankCost = 4;
        [UdonSynced]
        public int Round = 0;
        [UdonSynced]
        public bool InBattle = false;
        [UdonSynced, Range(0, 99)]
        public byte Coins = 0;
        [UdonSynced]
        public bool WaitingOnGameMaster = false;

        [Header("Models")]
        public GameMaster GameMaster;
        public CardGroup[] CardGroups;
        public Card[] Cards;


        private float _fixedUpdateTimeSinceLastTick = 0;

        void FixedUpdate()
        {
            _fixedUpdateTimeSinceLastTick += Time.deltaTime;
            if (Round == 0 && _fixedUpdateTimeSinceLastTick < 2)
            {
                return; // join buffer
            }
            if (_fixedUpdateTimeSinceLastTick < 0.5)
            {
                return;
            }
            _fixedUpdateTimeSinceLastTick = 0;

            // handle new round
            _newRoundHandler();
        }

        private void _newRoundHandler()
        {
            if (Round == GameMaster.Round || !IsOwner())
            {
                return;
            }
            Round = GameMaster.Round;
            InBattle = true;
            Coins = 3 + 1;
            if (Round > 1)
            {
                Coins += (byte)Round;
                Coins--;
            }
            if (Coins > 10)
            {
                Coins = 10;
            }
            if (RankCost > 0)
            {
                RankCost--;
            }
            Sync();
        }

        public void StartGame()
        {
            GameMaster.Shop.SetOwner();
            GameMaster.Shop.Restock();
            GameMaster.Shop.Sync();

            foreach (var Player in GameMaster.Players)
            {
                Player.SetOwner();
                Player.ResetStats();
                Player.Sync();
            }

            GameMaster.SetOwner();
            GameMaster.GameInProgress = true;
            GameMaster.StartEpoch = GameMaster.GetMillisecondEpoch();
            GameMaster.Sync();
        }


        public void ResetStats()
        {
            Health = 30;
            Rank = 1;
            Coins = 3 + 1;
        }

        public void JoinGame(VRCPlayerApi vRCPlayerApi)
        {
            SetOwner();
            VRCPlayerId = vRCPlayerApi.displayName;
            VRCPlayerAttached = true;
            InGame = true;
            Sync();

            if (!GameMaster.GameHosted)
            {
                GameMaster.SetOwner();
                GameMaster.VRCPlayerId = vRCPlayerApi.displayName;
                GameMaster.GameHosted = true;
                GameMaster.Sync();
            }
        }


        public void LeaveGame(VRCPlayerApi playerApi)
        {
            SetOwner();
            VRCPlayerId = null;
            VRCPlayerAttached = false;
            InGame = false;
            Sync();

            if (GameMaster.PlayersJoinedCount() == 0)
            {
                GameMaster.SetOwner();
                GameMaster.VRCPlayerId = null;
                GameMaster.GameHosted = false;
                GameMaster.Sync();
            }
            else if (GameMaster.VRCPlayerId == playerApi.displayName && GameMaster.IsOwner())
            {
                GameMaster.VRCPlayerId = Networking.LocalPlayer.displayName;
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
            if (VRCPlayerId != vRCPlayerApi.displayName
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
                || VRCPlayerId != playerApi.displayName
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
                || GameMaster.PlayersJoinedCount() > 0)
            {
                return false;
            }
            return true;
        }

        public bool CanStartGame(VRCPlayerApi playerApi)
        {
            if (VRCPlayerId != playerApi.displayName
                || GameMaster.VRCPlayerId != playerApi.displayName
                || GameMaster.PlayersJoinedCount() == 0
                || !GameMaster.GameHosted
                || GameMaster.GameInProgress)
            {
                return false;
            }
            return true;
        }
        public Card GetCardWithoutGroup()
        {
            foreach (var card in Cards)
            {
                if (!card.InCardGroup())
                {
                    return card;
                }
            }
            return null;
        }
    }

}