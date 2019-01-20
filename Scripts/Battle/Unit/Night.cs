using Battle;
using Battle.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    /// <summary>
    /// 兵士クラス
    /// </summary>
    public class Night : BaseUnit
    {
        public override void ExecuteSkill()
        {
            var pAtk = FieldManager.instance.panelList[UnitInfo.PanelIndex + 1].PlayerUnitInfo.UnitInfo.Atk;
            FieldManager.instance.panelList[UnitInfo.PanelIndex + 1].PlayerUnitInfo.UnitInfo.Atk = pAtk +3;
        }
    }
}