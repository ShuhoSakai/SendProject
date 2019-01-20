using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Battle
{
    public class TouchPanel : MonoBehaviour
    {
        public Unit.IUnit PlayerUnitInfo;

        public Unit.IUnit EnemyUnitInfo;

        public PanelType PanelType;

        /// <summary>
        /// どこにぱねるが　配置されてるかのための変数
        /// </summary>
        public int Index
        {
            get
            {
                return this.index;
            }

            set
            {
                this.index = value;
            }

        }

        /// <summary>
        /// 値を見るためよう,Inspectorでいじることはない
        /// </summary>
        [SerializeField]
        int index;

        /// <summary>
        /// これをtrueにするとタッチパネルが見えるようになる
        /// </summary>
        [SerializeField]
        bool debug;

        [SerializeField]
        SpriteRenderer spriteRenderer;

        float time = 3f;

        float castleDamageTime = 3f;

        void Start()
        {
            this.spriteRenderer.color = debug ? Color.white : new Color(0f, 0f, 0f, 0f);
        }

        void Update()
        {
            #region スモーク発生
            FieldManager.instance.cubeList[index].SmokeParticle.SetActive(this.PlayerUnitInfo != null && this.EnemyUnitInfo != null);
            #endregion

            #region キャラ同士の当たり判定
            if (this.PlayerUnitInfo != null && this.EnemyUnitInfo != null)
            {
                time -= Time.deltaTime;
                if (time <= 0.0)
                {
                    time = 3.0f;

                    var enemyAtk = this.EnemyUnitInfo.UnitInfo.Atk;
                    var playerUnitId = this.PlayerUnitInfo.UnitInfo.UnitId;
                    var playerPanelIndex = this.PlayerUnitInfo.UnitInfo.PanelIndex;
                    var playerType = this.PlayerUnitInfo.UnitInfo.PlayerOrEnemy;

                    var playerAtk = this.PlayerUnitInfo.UnitInfo.Atk;
                    var enemyUnitId = this.EnemyUnitInfo.UnitInfo.UnitId;
                    var enemyrPanelIndex = this.EnemyUnitInfo.UnitInfo.PanelIndex;
                    var enemyType = this.EnemyUnitInfo.UnitInfo.PlayerOrEnemy;

                    this.PlayerUnitInfo.ReceiveDamage(enemyAtk, playerUnitId, playerPanelIndex, playerType);
                    this.EnemyUnitInfo.ReceiveDamage(playerAtk, enemyUnitId, enemyrPanelIndex, enemyType);
                }
            }
            #endregion

            #region キャラが城の前にいたら城を殴らせる
            if (this.PanelType == PanelType.NearCastle && this.PlayerUnitInfo != null)
            {
                castleDamageTime -= Time.deltaTime;
                if (castleDamageTime <= 0.0)
                {
                    castleDamageTime = 3.0f;
                    this.PlayerUnitInfo.CastleDamage(this.PlayerUnitInfo.UnitInfo.Atk, PlayerType.Enemy);
                }
            }
            #endregion
        }
    }
}