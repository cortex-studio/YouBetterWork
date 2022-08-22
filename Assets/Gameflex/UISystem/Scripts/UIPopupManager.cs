using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Gameflex.UISystem.Scripts
{
    public class UIPopupManager : MonoBehaviour
    {
        [SerializeField] private VoidEvent _onVictoryAnimationFinished;
        [SerializeField] private VoidEvent _onFailAnimationFinished;
        
        public void ShowSucceedPopupAfterWait(float delay)
        {
            Invoke(nameof(RaiseVictoryAnimationFinished), delay);
        }
        
        public void ShowFailPopupAfterWait(float delay)
        {
            Invoke(nameof(RaiseFailAnimationFinished), delay);
        }

        private void RaiseVictoryAnimationFinished()
        {
            _onVictoryAnimationFinished.Raise();
        }
        
        private void RaiseFailAnimationFinished()
        {
            _onFailAnimationFinished.Raise();
        }
    }
}