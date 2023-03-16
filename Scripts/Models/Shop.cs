
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
        public byte[] CardStockRank1;
        [UdonSynced]
        public byte[] CardStockRank2;
        [UdonSynced]
        public byte[] CardStockRank3;
        [UdonSynced]
        public byte[] CardStockRank4;
        [UdonSynced]
        public byte[] CardStockRank5;

        public CardTemplate[] CardListRank1;
        public CardTemplate[] CardListRank2;
        public CardTemplate[] CardListRank3;
        public CardTemplate[] CardListRank4;
        public CardTemplate[] CardListRank5;

        public CardTemplateManager CardTemplateManager;

        public void AddCard(byte cardTemplateId)
        {
            var template = CardTemplateManager.GetTemplate(cardTemplateId);
            var stock = getCardStock(template.Rank);
            var len = stock.Length + 1;
            var data = new byte[len];
            for (var i = 0; i < len - 1; i++)
            {
                data[i] = stock[i];
            }
            data[stock.Length] = cardTemplateId;
            setCardStock(template.Rank, data);
        }


        public CardTemplate PluckRandom(byte rank)
        {
            if (rank == 1)
            {
                rank = (byte)Random.Range(1, 3);
            }
            else if (rank == 5)
            {
                rank = (byte)Random.Range(4, 6);
            }
            else
            {
                rank = (byte)Random.Range(rank - 1, rank + 2);
            }

            var stock = getCardStock(rank);
            if (stock.Length == 0)
            {
                return null;
            }
            var data = new byte[stock.Length - 1];
            var x = Random.Range(0, stock.Length);
            byte plucked = 0;
            bool hasPlucked = false;
            for (var i = 0; i < stock.Length; i++)
            {
                if (i == x && !hasPlucked)
                {
                    plucked = stock[i];
                    hasPlucked = true;
                    continue;
                }
                var offset = hasPlucked ? 1 : 0;
                data[i - offset] = stock[i];
            }
            setCardStock(rank, data);
            return CardTemplateManager.GetTemplate(plucked);
        }

        public void Restock()
        {
            CardStockRank1 = restock(CardListRank1, 1);
            CardStockRank2 = restock(CardListRank2, 2);
            CardStockRank3 = restock(CardListRank3, 3);
            CardStockRank4 = restock(CardListRank4, 4);
            CardStockRank5 = restock(CardListRank5, 5);
        }

        private byte[] restock(CardTemplate[] templates, byte rank)
        {
            if (templates.Length == 0)
            {
                return new byte[0];
            }
            var multipler = 24 - (3 * rank);
            var total = templates.Length * multipler;
            byte[] data = new byte[total];
            for (var i = 0; i < total; i++)
            {
                data[i] = templates[i % templates.Length].CardTemplateId;
            }
            return data;
        }

        private byte[] getCardStock(byte rank)
        {
            if (rank == 1)
            {
                return CardStockRank1;
            }
            if (rank == 2)
            {
                return CardStockRank2;
            }
            if (rank == 3)
            {
                return CardStockRank3;
            }
            if (rank == 4)
            {
                return CardStockRank4;
            }
            if (rank == 5)
            {
                return CardStockRank5;
            }
            return CardStockRank1;
        }

        private void setCardStock(byte rank, byte[] data)
        {
            if (rank == 1)
            {
                CardStockRank1 = data;
            }
            if (rank == 2)
            {
                CardStockRank2 = data;
            }
            if (rank == 3)
            {
                CardStockRank3 = data;
            }
            if (rank == 4)
            {
                CardStockRank4 = data;
            }
            if (rank == 5)
            {
                CardStockRank5 = data;
            }
        }
    }

}