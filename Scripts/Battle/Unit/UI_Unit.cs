using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battle;
using Battle.Unit;

namespace Unit
{
    public class UI_Unit : MonoBehaviour
    {
        [SerializeField]
        UnitInfo unitInfo;

        [SerializeField]
        Text atkText;

        [SerializeField]
        Text intellectText;

        [SerializeField]
        Button unitButton;

        public void Show(UnitInfo info)
        {
            atkText.text = info.Atk.ToString();
            intellectText.text = info.Intellect.ToString();
            unitInfo = info;

            SetButtonEvent();
        }

        public void SetButtonEvent()
        {
            unitButton.onClick.RemoveAllListeners();
            unitButton.onClick.AddListener(() =>
            {
                switch (unitInfo.UnitState)
                {
                    case UnitState.Move:
                    case UnitState.Alive:
                        UnitManager.instance.SetSkillButtonEvent(()=>
                        {
                            UnitManager.instance.playerUnitList[unitInfo.UnitId].ExecuteSkill();
                        });
                        break;
                    case UnitState.Death:
                        FieldManager.instance.IsSummon = true;
                        FieldManager.instance.SummonUnitId = unitInfo.UnitId;
                        break;
                }
            });
        }
    }
}