using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace PerfectDrift
{
    public class MoneyCollectFX : MonoBehaviour
    {

        [SerializeField] private GameObject money;
        [SerializeField] private Transform fx_RaisePoint;
        [SerializeField] private GameObject target;
        [SerializeField] private float movementCount = 5f;
        [SerializeField] private float movementDuration = 1f;
        [SerializeField] private float movementDelay = .25f;


        public void OnMoneyGain()
        {
            StartCoroutine("PlayTweenCo");
        }

        private IEnumerator PlayTweenCo()
        {
            Vector3 screenPosition = fx_RaisePoint.transform.position;

            PlayTween(screenPosition);

            for (int i = 0; i < movementCount - 1; i++)
            {
                yield return new WaitForSeconds(movementDelay);
                PlayTween(screenPosition);
            }
        }

        private void PlayTween(Vector3 viewportPosition)
        {
            GameObject spawned = Instantiate(money, money.transform.parent);
            
            spawned.GetComponent<RectTransform>().position = viewportPosition;
            spawned.gameObject.SetActive(true);
            spawned.transform.DOLocalRotate(new Vector3(0, 0, 360), movementDuration).SetRelative();
            spawned.transform.DOMove(target.transform.position, movementDuration).SetEase(Ease.Linear).OnComplete((() =>
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                Destroy(spawned);
            }));
        }
    }
}
