
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
            switch (Rank)
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

            return Color.red;
        }
    }
}