using DeusUtility.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.UIElements;
namespace Assets.Scripts.GameUI
{
    public class MainHUD : MonoBehaviour
    {
        [SerializeField]
        private GameObject canvas;
        private float scale;
        private Rect mainRect;
        public enum Window { None, Victory, Defeat }
        public Window CurWin;
        private UIWindowInfo[] Windows;
        private GameController Global;

        [SerializeField]
        private GUISkin Skin;
        [SerializeField]
        private Image LevelProgressLeft;
        [SerializeField]
        private Image LevelProgressRight;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text livesText;
        [SerializeField]
        private Text levelText;
        private void OnEnable()
        {
            GameController.Instance.Blinds.GoFadeIn();
            canvas.SetActive(true);

            scale = Screen.width / (400f / 1f);
            mainRect = new Rect(0, 0, Screen.width / scale, Screen.height / scale);
            Global = GameController.Instance;

            Windows = new UIWindowInfo[2];
            Windows[0] = new UIWindowInfo(UIUtil.GetRect(new Vector2(400, 200), PositionAnchor.Center, mainRect.size));
        }
        private void OnDisable()
        {
            canvas.SetActive(false);
            //GameController.Instance.Blinds.DelayedSwichEnquenue(canvas);
            //GameController.Instance.Blinds.GoFadeIn();
            CurWin = Window.None;
        }
        // Update is called once per frame
        private void Update()
        {
            LevelProgressLeft.fillAmount = GameController.LevelProgress;
            LevelProgressRight.fillAmount = GameController.LevelProgress;
            scoreText.text = "Score: " + Mathf.Round(GameController.LevelScore);
            livesText.text = "Lives: " + GameController.LiveCount;
            levelText.text = "Difficult " + GameController.CurrentLevel;
        }

        private void OnGUI()
        {
            GUI.skin = Skin;
            if (scale != 1)
                GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);
            GUI.BeginGroup(mainRect);
            switch (CurWin)
            {
                case Window.Victory:
                    {
                        GUI.Window(0, Windows[0].rect, DrawVictoryW, "");
                        break;
                    }
                case Window.Defeat:
                    {
                        GUI.Window(0, Windows[0].rect, DrawDefeatW, "");
                        break;
                    }
            }
            GUI.EndGroup();
        }
        void DrawVictoryW(int windowID)
        {
            UIUtil.WindowTitle(Windows[windowID], Global.Texts("Victory"));
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 100)), Global.Texts("Main menu")))
            {
                GameController.Instance.LevelOut();
            }
        }
        void DrawDefeatW(int windowID)
        {
            UIUtil.WindowTitle(Windows[windowID], Global.Texts("Defeat"));
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 100)), Global.Texts("Main menu")))
            {
                GameController.Instance.LevelOut();
            }
        }
    }
}