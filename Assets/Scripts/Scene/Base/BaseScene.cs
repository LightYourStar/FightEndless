using UnityEngine;

namespace LD
{

    public abstract class BaseScene
    {
        public static float TimeScale { set; get;}

        // public LDBattleCamera BattleCamera { private set; get; }
        protected int m_PauseRef = 0;
        public string m_BgmAudio = string.Empty;

        private int BossBgmRef = 0;
        public static float GetDtTime()
        {
            return Time.deltaTime * TimeScale;
        }
        public virtual void Init()
        {
            // Global.gApp.CurScene = this;
            TimeScale = 1;
            m_PauseRef = 0;
            CloseLoadUI();
        }
        public virtual void CloseLoadUI()
        {
            Global.gApp.gUiMgr.CloseLoadingUI();
        }
        protected virtual void InitCamera(string path)
        {
        }

        public virtual void Update(float dt)
        {
        }
        public virtual void OnDestroy()
        {
            // Global.gApp.CurScene = null;
            TimeScale = 1;
            // BattleCamera.DestroySelf();
        }
    }
}