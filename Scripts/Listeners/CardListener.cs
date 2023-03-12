
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;
using AutoVRC.Models;
using AutoVRC.Controllers;
using AutoVRC.Tools;
using AutoVRC.Templates;

namespace AutoVRC.Listeners
{
    public class CardListener : Listener
    {
        [Header("Models")]
        public Models.Card Card;

        public CardTemplateManager CardTemplateManager;

        public byte CardTemplateId = 0;
        public CardTemplate CardTemplate = null;
        public Text Rank;
        public Text Title;
        public Text Damage;

        public Text Health;
        public Transform HandCardGroup;
        public Transform FieldCardGroup;
        public Transform ShopCardGroup;
        public GameObject Mesh;

        private Vector3 targetPosition = Vector3.zero;

        void Start()
        {
            updateDisplay();
        }

        void FixedUpdate()
        {
            var animationSpeed = 0.1f;
            if (transform.position != targetPosition)
            {
                var dist = Vector3.Distance(transform.position, targetPosition);
                if (dist < 0.01f)
                {
                    transform.position = targetPosition;
                    Card.OnSync();
                }
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / animationSpeed);
            }
        }

        public override void OnBootstrap()
        {
            Subscribe(Card);
            Subscribe(Card.Player);
            Subscribe(Card.Player.GameMaster);
            foreach (var group in Card.Player.CardGroups)
            {
                Subscribe(group);
            }
        }

        public override void OnModelSync()
        {
            if (!toggleVisibility())
            {
                return;
            }
            updateValues();
            updatePosition();
        }

        private bool toggleVisibility()
        {
            if (!Card.Player.GameMaster.GameInProgress
                || !Card.InCardGroup()
                || !Card.Player.InGame)
            {
                gameObject.SetActive(false);
                return false;
            }
            gameObject.SetActive(true);
            return true;
        }

        private void updateValues()
        {
            if (CardTemplateId != Card.CardTemplateId)
            {
                updateDisplay();
            }
            Damage.text = Card.Damage.ToString();
            Health.text = Card.Health.ToString();
        }

        private void updateDisplay()
        {
            CardTemplate = CardTemplateManager.GetTemplate(Card.CardTemplateId);
            Rank.text = CardTemplate.Rank.ToString();
            Title.text = CardTemplate.Title.ToString();
            Mesh.transform.Find("Front").GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CardTemplate.Art);
        }

        private void updatePosition()
        {
            var cardGroup = Card.GetCardGroup();
            if (!cardGroup)
            {
                return;
            }
            var trans = getCardGroupTransform();
            var count = cardGroup.GetCount();
            var width = Mesh.GetComponentInChildren<MeshRenderer>().bounds.size.x;
            var margin = width * 0.3f;
            var length = width * count;
            var index = cardGroup.GetPosition(Card.CardId);
            var position = trans.position;
            position.x -= length / 2;
            position.x += width * index;
            if (index > 0)
            {
                position.x += margin * index;
            }
            targetPosition = position;
        }

        private Transform getCardGroupTransform()
        {
            if (Card.InHand())
            {
                return HandCardGroup;
            }
            else if (Card.InField())
            {
                return FieldCardGroup;
            }
            else if (Card.InShop())
            {
                return ShopCardGroup;
            }
            return null;
        }
    }
}