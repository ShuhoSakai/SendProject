using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class PlayerManager : Utility.SingletonMonoBehavior<PlayerManager>
    {
        [SerializeField]
        RectTransform playerCastleHP_Gage;

        [SerializeField]
        RectTransform enemyCastleHP_Gage;

        public PlayerInfo playerInfo;

        public PlayerInfo enemyInfo;

        public void Initialize(PlayerInfo player,PlayerInfo enemy)
        {
            playerInfo = player;
            enemyInfo = enemy;
        }

        /// <summary>
        /// 城にダメージを与えるメソッド
        /// </summary>
        /// <param name="value">与える数値.</param>
        /// <param name="type">自分側か敵側か.</param>
        public void CastleDamage(float value, PlayerType type)
        {
            Debug.Log("CastleDamage");

            switch(type)
            {
                case PlayerType.Player:
                    {
                        playerInfo.CastleHP -= (int)value;
                        var slideValue = 1f - ((float)playerInfo.CastleHP / (float)playerInfo.CastleMaxHP);
                        if (slideValue <= 0f)
                        {
                            slideValue = 0f;
                        }
                        playerCastleHP_Gage.localPosition = new Vector3(slideValue * Castle.HPSlideValue, playerCastleHP_Gage.localPosition.y, playerCastleHP_Gage.localPosition.z);

                        if (playerInfo.CastleHP <= 0)
                        {
                            BattleManager.instance.BattleFinished(BattleResult.Lose);
                        }
                    }
                    break;
                case PlayerType.Enemy:
                    {
                        enemyInfo.CastleHP -= (int)value;
                        var slideValue = 1f - ((float)enemyInfo.CastleHP / (float)enemyInfo.CastleMaxHP);
                        if (slideValue <= 0f)
                        {
                            slideValue = 0f;
                        }
                        enemyCastleHP_Gage.localPosition = new Vector3(slideValue * Castle.HPSlideValue, enemyCastleHP_Gage.localPosition.y, enemyCastleHP_Gage.localPosition.z);

                        if(enemyInfo.CastleHP <=0)
                        {
                            BattleManager.instance.BattleFinished(BattleResult.Win);

                        }
                    }
                    break;
            }
        }
    }
}