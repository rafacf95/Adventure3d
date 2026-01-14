using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease ease = Ease.OutBack;

        [Header("Movement")]
        public float minDistance = 1f;
        public float speed = 5f;
        public List<Transform> waypoints;


        private StateMachine<BossAction> _stateMachine;

        private void Start()
        {
            Init();
        }
        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();

            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        }
        public void SwitchState(BossAction state)
        {
            _stateMachine.SwitchState(state, this);
        }

        #region DEBUG

        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }


        #endregion


        #region ANIMATION

        public void StartAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(ease).From();
        }

        #endregion

        #region MOVEMENT

        public void GoToRandomPoint()
        {
            StartCoroutine(GoToPointCoroutine(waypoints[Random.Range(0, waypoints.Count)]));
        }

        IEnumerator GoToPointCoroutine(Transform t)
        {
            while(Vector3.Distance(transform.position, t.position) > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion


    }

}
