
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;

namespace AutoVRC.Models
{

    public class Card : Model
    {
        public byte CardId;   // unique, used for group assigning

        [UdonSynced]
        public byte Damage = 1;
        [UdonSynced]
        public byte Health = 1;

        [Header("Relationships")]
        public Player Player;
        public CardGroup Hand;
        public CardGroup Field;
        public CardGroup Shop;

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
            SetOwner();
            var prev = GetCardGroup();
            prev.SetOwner();
            if (prev != null)
            {
                prev.Remove(CardId);
                prev.Sync();
            }
            Hand.Add(CardId);
            prev.Sync();
            Hand.Sync();
        }
        public void AddToField()
        {
            SetOwner();
            var prev = GetCardGroup();
            prev.SetOwner();
            if (prev != null)
            {
                prev.Remove(CardId);
            }
            Field.Add(CardId);
            prev.Sync();
            Field.Sync();
        }
        public void AddToShop()
        {
            SetOwner();
            var prev = GetCardGroup();
            prev.SetOwner();
            if (prev != null)
            {
                prev.Remove(CardId);
            }
            Shop.Add(CardId);
            prev.Sync();
            Shop.Sync();
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