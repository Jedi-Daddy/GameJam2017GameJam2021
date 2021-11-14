using UnityEngine;

namespace Assets.Scripts.ui
{
    public class PreloaderAnimator : MonoBehaviour
    {
        public static PreloaderAnimator Instance;
        private Animator _animator;

        void Awake()
        {
            Instance = this;
            _animator = GetComponentInChildren<Animator>();
        }

        public void Play(string name)
        {
            _animator.SetTrigger(name);
        }
    }
}