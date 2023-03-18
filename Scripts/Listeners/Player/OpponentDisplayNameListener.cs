
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Player
{

    public class OpponentDisplayNameListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;
        public string DisplayName;

        public override void OnBootstrap()
        {
            Subscribe(Player);
            foreach (var player in Player.GameMaster.Players)
            {
                Subscribe(player);
            }
        }

        public override void OnModelSync()
        {
            var setName = "No Player";

            Models.Player opponent = null;
            if (Player.NextOpponentId != -1)
            {
                foreach (var player in Player.GameMaster.Players)
                {
                    if (player.PlayerId == Player.NextOpponentId)
                    {
                        opponent = player;
                    }
                }
            }
            if (opponent != null)
            {
                setName = "VS " + "(" + opponent.PlayerId + ") " + opponent.VRCPlayerId;
            }
            if (DisplayName != setName)
            {
                gameObject.GetComponent<Text>().text = DisplayName = setName;
            }
        }
    }

}