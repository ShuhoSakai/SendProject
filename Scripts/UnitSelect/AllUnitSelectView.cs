using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Battle;

namespace UnitSelect
{
    public class AllUnitSelectView : SingletonMonoBehavior<AllUnitSelectView>
    {
        [SerializeField]
        Transform content;

        [SerializeField]
        UnitNode unitNode;

        public void ShowUnits(UnitInfo[] infos)
        {
            foreach (var info in infos)
            {
                var node = Instantiate(unitNode, content);
                node.Show(info,()=>
                {
                    if(node.State == SelectState.Free)
                    {
                        node.State = SelectState.Select;
                        SelectUnitView.instance.AddUnit(info);
                    }
                    else
                    {
                        node.State = SelectState.Free;
                        SelectUnitView.instance.RemoveUnit(info);
                    }
                });
            }
        }
    }
}