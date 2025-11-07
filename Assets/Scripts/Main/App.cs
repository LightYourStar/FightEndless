using System.Collections;
using UnityEngine;

namespace LD
{
    public class App
    {
        public ResMgr gResMgr;
        public GameCtrl gGameCtrl;
        public LDUiMgr gUiMgr;
        public MsgDispatcher gMsgDispatcher;
        private Global m_Global;

        public Global gGlobal;
        public GameObject gUICamera;
        public Camera gUICameraCmpt;
        public Camera gTopUICameraCmpt;
        public GameObject gKeepNode;
        public GameObject gRoleNode;
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
            gUICamera = keepNode.transform.Find("CameraNode/UICamera").gameObject;
            gUICameraCmpt = gUICamera.GetComponent<Camera>();
            gTopUICameraCmpt = keepNode.transform.Find("CameraNode/UICamera2").GetComponent<Camera>();
            gTopUICameraCmpt.gameObject.SetActive(false);

            gLightCompt = keepNode.transform.Find("DireLight").GetComponent<Light>();
        }

        private IEnumerator PreInit()
        {
            Global.Log("PreInit ====111");
            yield return new WaitForEndOfFrame();
            gMsgDispatcher = new MsgDispatcher();
            gUiMgr = new LDUiMgr();
            gGameCtrl = new GameCtrl();

            InitData();
            Global.gApp.gUiMgr.OpenUIAsync(LDUICfg.LogoUI);
            Global.gApp.gGameCtrl.ChangeToLoginScene();
        }

        public void DUpdate(float dt)
        {
            if (m_InitSucess)
            {
                if (gGameCtrl != null)
                {
                    gGameCtrl.DUpdate(dt);
                }
            }

            if (gUiMgr != null)
            {
                gUiMgr.OnDUpdate(dt);
            }
        }

        public void OnDestroy()
        {
            gResMgr.OnDestroy();
        }
    }
}