
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
        [Header("Relationships")]
        public GameMaster GameMaster;

        [UdonSynced]
        public byte[] CardStock;

        public CardTemplateManager CardTemplateManager;

        public byte CardMultiplier = 12;

        public CardTemplate PluckRandom()
        {
            if (CardStock.Length == 0)
            {
                return null;
            }
            var x = Random.Range(0, CardStock.Length - 1);
            var data = new byte[CardStock.Length - 1];
            byte plucked = 0;
            for (var i = 0; i < CardStock.Length - 1; i++)
            {
                if (i == x)
                {
                    plucked = CardStock[i];
                    continue;
                }
                data[i] = CardStock[i];
            }
            CardStock = data;
            return CardTemplateManager.GetTemplate(plucked);
        }

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