
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AutoVRC.Tools
{

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CardTemplateManager : UdonSharpBehaviour
    {
        public CardTemplate[] CardTemplates;

        public CardTemplate GetTemplate(byte cardTemplateId)
        {
            foreach (var cardTemplate in CardTemplates)
            {
                if (cardTemplate.CardTemplateId == cardTemplateId)
                {
                    return cardTemplate;
                }
            }
            return null;
        }
    }
}