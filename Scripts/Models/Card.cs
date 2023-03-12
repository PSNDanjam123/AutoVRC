
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Templates;

namespace AutoVRC.Models
{

    public class Card : Model
    {
        public byte CardId;   // unique, used for group assigning

        [UdonSynced]
        public byte CardTemplateId;   // used to find the card template with default stats and info 

        [UdonSynced]
        public byte Damage = 1;
        [UdonSynced]
        public byte Health = 1;

        [Header("Relationships")]
        public Player Player;
        public CardGroup Hand;
        public CardGroup Field;
        public CardGroup Shop;

        public void LoadTemplate(CardTemplate template)
        {
            CardTemplateId = template.CardTemplateId;
            Damage = template.Damage;
            Health = template.Health;
        }

        public int GetCardGroupPosition()
        {
            var group = GetCardGroup();
            if (!group)
            {
                return -1;
            }
            return group.GetPosition(CardId);
        }

        public CardGroup GetCardGroup()
        {
            if (InHand())
            {
                return Hand;
            }
            else if (InField())
            {
                return Field;
            }
            else if (InShop())
            {
                return Shop;
            }
            return null;
        }

        public bool InCardGroup()
        {
            return InHand() || InField() || InShop();
        }

        public void AddToHand()
        {
            if (Hand.GetCount() > 9)
            {
                return;
            }
            SetOwner();
            var prev = GetCardGroup();
            if (prev != null)
            {
                prev.SetOwner();
                prev.Remove(CardId);
                prev.Sync();
            }
            Hand.Add(CardId);
            Hand.Sync();
        }
        public void AddToField()
        {
            if (Field.GetCount() > 9)
            {
                return;
            }
            SetOwner();
            var prev = GetCardGroup();
            if (prev != null)
            {
                prev.SetOwner();
                prev.Remove(CardId);
                prev.Sync();
            }
            Field.Add(CardId);
            Field.Sync();
        }
        public void AddToShop()
        {
            if (Shop.GetCount() > 9)
            {
                return;
            }
            Shop.SetOwner();
            var prev = GetCardGroup();
            if (prev != null)
            {
                prev.SetOwner();
                prev.Remove(CardId);
                prev.Sync();
            }
            Shop.Add(CardId);
            Shop.Sync();
        }

        public void RemoveFromShop()
        {
            Shop.SetOwner();
            Shop.Remove(CardId);
            Shop.Sync();
        }

        public void MoveLeft()
        {
            var group = GetCardGroup();
            group.SetOwner();
            group.MoveLeft(CardId);
            group.Sync();
        }
        public void MoveRight()
        {
            var group = GetCardGroup();
            group.SetOwner();
            group.MoveRight(CardId);
            group.Sync();
        }

        public bool InHand()
        {
            return Hand.HasCard(CardId);
        }
        public bool InField()
        {
            return Field.HasCard(CardId);
        }
        public bool InShop()
        {
            return Shop.HasCard(CardId);
        }

    }

}