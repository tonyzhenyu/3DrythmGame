using System;
using System.Collections;
using UnityEngine;

namespace ZYTools.QuickAnimate
{
    
    //how to use ->
    public class QuickAnimator : MonoBehaviour
    {
        public delegate void QuickAnimationHandler();
        public event QuickAnimationHandler OnStart;
        public event QuickAnimationHandler OnEvaluate;
        public event QuickAnimationHandler OnEnd;

        public QuickAnimation animaType;

        private void Awake()
        {
            animaType = new QuickAnimation();
        }
        private void Start()
        {
            StartCoroutine("PlayAnimation");
        }
        private void Update()
        {
            animaType.Evaluate();
            OnEvaluate?.Invoke();
        }
        private void OnDestroy()
        {
            CLearAllEvent(OnStart);
            CLearAllEvent(OnEvaluate);
            CLearAllEvent(OnEnd);
        }
        //clearall
        public void CLearAllEvent(QuickAnimationHandler myevent)
        {
            if (myevent == null) return;
            Delegate[] dels = myevent.GetInvocationList();
            foreach (Delegate del in dels)
            {
                object delObj = del.GetType().GetProperty("Method").GetValue(del, null);
                string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null); 
                myevent -= del as QuickAnimationHandler;
            }
        }
        IEnumerator PlayAnimation()
        {
            yield return new WaitForSeconds(animaType.delay);
            animaType.myState = AnimaState.Play;
            OnStart?.Invoke();

            yield return new WaitForSeconds(animaType.delay + animaType.duration);
            animaType.myState = AnimaState.Stop;
            OnEnd?.Invoke();
            Destroy(this);
        }
    }

}

