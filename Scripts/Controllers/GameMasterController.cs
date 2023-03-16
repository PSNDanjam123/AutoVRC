
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

using AutoVRC.Framework;
using AutoVRC.Models;
using AutoVRC.Listeners;

namespace AutoVRC.Controllers
{

    public class GameMasterController : Controller
    {
        public static void HandleTick(GameMaster GameMaster, GameTickListener gameTickListener)
        {
            gameTickListener.HandlingTick = true;
            handleShopRequests(GameMaster);
            gameTickListener.HandlingTick = false;
        }

        private static void handleShopRequests(GameMaster GameMaster)
        {
            foreach (var player in GameMaster.Players)
            {
                if (!player.WaitingOnGameMaster)
                {
                    continue;
                }
                handleShopRequest(GameMaster.Shop, player);
            }
        }

        private static void handleShopRequest(Shop Shop, Player Player)
        {
            Player.SetOwner();

            // Remove cards from shop
            foreach (var card in Player.Cards)
            {
                if (card.InShop())
                {
                    card.SetOwner();
                    card.Shop.SetOwner();
                    card.Shop.Remove(card.CardId);
                    Shop.AddCard(card.CardTemplateId);
                    card.Shop.Sync();
                    card.Sync();
                }
            }

            var refreshAmount = 3;
            for (var i = 0; i < refreshAmount; i++)
            {
                var card = Player.GetCardWithoutGroup();
                if (card == null)
                {
                    break; // player has no valid cards
                }
                var template = Shop.PluckRandom(Player.Rank);
                if (template == null)
                {
                    break;
                }
                card.SetOwner();
                card.LoadTemplate(template);
                card.AddToShop();
                card.Sync();
            }
            Player.WaitingOnGameMaster = false;
            Player.Sync();
        }
    }

}