
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Menu.MainMenu
{
    public class PlayersCountListener : Listener
    {
        [Header("Models")]
        public Models.GameMaster GameMaster;
        public override void OnBootstrap()
        {
            Subscribe(GameMaster);
            foreach (var Player in GameMaster.Players)
            {
                Subscribe(Player);
            }
        }

        public override void OnModelSync()
        {
            if (!GameMaster.GameHosted || GameMaster.GameInProgress)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            var total = GameMaster.Players.Length;
            var count = GameMaster.PlayersJoinedCount();
            gameObject.GetComponent<Text>().text = "Players Joined: " + count + " / " + total;
        }
    }
}