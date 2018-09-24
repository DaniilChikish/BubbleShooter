using SpaceCommander.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        #region Singleton
        private static GameController instance;
        /// <summary>
        /// Return singleton
        /// </summary>
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject newGlobal = new GameObject("GameController");
                    newGlobal.tag = "GameController";
                    instance = newGlobal.AddComponent<GameController>();
                    instance.Initialise();
                }
                return instance;
            }
        }
        #endregion

        #region Data fields
        //properties
        private float curLevel;
        public static float CurrentLevel { get { return Instance.curLevel; } protected set { Instance.curLevel = value; } }

        private float levelProg;
        public static float LevelProgress { get { return Instance.levelProg; } }

        private float levelScore;
        public static float LevelScore { get { return Instance.levelScore; } }

        private int liveCount;
        public static int LiveCount { get { return Instance.liveCount; } }

        private const float maxMaterial = 100;
        private float levelCounter;
        //settings
        private float volMain = 1;
        public static float SoundLevel { set{ Instance.volMain = value; } get { return Instance.volMain; } }
        public static float MusicLevel { set { Instance.musicPlayer.SetVolume(value); } get { return Instance.musicPlayer.GetVolume(); } }
        #endregion

        #region Modules
        private GameUI.BlindController blinds;
        public GameUI.BlindController Blinds { get { return blinds; } }
        private GameUI.MainHUD hud;
        private GameUI.MainMenu menu;
        private BubbleEmmiter emmiter;
        private GameSpecs spec;
        private UserProfile profile;
        private MusicManager musicPlayer;
        /// <summary>
        /// Reference to BubbleEmmiter
        /// </summary>
        public BubbleEmmiter Emmiter { get { return emmiter; } }
        private SoundStorage sound;
        /// <summary>
        /// Reference to SoundStorage
        /// </summary>
        public SoundStorage Sound { get { return sound; } }
        /// <summary>
        /// Interface for localisations module
        /// </summary>
        public string Texts(string key)
        {
            //return localTexts.GetText("Text." + Settings.Localisation.ToString(), key);
            return key;
        }
        #endregion

        #region Basic functionality
        void OnEnable()
        {
            instance = this;
            Initialise();
        }

        void Update()
        {
            levelCounter += Time.deltaTime;
            levelProg = emmiter.Materials / maxMaterial;
        }
        private void LateUpdate()
        {
            if (emmiter.isActiveAndEnabled)
                CheckVictory();
        }
        private void Initialise()
        {
            blinds = FindObjectOfType<GameUI.BlindController>();
            hud = FindObjectOfType<GameUI.MainHUD>();
            menu = FindObjectOfType<GameUI.MainMenu>();
            emmiter = FindObjectOfType<BubbleEmmiter>();
            sound = FindObjectOfType<SoundStorage>();
            musicPlayer = FindObjectOfType<MusicManager>();
            spec = LoadSpec();
        }
        private GameSpecs LoadSpec()
        {
            return new GameSpecs();
        }
        private UserProfile LoadUserProfile()
        {
            UserProfile user;
            if (Serializer.Load(out user))
                Debug.Log("Loaded");
            else
                Debug.Log("Not load");

            return user;
        }
        private void SaveUserProfile()
        {
            profile.prevSessionScore = LevelScore;
            if (LevelScore > profile.recordScore)
                profile.recordScore = LevelScore;
            if (Serializer.Save(profile))
                Debug.Log("Saved");
            else Debug.Log("Saven't");
        }
        #endregion

        #region Gameplay control
        public void IncrementScore()
        {
            levelScore++;
        }
        public void IncrementScore(float x)
        {
            levelScore += x;
        }
        public void DecrementLives()
        {
            liveCount--;
        }
        public void DecrementLives(int x)
        {
            liveCount -= x;
        }

        public void GoSingleGame()
        {
            levelScore = 0;
            liveCount = 3;

            profile = LoadUserProfile();
            float dificultFactor = profile.prevSessionScore / 1000;
            Debug.Log("defF = " + dificultFactor);
            CurrentLevel = System.Convert.ToSingle(System.Math.Round(dificultFactor, 2));

            Blinds.DelayedSwichEnquenue(hud);
            Blinds.DelayedSwichEnquenue(menu);
            Blinds.DelayedSwichEnquenue(emmiter);
            emmiter.Initialize(maxMaterial, dificultFactor, 70, 15);
            Blinds.GoFadeOut();
        }
        public void GoMultiplayer()
        {
            GoSingleGame();
        }
        public void GoAtack()
        {
            GoSingleGame();
        }
        public void CheckVictory()
        {
            if (liveCount <= 0)
            {
                hud.CurWin = GameUI.MainHUD.Window.Defeat;
            }
            else if (emmiter.Materials <= 0)
            {
                hud.CurWin = GameUI.MainHUD.Window.Victory;
            }
        }
       public void LevelOut()
        {
            Blinds.DelayedSwichEnquenue(hud);
            Blinds.DelayedSwichEnquenue(menu);
            Blinds.DelayedSwichEnquenue(emmiter);
            Blinds.GoFadeOut();
            SaveUserProfile();
        }
        #endregion
    }
}