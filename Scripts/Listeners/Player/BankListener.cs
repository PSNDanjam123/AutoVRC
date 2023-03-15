
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;


namespace AutoVRC.Listeners.Player
{

    public class BankListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;

        public byte Coins = 0;

        public override void OnBootstrap()
        {
            Subscribe(Player);
        }

        public override void OnModelSync()
        {
            if (Coins != Player.Coins)
            {
                Coins = Player.Coins;
                gameObject.GetComponent<Text>().text = Coins.ToString();
            }
        }
    }
}