using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Main.Scripts.UI
{
    public class BubbleButton : MonoBehaviour
    {
        [SerializeField] private BubbleMenu menu;
        [SerializeField] private float scaleMultiplier = 1.2f;
        [SerializeField] private float scaleTime = 0.5f;
        [SerializeField] private AudioClip buttonClickedSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private UnityEvent onClick;

        private Vector3 _originalScale;

        private bool _isSelected = false;
        private bool _isEnabled = true;
        
        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = _originalScale;
        }

        public void Hover()
        {
            if (!_isEnabled) return; 
            
            LeanTween.scale(gameObject, _originalScale * scaleMultiplier, scaleTime)
                .setEase(LeanTweenType.easeInOutCubic);
        }
        
        public void Click()
        {
            if(!_isEnabled) return;
            
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(buttonClickedSound);

            onClick.Invoke();
        }
        
        public void Reset()
        {
            gameObject.SetActive(true);
            
            LeanTween.cancel(gameObject);
            // reset size of button gradually
            LeanTween.scale(gameObject, _originalScale, 0.1f).setEase(LeanTweenType.easeOutElastic);
        }
        
        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }
    }
}
