using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Battle;
using System.Linq;

namespace UnitSelect
{
    public class SelectUnitView : SingletonMonoBehavior<SelectUnitView>
    {
        [SerializeField]
        Transform content;

        [SerializeField]
        UnitNode unitNode;

        [SerializeField]
        List<UnitInfo> unitList = new List<UnitInfo>();

        public void AddUnit(UnitInfo info)
        {
            unitList.Add(info);

            foreach(Transform n in content)
            {
                Destroy(n.gameObject);
            }

            foreach (var unit in unitList)
            {
                var node = Instantiate(unitNode, content);
                node.Show(unit,()=>
                {
                    Debug.Log("ButtonEvent");
                });
            }
        }

        public void RemoveUnit(UnitInfo info)
        {
            unitList.Remove(info);

            foreach (Transform n in content)
            {
                if(n.GetComponent<UnitNode>().Info.UnitId == info.UnitId)
                {
                    Destroy(n.gameObject);
                }
            }
        }
    }
}