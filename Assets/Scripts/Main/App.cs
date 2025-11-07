using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public partial class App
    {
        // public GameDatas gGameData;
        public ResMgr gResMgr;
        public GameCtrl gGameCtrl;
        public LDUiMgr gUiMgr;
        // public LDSystemMgr gSystemMgr;
        // public GameAdapterUtils gGameAdapter;

        private Global m_Global;

        public Global gGlobal;
        public GameObject gUICamera;
        public Camera gUICameraCmpt;
        public Camera gTopUICameraCmpt;
        public GameObject gKeepNode;
        public GameObject gRoleNode;
        // public AutoCam gCamCompt;
        // public ShakecameraControl gShakeCompt;
        public Light gLightCompt;

        private bool m_InitSucess = false;

        public void Awake(Global global, GameObject keepNode)
        {
            m_Global = global;
            Debug.Log("App Awake ");
            InitNode(keepNode);


#if USE_ADDRESSABLES // 加载方式Addressable 优先
            gResMgr = new ResBundleMgr();
#elif USE_UPDATE // 定义更新 的加载方式
            gResMgr = new ResMobileMgr();
#else // 否则 走editor
            gResMgr = new ResEditorMgr();
#endif

            gResMgr.PreLoadAssets(InitApp);
        }

        public void InitApp()
        {
            m_Global.StartCoroutine(PreInit());
        }

        public void InitData()
        {
            m_InitSucess = true;
            Debug.Log("gSystemMgr AfterInit");
            // 初始化数据
            // gSystemMgr.AfterInit();

        }

        private void InitNode(GameObject keepNode)
        {
            gKeepNode = keepNode;
            gRoleNode = keepNode.transform.Find("RoleNode").gameObject;
            gGlobal = keepNode.transform.Find("Global").GetComponent<Global>();
            // gCamCompt = keepNode.transform.Find("CameraNode/ShakeNode/CameraCtrlNode").GetComponent<AutoCam>();
            // gShakeCompt = keepNode.transform.Find("CameraNode/ShakeNode").GetComponent<ShakecameraControl>();
            gUICamera = keepNode.transform.Find("CameraNode/UICamera").gameObject;
            gUICameraCmpt = gUICamera.GetComponent<Camera>();
            gTopUICameraCmpt = keepNode.transform.Find("CameraNode/UICamera2").GetComponent<Camera>();
            gTopUICameraCmpt.gameObject.SetActive(false);

            // gMapNode = keepNode.transform.Find("MapNode").gameObject;
            // gBulletNode = keepNode.transform.Find("BulletNode").gameObject;
            // gAudioSource = keepNode.transform.Find("Global").GetComponent<AudioSourceController>();
            gLightCompt = keepNode.transform.Find("DireLight").GetComponent<Light>();
        }

        private IEnumerator PreInit()
        {
            Global.Log("PreInit ====111");
            // gGameData = new GameDatas();
            yield return new WaitForEndOfFrame();
            // yield return gGameData.WaitOnInitSucceed();
            // gMsgDispatcher = new MsgDispatcher();
            // gToastMgr = new LDToastMgr();
            gUiMgr = new LDUiMgr();
            // gGameAdapter = new GameAdapterUtils();
            gGameCtrl = new GameCtrl();

            // gNetMgr = new LDNetMgr();
            // gSystemMgr = new LDSystemMgr();

            InitData();
            Global.gApp.gUiMgr.OpenUIAsync(LDUICfg.LogoUI);
            Global.gApp.gGameCtrl.ChangeToLoginScene();
        }

        public void DUpdate(float dt)
        {
            if (m_InitSucess)
            {
                // if (gGameAdapter != null)
                // {
                //     gGameAdapter.DUpdate();
                // }
                // gNetMgr.OnDUpdate(dt);

                if (gGameCtrl != null)
                {
                    gGameCtrl.DUpdate(dt);
                    // gCamCompt.OnDUpdate(dt);
                }
                // gSystemMgr.OnDUpdate(dt);
            }

            if (gUiMgr != null)
            {
                gUiMgr.OnDUpdate(dt);
            }
        }

        public void OnDestroy()
        {
            // gGameData = null;
            // gNetlktroy();
            gResMgr.OnDestroy();
            // if(Global.gApp.CurFightScene != null)
            // {
            //     Global.gApp.CurFightScene.Pause();
            //     Global.gApp.CurFightScene.gKCPNetMgr.CloseKcp();
            // }
        }

    }
}