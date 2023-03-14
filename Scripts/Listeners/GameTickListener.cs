
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

        public bool HandlingTick = false;

        public float RequestCooldown = 0.1f;

        private float lastFixedUpdate = 0;

        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
        }

        void FixedUpdate()
        {
            if (!GameMaster.GameInProgress
                || !GameMaster.IsOwner()
                || HandlingTick)
            {
                return; // Only the owner of GameMaster can handle game ticks, game ticks only run while game is in progress
            }
            lastFixedUpdate += Time.deltaTime;
            if (lastFixedUpdate < RequestCooldown)
            {
                return; // Not enough time has passed to handle a game tick
            }
            GameMasterController.HandleTick(GameMaster, this);
            lastFixedUpdate = 0;
        }
    }
}