using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZY{
    public class InputHandler : MonoBehaviour
    {
        [HideInInspector] public Vector2 inputAxies { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }
        [HideInInspector] public float mouseScroller { get { return Input.GetAxis("Mouse ScrollWheel"); } }
        [HideInInspector] public bool button_attack;

        public bool leftBtnClick { get { return OnClick(KeyCode.LeftArrow)|| OnClick(KeyCode.A); } }
        public bool rightBtnClick { get { return OnClick(KeyCode.RightArrow)|| OnClick(KeyCode.D); } }
        public bool leftBtnHold { get { return OnHold(KeyCode.LeftArrow) || OnHold(KeyCode.A); } }
        public bool rightBtnHold { get { return OnHold(KeyCode.RightArrow) || OnHold(KeyCode.D); } }
        public bool upBtnClick { get { return OnClick(KeyCode.UpArrow) || OnClick(KeyCode.W); } }
        public bool downBtnClick { get { return OnClick(KeyCode.DownArrow) || OnClick(KeyCode.S); } }
        public bool upBtnHold { get { return OnHold(KeyCode.UpArrow) || OnHold(KeyCode.W); } }
        public bool downBtnHold { get { return OnHold(KeyCode.DownArrow) || OnHold(KeyCode.S); } }

        float timer = 0;
        private bool OnClick(KeyCode Key, float threshold = 0.1f)
        {
            if (Input.GetKey(Key)&&timer<=threshold)
            {
                timer += Time.deltaTime;
                return true;
            }
            else
            {
                if (Input.GetKeyUp(Key))
                {
                    timer = 0;
                }
                return false;
            }
        }
        private bool OnHold(KeyCode Key)
        {
            if (Input.GetKey(Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
