
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Player
{

    public class DisplayNameListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;
        public string DisplayName;

        public override void OnBootstrap()
        {
            Subscribe(Player);
        }

        public override void OnModelSync()
        {
            var setName = Player.VRCPlayerId + " " + Player.DisplayName;
            if (DisplayName != setName)
            {
                gameObject.GetComponent<Text>().text = DisplayName = setName;
            }
        }
    }

}