
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AutoVRC.Templates
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CardTemplate : UdonSharpBehaviour
    {
        public byte CardTemplateId = 0;

        public string Title = "No Name";

        public Texture Art;

        [Header("Stats")]
        [Range(1, 6)]
        public byte Rank = 1;
        [Range(1, 255)]
        public byte Damage = 1;
        [Range(1, 255)]
        public byte Health = 1;

        public Color GetRankColor()
        {
            switch ((uint)Rank)
            {
                case 1: // Visitor
                    return new Color(192 / 255, 192 / 255, 192 / 255, 0.6f);
                case 2: // New
                    return new Color(25 / 255, 113 / 255, 192 / 238, 0.6f);
                case 3: // User
                    return new Color(42 / 255, 207 / 255, 92 / 238, 0.6f);
                case 4: // Known
                    return new Color(255 / 255, 127 / 255, 68 / 238, 0.6f);
                case 5: // Trusted
                    return new Color(132 / 255, 68 / 255, 236 / 238, 0.6f);
            }

            return Color.red;
        }
    }
}