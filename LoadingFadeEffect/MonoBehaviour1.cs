using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace _Utility
{
    public class LoadingFadeEffect : MonoBehaviour
    {
        public static bool CanLoadScene = false;

        [SerializeField] private Image loadingBackground;
        [SerializeField] [Range(0, 0.005f)] private float loadingStepTime;
        [SerializeField] [Range(0, 0.1f)] private float loadingStepValue;

        public static LoadingFadeEffect Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        // Sahne yüklenmesi için ekran rengi kararıyor
        private IEnumerator FadeInEffect()
        {
            Color backgroundColor = loadingBackground.color;
            backgroundColor.a = 0;
            loadingBackground.color = backgroundColor;
            loadingBackground.gameObject.SetActive(true);

            WaitForSeconds wait = new WaitForSeconds(loadingStepTime);

            while (backgroundColor.a <= 1)
            {
                yield return wait;

                backgroundColor.a += loadingStepValue;
                loadingBackground.color = backgroundColor;
            }

            CanLoadScene = true;
        }

        // Sahne yüklendi ve ekran rengi açılıyor
        private IEnumerator FadeOutEffect()
        {
            CanLoadScene = false;

            Color backgroundColor = loadingBackground.color;

            WaitForSeconds wait = new WaitForSeconds(loadingStepTime);

            while (backgroundColor.a >= 0)
            {
                yield return wait;

                backgroundColor.a -= loadingStepValue;
                loadingBackground.color = backgroundColor;
            }
            
            loadingBackground.gameObject.SetActive(false);
        }

        private IEnumerator FadeAllEffect()
        {
            yield return StartCoroutine(FadeInEffect());

            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(FadeOutEffect());
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInEffect());
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutEffect());
        }

        public void FadeAll()
        {
            StartCoroutine(FadeAllEffect());
        }
    }
}