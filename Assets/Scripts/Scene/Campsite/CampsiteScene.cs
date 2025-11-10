using UnityEngine;

namespace LD
{
    public class CampsiteScene : BaseScene
    {
        public CampsiteScene()
        {
        }

        public override void Init()
        {
            base.Init();
            Debug.Log("CampsiteScene Init 11");
            Global.gApp.gUICameraCmpt.cullingMask = 1<<(5|1|1); //#todo 改成位与;
            Global.gApp.gUICameraCmpt.clearFlags = CameraClearFlags.Color;


            Global.gApp.gUiMgr.OpenUIAsync<CommonUI>(LDUICfg.CommonUI);

        }

        public override void OnDestroy()
        {
            Global.gApp.gUICameraCmpt.cullingMask = 1;
            Global.gApp.gResMgr.UnLoadCrossSceneAssets();
        }
    }
}