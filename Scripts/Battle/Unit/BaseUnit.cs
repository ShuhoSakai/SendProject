using Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.Unit;

namespace Unit
{
    public abstract class BaseUnit : MonoBehaviour, IUnit
    {
        public Transform Transform { get; set; }

        public UnitInfo UnitInfo { get; set; }

        public int MoveCounter;

        public List<NormalCube> MoveList = new List<NormalCube>();

        public Animator Animator { get; set; }

        public void ReceiveDamage(int damage, int id, int index, PlayerType type)
        {
            UnitInfo.NowHp -= damage;

            //// プレイヤーユニットのUIのHP更新
            if (UnitInfo.PlayerOrEnemy == PlayerType.Player)
            {
                UnitManager.instance.UpdateUI_Units(id);
            }

            //// Hp0切ってたら死亡フェーズへ
            if (UnitInfo.NowHp <= 0)
            {
                Death(id, index, type);
            }
        }

        public void CastleDamage(int value, PlayerType type)
        {
            PlayerManager.instance.CastleDamage(value, type);
        }

        public void Death(int id, int index, PlayerType type)
        {
            UnitManager.instance.DaethUnit(id, index, type);
        }

        public void PlayAnimator(AnimationConst.AnimState type)
        {
            var cont = gameObject.GetComponent<Animator>();

            switch(type)
            {
                case AnimationConst.AnimState.Idle:
                    cont.Play(AnimationConst.Idle);
                    break;
                case AnimationConst.AnimState.Walk:
                    cont.Play(AnimationConst.Walk);
                    break;
            }
        }

        void Update()
        {
            if (MoveCounter < MoveList.Count - 1)
            {
                PlayAnimator(AnimationConst.AnimState.Walk);

                var target = this.MoveList[MoveCounter + 1].transform.localPosition;
                UnitManager.instance.playerUnitList[UnitInfo.UnitId].Transform.localPosition = Vector3.MoveTowards(UnitManager.instance.playerUnitList[UnitInfo.UnitId].Transform.localPosition, target, UnitInfo.Spd * Time.deltaTime);

                //// プレイヤーの進んでいる向きに角度を変える
                var diff = FieldManager.instance.cubeList[this.MoveList[this.MoveCounter + 1].Index].transform.localPosition - UnitManager.instance.playerUnitList[UnitInfo.UnitId].Transform.localPosition;
                if (diff.magnitude > 0.01f)
                {
                    UnitManager.instance.playerUnitList[UnitInfo.UnitId].Transform.localRotation = Quaternion.LookRotation(diff);
                }

                //// 座標がゴールにたどり着いているかチェック
                if (UnitManager.instance.playerUnitList[UnitInfo.UnitId].Transform.localPosition == target)
                {
                    //// 自分のUnit情報が入っていたら
                    if (FieldManager.instance.panelList[this.MoveList[this.MoveCounter].Index].PlayerUnitInfo.UnitInfo.UnitId == UnitInfo.UnitId)
                    {
                        //// 自分が通過したマスをnullにする
                        FieldManager.instance.panelList[this.MoveList[this.MoveCounter].Index].PlayerUnitInfo = null;
                    }

                    //// すでに他のユニットの情報が入っていたら入れない
                    if (FieldManager.instance.panelList[this.MoveList[this.MoveCounter + 1].Index].PlayerUnitInfo == null)
                    {
                        //// 自分が通過中のマスに自分のユニット情報を入れる
                        FieldManager.instance.panelList[this.MoveList[this.MoveCounter + 1].Index].PlayerUnitInfo = UnitManager.instance.playerUnitList[UnitInfo.UnitId];
                    }

                    //// ユニット情報に今どのパネルにいるかを更新する
                    UnitInfo.PanelIndex = this.MoveList[this.MoveCounter + 1].Index;

                    //// たどり着いていたら次の目的地へ進む
                    this.MoveCounter++;
                }
            }
            else
            {
                PlayAnimator(AnimationConst.AnimState.Idle);
                //// 移動完了した
                MoveList.Clear();
                MoveCounter = 0;
            }
        }

        public void ExecuteMove(List<NormalCube> list)
        {
            MoveList.Clear();
            MoveList = list;

            MoveCounter = 0;
        }

        public virtual void ExecuteSkill()
        {
        }
    }
}