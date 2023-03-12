
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Controllers;

namespace AutoVRC.Listeners
{
    public class GameTickListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;

        public bool Owner = false;

        public bool HandlingTick = false;

        public float RequestCooldown = 2.0f;

        private float lastFixedUpdate = 0;

        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
        }

        public override void OnModelSync()
        {
            GameMasterController.ToggleGameTickListener(GameMaster, this);
        }

        void FixedUpdate()
        {
            if (!Owner)
            {
                return; // Only the owner (GameMaster) can handle game ticks
            }
            lastFixedUpdate += Time.deltaTime;
            if (lastFixedUpdate < RequestCooldown)
            {
                return; // Not enough time has passed to handle a game tick
            }
            Log("FixedUpdate", "Handling Tick!");
            GameMasterController.HandleTick(GameMaster, this);
            lastFixedUpdate = 0;
        }
    }
}