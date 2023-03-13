
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;


namespace AutoVRC.Models
{

    public class CardGroup : Model
    {
        [UdonSynced]
        public byte[] CardIds = { };

        public byte MaxSize = 10;

        [Header("Models")]
        public Player Player;

        public void Add(byte cardId)
        {
            var len = CardIds.Length;
            if (len == MaxSize)
            {
                return;
            }

            byte[] data = new byte[len + 1];
            for (var i = 0; i < len; i++)
            {
                data[i] = CardIds[i];
            }
            data[len] = cardId;
            CardIds = data;
        }

        public void Remove(byte cardId)
        {
            var len = CardIds.Length;
            if (len == 0)
            {
                return;
            }

            byte[] data = new byte[len - 1];
            var offset = 0;
            for (var i = 0; i < len; i++)
            {
                var x = i + offset;
                if (CardIds[i] == cardId)
                {
                    offset = -1;
                    continue;
                }
                data[x] = CardIds[i];
            }
            CardIds = data;
        }

        public void MoveLeft(byte cardId)
        {
            var len = CardIds.Length;
            if (len <= 1)
            {
                return;
            }
            for (var i = 1; i < len; i++)
            {
                if (CardIds[i] == cardId)
                {
                    var prev = CardIds[i - 1];
                    CardIds[i - 1] = cardId;
                    CardIds[i] = prev;
                    return;
                }

            }
        }

        public void MoveRight(byte cardId)
        {
            var len = CardIds.Length;
            for (var i = 0; i < len - 1; i++)
            {
                if (CardIds[i] == cardId)
                {
                    var next = CardIds[i + 1];
                    CardIds[i + 1] = cardId;
                    CardIds[i] = next;
                    return;
                }

            }
        }

        public int GetCount()
        {
            return CardIds.Length;
        }

        public bool HasCard(ushort cardId)
        {
            return GetPosition(cardId) != -1;
        }

        public int GetPosition(ushort cardId)
        {
            var len = CardIds.Length;
            for (var i = 0; i < len; i++)
            {
                if (CardIds[i] == cardId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}