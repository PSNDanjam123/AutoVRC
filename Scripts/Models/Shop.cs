
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
            var data = new byte[CardStock.Length - 1];
            var x = Random.Range(0, CardStock.Length);
            byte plucked = 0;
            bool hasPlucked = false;
            for (var i = 0; i < CardStock.Length; i++)
            {
                if (i == x && !hasPlucked)
                {
                    plucked = CardStock[i];
                    hasPlucked = true;
                    continue;
                }
                var offset = hasPlucked ? 1 : 0;
                data[i - offset] = CardStock[i];
            }
            CardStock = data;
            Log("PluckRandom", "plucked card template id: " + plucked);
            return CardTemplateManager.GetTemplate(plucked);
        }

        public void Restock()
        {
            var templates = CardTemplateManager.CardTemplates;
            var totalTemplates = templates.Length - 1;  // Remove hidden
            var totalCards = CardMultiplier * totalTemplates;

            var data = new byte[totalCards];

            var i = 0;
            for (var j = 0; j < CardMultiplier; j++)
            {
                foreach (var template in templates)
                {
                    if (template.CardTemplateId == 0)
                    {
                        continue;
                    }
                    data[i] = template.CardTemplateId;
                    i++;
                }
            }
            CardStock = data;
        }
    }

}