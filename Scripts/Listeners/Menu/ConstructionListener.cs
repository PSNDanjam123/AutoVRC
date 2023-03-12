
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Menu
{
    public class ConstructionListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;

        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
        }

        public override void OnModelSync()
        {
            gameObject.SetActive(GameMaster.GameInProgress);
        }

    }

}