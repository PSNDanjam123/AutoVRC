
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
        public string VRCPlayerId = null;
        [UdonSynced, Tooltip("Has a game been hosted?")]
        public bool GameHosted = false;
        [UdonSynced, Tooltip("Is the game in progress")]
        public bool GameInProgress = false;
        [UdonSynced]
        public int GameSeed = 0;

        [UdonSynced]
        public double StartEpoch = 0; // Milliseconds
        public float TotalGameTime = 0; // How long has the game been running for
        public int Round = 0;  // What round are we on

        public int RoundSeconds = 0; // How many seconds are in this round

        public int RoundLength = 30; // The default round length
        public int RoundIncrementTime = 5;   // seconds added to each round

        [Header("Models")]
        public Player[] Players;
        public Shop Shop;

        private float _fixedUpdateTimeSinceLastTick = 0;
        void FixedUpdate()
        {
            _fixedUpdateTimeSinceLastTick += Time.deltaTime;
            if (_fixedUpdateTimeSinceLastTick < 0.1)
            {
                return;
            }
            _fixedUpdateTimeSinceLastTick = 0;
            if (GameInProgress == false)
            {
                return;
            }
            updateTotalGameTime();
            updateRound();
        }

        private void updateTotalGameTime()
        {
            double d = GetDiffSinceGameStart();
            TotalGameTime = (float)d / 1000;
        }

        private void updateRound()
        {
            var seconds = GetDiffSinceGameStart() / 1000;
            var round = 1;
            var total = RoundLength;
            while (seconds > total)
            {
                total += RoundLength + (RoundIncrementTime * round);
                round++;
            }
            RoundSeconds = (int)(total - seconds);
            Round = round;
        }

        public double GetDiffSinceGameStart()
        {
            return GetMillisecondEpoch() - StartEpoch;
        }

        public double GetMillisecondEpoch()
        {
            System.TimeSpan t = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return t.TotalMilliseconds;
        }

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