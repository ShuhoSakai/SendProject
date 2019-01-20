using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Battle.Unit;

namespace Battle
{
    public class BattleScene : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーのユニットリストデータ
        /// </summary>
        List<UnitInfo> unitList = new List<UnitInfo>();

        void Start()
        {
            {
                //// 仮データ作成
                var playerUnit = new UnitInfo();
                playerUnit.MaxHp = 10;
                playerUnit.UnitId = 1;
                playerUnit.Name = "プレイヤー1";
                playerUnit.NowHp = playerUnit.MaxHp;
                playerUnit.Atk = 3;
                playerUnit.Spd = 1;
                playerUnit.PlayerOrEnemy = PlayerType.Player;
                playerUnit.PanelIndex = 0;
                playerUnit.Intellect = 5;
                playerUnit.UnitState = UnitState.Alive;

                this.unitList.Add(playerUnit);

                var playerUnit2 = new UnitInfo();
                playerUnit2.MaxHp = 10;
                playerUnit2.UnitId = 2;
                playerUnit2.Name = "プレイヤー2";
                playerUnit2.NowHp = playerUnit.MaxHp;
                playerUnit2.Atk = 3;
                playerUnit2.Spd = 1;
                playerUnit2.PlayerOrEnemy = PlayerType.Player;
                playerUnit2.PanelIndex = 1;
                playerUnit2.Intellect = 5;
                playerUnit2.UnitState = UnitState.Alive;

                this.unitList.Add(playerUnit2);
            }

            {
                //// 仮データ作成
                var enemyUnit = new UnitInfo();
                enemyUnit.MaxHp = 10;
                enemyUnit.UnitId = 10;
                enemyUnit.Name = "エネミー1";
                enemyUnit.NowHp = enemyUnit.MaxHp;
                enemyUnit.Atk = 3;
                enemyUnit.Spd = 1;
                enemyUnit.Intellect = 5;
                enemyUnit.PlayerOrEnemy = PlayerType.Enemy;
                enemyUnit.PanelIndex = 8;
                enemyUnit.UnitState = UnitState.Alive;

                this.unitList.Add(enemyUnit);
            }

            BattleManager.instance.Initialize(this.unitList);
        }
    }
}
