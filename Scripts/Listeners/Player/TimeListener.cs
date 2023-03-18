
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

using AutoVRC.Framework;


namespace AutoVRC.Listeners.Player
{
    public class TimeListener : Listener
    {
        [Header("Models")]
        public Models.Player Player;
        public Text RoundTime;
        public Text TotalTime;

        private float _fixedUpdateTimeSinceLastTick = 0;
        void FixedUpdate()
        {
            _fixedUpdateTimeSinceLastTick += Time.deltaTime;
            if (!Player.GameMaster.GameInProgress)
            {
                return;
            }
            if (_fixedUpdateTimeSinceLastTick < 0.1)
            {
                return;
            }
            updateText(Player.GameMaster.RoundSeconds, Player.GameMaster.TotalGameTime);
        }

        public override void OnBootstrap()
        {
            gameObject.SetActive((Player.InGame && Player.GameMaster.GameInProgress));
        }

        private void updateText(int roundTime, float totalTime)
        {
            string totalTimeText = string.Format("{0:N1}", totalTime);
            if (RoundTime.text != roundTime.ToString())
            {
                RoundTime.text = roundTime.ToString();
            }
            if (TotalTime.text != totalTimeText)
            {
                TotalTime.text = totalTimeText.ToString();
            }

        }
    }
}
