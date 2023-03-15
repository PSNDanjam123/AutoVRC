
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;

namespace AutoVRC.Listeners.Player
{
    public class HealthListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;

        public byte Health;

        public override void OnBootstrap()
        {
            Subscribe(Player);
        }

        public override void OnModelSync()
        {
            if (Health != Player.Health)
            {
                Health = Player.Health;
                gameObject.GetComponent<Text>().text = Health.ToString();
            }
        }
    }
}
