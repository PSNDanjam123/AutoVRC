
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
    }
}