using LD.Loading;
using System;

namespace LD
{
    public class LoadingFrameCtrl : FrameCtrl
    {
        //LDPassInfo m_BattleInfo;
        public bool IsLoadingScene { set; get; }

        public LoadingFrameCtrl(BaseScene scene) : base(scene)
        {
        }

        public void ChangeToLoadingScene(string sceneName, int loadingType, Action cb)
        {
            // if (Global.gApp.CurFightScene != null)
            // {
            //     Global.gApp.CurFightScene.Pause();
            // }

            IsLoadingScene = true;
            LoadingScene(sceneName, cb);
        }

        private void LoadingScene(string sceneName, Action cb)
        {
            LoadingCoroutine coroutine = Global.gApp.gKeepNode.AddComponent<LoadingCoroutine>();
            coroutine.LoadScene(sceneName, () => { LoadLoadingSceneCompeleted(cb); });
        }

        private void LoadLoadingSceneCompeleted(Action cb)
        {
            LoadingCoroutine coroutine = Global.gApp.gKeepNode.AddComponent<LoadingCoroutine>();
            coroutine.UnLoadLoadingScene(cb);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}