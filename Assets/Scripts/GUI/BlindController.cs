using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameUI
{
    public class BlindController : MonoBehaviour
    {
        public Texture2D black;
        private int fadeDir;
        public int FadeDirection { get { return fadeDir; } }
        public float fadeDuration;
        private float alfa;
        private float backCount = 0.8f;
        private Queue<GameObject> delayedSwichBufferO = new Queue<GameObject>();
        private Queue<MonoBehaviour> delayedSwichBufferB = new Queue<MonoBehaviour>();

        public void GoFadeIn()
        {
            GoFadeIn(0.8f);
        }
        public void GoFadeIn(float fadeDuration)
        {
            Debug.Log("TryFadeIn");
            this.fadeDuration = fadeDuration;
            backCount = fadeDuration;
            fadeDir = -1;
            alfa = 1;
        }
        public void GoFadeOut()
        {
            GoFadeOut(0.8f);
        }
        public void DelayedSwichEnquenue(GameObject delayedSwichObject)
        {
            if (delayedSwichObject != null)
                delayedSwichBufferO.Enqueue(delayedSwichObject);
        }
        public void DelayedSwichEnquenue(MonoBehaviour delayedSwichBehaviour)
        {
            if (delayedSwichBehaviour != null)
                delayedSwichBufferB.Enqueue(delayedSwichBehaviour); 
        }
        public void GoFadeOut(float fadeDuration)
        {
            Debug.Log("TryFadeOut");
            this.fadeDuration = fadeDuration;
            backCount = fadeDuration;
            fadeDir = 1;
            alfa = 0;
        }
        private void Update()
        {
            if (fadeDir != 0 && backCount <= 0)
            {
                fadeDir = 0;
                while (delayedSwichBufferO.Count > 0)
                {
                    GameObject x = delayedSwichBufferO.Dequeue();
                    x.SetActive(!x.activeSelf);
                }
                while (delayedSwichBufferB.Count > 0)
                {
                    MonoBehaviour x = delayedSwichBufferB.Dequeue();
                    x.enabled = !x.enabled;
                }
            }
            else
                backCount -= Time.unscaledDeltaTime;
            alfa = Mathf.Clamp01(alfa + ((1f / fadeDuration) * fadeDir * Time.deltaTime));
        }
        private void OnGUI()
        {
            GUI.depth = -100;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alfa);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), black);
        }
    }
}
