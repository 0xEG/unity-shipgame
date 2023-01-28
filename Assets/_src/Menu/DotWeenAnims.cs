using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Menu
{
    public class DotWeenAnims : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private bool isVertical;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Ease ease;
        public bool isOpened = false;
        private bool isOpening = false;

        [SerializeField] private float OutValue;

        [SerializeField] private float DefaultValue;
        public void OnPointerDown(PointerEventData eventData)
        {
            TryToOpen();
        }

        private void TryToOpen()
        {
            if (isOpening)
            {
                return;
            }

            StartCoroutine(OpenTab());
        }

        private IEnumerator OpenTab()
        {
            isOpening = true;
            isOpened = !isOpened;

        
            float TargetPos = isOpened ? OutValue: DefaultValue;
            float TargetRotation = TargetPos == DefaultValue ? OutValue : DefaultValue;


            TweenerCore<float, float, FloatOptions> tweener;
            if (isVertical)
            { 
                tweener = DOTween.To(() => TargetPos, x => TargetPos = x, TargetRotation, .7f)
                    .SetEase(ease)
                    .OnUpdate(() => _rectTransform.anchoredPosition = new Vector3(0, TargetPos));
            }

            else
            { 
                tweener = DOTween.To(() => TargetPos, x => TargetPos = x, TargetRotation, .7f)
                    .SetEase(ease)
                    .OnUpdate(() => _rectTransform.anchoredPosition = new Vector3(TargetPos, 0));
            }

            while (tweener.IsActive())
            {
                yield return null;
            
            }
            isOpening = false;
        }
    }
}
