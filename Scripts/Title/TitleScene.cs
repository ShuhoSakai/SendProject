using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Title
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        Animator castleAnim;

        [SerializeField]
        Animator cameraAnim;

        [SerializeField]
        Material skyMaterial;

        [SerializeField]
        public float speed = 0.1f;

        private float rotation;

        private void Update()
        {
            rotation -= speed;
            if (0 >= rotation)
            {
                rotation = 360;
            }

            skyMaterial.SetFloat("_Rotation", rotation);
        }


        public void OnClickTitleButton()
        {
            castleAnim.Play("Castle 01 Door Open");
            cameraAnim.Play("Open");
            //SceneManager.LoadScene(SceneConstant.Menu);
        }
    }
}
