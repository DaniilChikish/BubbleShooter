using DeusUtility.UI;
using UnityEngine;

namespace Assets.Scripts.GameUI
{
    public class MainMenu : MonoBehaviour
    {
        public enum Window { MainMenu, Options, Shop}
        public Window CurWin;

        [SerializeField]
        private GUISkin Skin;
        private UIWindowInfo[] Windows;
        private float scale;
        private Vector2 screenRatio;
        private Rect mainRect;
        private float backCount;
        private float fBuffer;

        private void Start()
        {
            {
                scale = Screen.width / (400f / 1f);
                mainRect = new Rect(0, 0, Screen.width / scale, Screen.height / scale);
            }
            Windows = new UIWindowInfo[3];
            Windows[0] = new UIWindowInfo(UIUtil.GetRect(new Vector2(400, 400), PositionAnchor.Center, mainRect.size));//main
            Windows[1] = new UIWindowInfo(UIUtil.GetRect(new Vector2(400, 400), PositionAnchor.Center, mainRect.size));
            Windows[2] = new UIWindowInfo(UIUtil.GetRect(new Vector2(400, 200), PositionAnchor.Center, mainRect.size));
        }
        private void OnEnable()
        {
            if (Time.realtimeSinceStartup > 10)
                GameController.Instance.Blinds.GoFadeIn();
        }
        private void Update()
        {
            switch (CurWin)
            {
                case Window.MainMenu:
                    {
                        break;
                    }
                //case Window.Game:
                //    {
                //        break;
                //    }
                case Window.Options:
                    {
                        break;
                    }
                case Window.Shop:
                    {
                        break;
                    }
            }
        }
        void OnGUI()
        {
            GUI.skin = Skin;
            if (scale != 1)
                GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);
            GUI.BeginGroup(mainRect);
            switch (CurWin)
            {
                case Window.MainMenu:
                    {
                        GUI.Window(0, Windows[0].rect, DrawMainW, "");
                        break;
                    }
                case Window.Options:
                    {
                        GUI.Window(1, Windows[1].rect, DrawOptionsW, "");
                        break;
                    }
                case Window.Shop:
                    {
                        GUI.Window(2, Windows[2].rect, DrawShopW, "");
                        break;
                    }
            }
            GUI.EndGroup();
            UIUtil.Exclamation(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.LeftDown, mainRect.size), "by Jogo Deus");
            UIUtil.Exclamation(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.RightDown, mainRect.size), "v. " + Application.version);
        }
        void DrawMainW(int windowID)
        {
            UIUtil.WindowTitle(Windows[windowID], GameController.Instance.Texts("Bubbles"));
            //GUI.color.a = window.UIAlpha;
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 100)), GameController.Instance.Texts("Play")))
            {
                GameController.Instance.GoSingleGame();
                //CurWin = Window.Game;
            }
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 150)), GameController.Instance.Texts("Time Atack")))
            {
                GameController.Instance.GoAtack();
                //CurWin = Window.Game;
            }
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 200)), GameController.Instance.Texts("Shop")))
            {
                CurWin = Window.Shop;
            }
            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 250)), GameController.Instance.Texts("Options")))
            {
                CurWin = Window.Options;
            }
        }
        void DrawOptionsW(int windowID)
        {
            UIUtil.WindowTitle(Windows[windowID], GameController.Instance.Texts("Options"));
            GUI.BeginGroup(UIUtil.GetRect(new Vector2(300, 55), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 100)));
            UIUtil.Label(new Rect(100, 0, 100, 20), GameController.Instance.Texts("Sound"));
            fBuffer = GUI.HorizontalSlider(new Rect(0, 40, 300, 13), GameController.SoundLevel, 0.0f, 1f);
            if (GameController.SoundLevel != fBuffer)
            {
                GameController.SoundLevel = fBuffer;
            }
            GUI.EndGroup();

            GUI.BeginGroup(UIUtil.GetRect(new Vector2(300, 55), PositionAnchor.Up, Windows[windowID].rect.size, new Vector2(0, 165)));
            UIUtil.Label(new Rect(100, 0, 100, 20), GameController.Instance.Texts("Music"));
            fBuffer = GUI.HorizontalSlider(new Rect(0, 40, 300, 13), GameController.MusicLevel, 0.0f, 1f);
            if (GameController.MusicLevel != fBuffer)
            {
                GameController.MusicLevel = fBuffer;
            }
            GUI.EndGroup();

            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Down, Windows[windowID].rect.size, new Vector2(0, -50)), GameController.Instance.Texts("Back")))
            {
                CurWin = Window.MainMenu;
            }
        }
        void DrawShopW(int windowID)
        {
            UIUtil.WindowTitle(Windows[windowID], GameController.Instance.Texts("Shop"));

            if (UIUtil.ButtonBig(UIUtil.GetRect(new Vector2(200, 50), PositionAnchor.Down, Windows[windowID].rect.size, new Vector2(0, -50)), GameController.Instance.Texts("Back")))
            {
                CurWin = Window.MainMenu;
            }
        }
    }
}
