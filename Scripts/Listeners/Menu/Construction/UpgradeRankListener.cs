
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;
using AutoVRC.Controllers;


namespace AutoVRC.Listeners.Menu.Construction
{
    public class UpgradeRankListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;

        public Image Background;
        public Text Text;
        public Text Cost;

        public override void OnBootstrap()
        {
            Subscribe(Player);
            Subscribe(Player.GameMaster);
        }

        public override void Interact()
        {
            ConstructionController.UpgradeRank(Player, Networking.LocalPlayer);
        }

        public override void OnModelSync()
        {
            Background.color = getRankColor(Player.Rank);
            Text.text = getRankText(Player.Rank);
            Cost.text = Player.RankCost.ToString();
            gameObject.SetActive(Player.InGame && Player.GameMaster.GameInProgress && !Player.WaitingOnGameMaster);
        }

        private string getRankText(byte rank)
        {
            switch (rank)
            {
                case 1: // Visitor
                    return "Visitor";
                case 2: // New
                    return "New";
                case 3: // User
                    return "User";
                case 4: // Known
                    return "Known";
                case 5: // Trusted
                    return "Trusted";
            }
            return "Unknown";
        }


        private Color getRankColor(byte rank)
        {
            switch (rank)
            {
                case 1: // Visitor
                    return new Color32(192, 192, 192, 200);
                case 2: // New
                    return new Color32(25, 113, 192, 200);
                case 3: // User
                    return new Color32(42, 207, 92, 200);
                case 4: // Known
                    return new Color32(255, 127, 68, 200);
                case 5: // Trusted
                    return new Color32(132, 68, 236, 200);
            }
            return Color.black;
        }
    }
}