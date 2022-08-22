using UnityEngine;
using UnityEngine.UI;

namespace Gameflex.UISystem.Scripts
{
    public class UIStarUpdater : MonoBehaviour
    {
        [SerializeField] private Color _passiveStarColor;

        [SerializeField] private Image[] _stars;

        public void SetStarsColors(int starsCount)
        {
            int passiveStarsCount = 3 - starsCount;

            for (int i = _stars.Length - 1; i > _stars.Length - 1 - passiveStarsCount; i--)
            {
                _stars[i].color = _passiveStarColor;
            }
        }
    }
}
