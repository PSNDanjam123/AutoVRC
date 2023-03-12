
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Templates;
using AutoVRC.Tools;

namespace AutoVRC.Models
{
    public class Shop : Model
    {
        [UdonSynced]
        public byte[] CardStock;

        public CardTemplateManager CardTemplateManager;

        public byte CardMultiplier = 12;

        public void Restock()
        {
            var templates = CardTemplateManager.CardTemplates;
            var totalTemplates = templates.Length;
            var totalCards = CardMultiplier * totalTemplates;

            var data = new byte[totalCards];

            var i = 0;
            foreach (var template in templates)
            {
                for (var j = 0; j < CardMultiplier; j++)
                {
                    data[j] = template.CardTemplateId;
                }
                i++;
            }
            CardStock = data;
        }
    }

}