using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SetPosition
{
    public class SetPositionScene : MonoBehaviour
    {
        private void Start()
        {
            FieldManager.instance.CreateField();
        }

        public void OnClickDecide()
        {
            SceneManager.LoadScene(SceneConstant.Battle);
        }
    }
}