using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battle.Unit;

namespace Battle
{
    public class BattleManager : Utility.SingletonMonoBehavior<BattleManager>
    {
        [SerializeField]
        float timeLimit;

        [SerializeField]
        Text timeText;

        /// <summary>
        /// バトルの試合のカウント用タイマー
        /// </summary>
        float timer;

        public void Initialize(List<UnitInfo> unitList)
        {
            FieldManager.instance.Initialize();
            UnitManager.instance.Initialize(unitList);

            //// TODO 仮データ
            var playerInfo = new PlayerInfo();
            playerInfo.CastleMaxHP = 50;
            playerInfo.CastleHP = playerInfo.CastleMaxHP;

            var enemyInfo = new PlayerInfo();
            enemyInfo.CastleMaxHP = 50;
            enemyInfo.CastleHP = playerInfo.CastleMaxHP;

            PlayerManager.instance.Initialize(playerInfo, enemyInfo);

            timer = timeLimit;
        }

        public void BattleFinished(BattleResult result)
        {
            switch(result)
            {
                case BattleResult.Win:
                    Debug.Log("勝利");
                    break;
                case BattleResult.Lose:
                    Debug.Log("敗北");
                    break;
                case BattleResult.Draw:
                    Debug.Log("引き分け");
                    break;
            }
        }

        void Update()
        {
            if (0 >= timer)
            {
                //// 引き分け判定
                if(PlayerManager.instance.playerInfo.CastleHP==PlayerManager.instance.enemyInfo.CastleHP)
                {
                    BattleFinished(BattleResult.Draw);
                }else
                {
                    //// どちらかのHPが少ない
                    var result = PlayerManager.instance.playerInfo.CastleHP > PlayerManager.instance.enemyInfo.CastleHP ? BattleResult.Win : BattleResult.Lose;
                    BattleFinished(result);
                }
            }
            else
            {
                timer -= Time.deltaTime;
                timeText.text = Mathf.FloorToInt(timer).ToString();
            }
        }
    }
}
