using UnityEngine;
using System.Collections.Generic;

namespace LD
{
    public partial class LDUIMgr
    {
        private List<LDBaseUI> m_OpenUIStack; // 当前所有的 打开的ui
        private Transform m_RootTsf;
        private Canvas m_TopCanvasTsf;

        private int m_LockRef = 0;
        // private LDSystemEnum m_MainSceneJumpSys = LDSystemEnum.None; // 打开主场景时要跳转到哪
        // private PageJumpInfo m_MainSceneJumpSysParam = null;
        private float m_DelUIPreTime = 0;

        private float m_AutoReleaseResTime = 5f;//自动卸载 计时器
        public LDUIMgr()
        {
            m_OpenUIStack = new List<LDBaseUI>(4);
            // gTimerMgr = new LDTimerMgr();
            CreateCanvasNode();

            InitPerformanceTasks();
        }
        public void OnDUpdate(float dt)
        {
            // if (gTimerMgr != null)
            // {
            //     gTimerMgr.OnSafeDUpdate(dt);
            // }
            // if (Global.gApp.CurScene is CampsiteScene)
            // {
            //     m_DelUIPreTime += dt;
            //     if (m_DelUIPreTime >= m_AutoReleaseResTime)
            //     {
            //         ReleaseUnUseUI();
            //     }
            // }
        }
        public void DelayReleaseUnUseUI()
        {
            // gTimerMgr.AddTimer(0.1f, 1,(a, b) =>
            //  {
            //      ReleaseUnUseUI();
            //  });

        }
        public void ReleaseUnUseUI()
        {
            m_DelUIPreTime = 0;
            foreach (KeyValuePair<string, WndUICfg> item in LDUICfg.gUIInfo)
            {
                if(!GetOpenPanelCompent(item.Key) && item.Value.AutoUnload)
                {
                    Global.gApp.gResMgr.TryReleaseUI(item.Value.ResPath);
                }
            }
        }
        public void CloseLoadingUI()
        {
            Global.gApp.gUiMgr.CloseUI(LDUICfg.LoadingUI);
        }


        public void OpenUIAsync(string panelName)
        {
            OpenUIAsync<LDBaseUI>(panelName);
        }
        public LDUIContent<T> OpenUIAsync<T>(string panelName) where T : LDBaseUI
        {
            Debug.Log(panelName);
            if (m_LockRef == 0)
            {
                WndUICfg uiInfo;
                if (LDUICfg.gUIInfo.TryGetValue(panelName, out uiInfo))
                {
                    LDUIContent<T> UIContent = new LDUIContent<T>();
                    UIContent.Init(panelName);
                    return UIContent;
                }
                else
                {
                    Debug.LogError("error ====== ui not exit ==== " + panelName);
                }
            }
            LDUIContent<T> UIContent2 = new LDUIContent<T>();
            return UIContent2;
        }
        public LDBaseUI OpenUI(string panelName)
        {
            Debug.Log(panelName);
            m_DelUIPreTime = 0;
            if (m_LockRef == 0)
            {
                CreateCanvasNode();
                CloseUI(panelName);
                WndUICfg uiInfo;
                if (LDUICfg.gUIInfo.TryGetValue(panelName, out uiInfo))
                {
                    return OpenUIImp(uiInfo, panelName);
                }
                else
                {
                    Debug.LogError("error ====== ui not exit ==== " + panelName);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private LDBaseUI CreateUI(WndUICfg uiInfo, string name)
        {
            GameObject uiPanel = Global.gApp.gResMgr.InstantiateLoadObj(uiInfo.ResPath, uiInfo.ResSceneType);
            LDBaseUI basePanel = uiPanel.GetComponent<LDBaseUI>();
            return basePanel;
        }
        private LDBaseUI OpenUIImp(WndUICfg uiInfo, string name)
        {
            LDBaseUI baseUi = CreateUI(uiInfo, name);
            Canvas[] canvas = baseUi.gameObject.GetComponentsInChildren<Canvas>();
            if (canvas.Length > 0)
            {
                baseUi.transform.SetParent(m_RootTsf, false);
                foreach (Canvas mCanvas in canvas)
                {
                    mCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                    mCanvas.worldCamera = Global.gApp.gUICameraCmpt;
                    if (!uiInfo.AutoOrder)
                    {
                        if (uiInfo.OrderInfo.ContainsKey(mCanvas.gameObject.name))
                        {
                            mCanvas.sortingOrder = uiInfo.OrderInfo[mCanvas.gameObject.name].OrderInLayer;
                            mCanvas.planeDistance = uiInfo.OrderInfo[mCanvas.gameObject.name].PlaneDistance;
                        }
                        else
                        {
                            Debug.LogError(" mCanvas.gameObject.name not cfg " + mCanvas.gameObject.name);
                        }
                    }
                    // GameAdapterUtils.AdaptCanvas(mCanvas);
                }

                if (uiInfo.AutoOrder)
                {
                    GetLastPanelMaxOrder(out int planeDistance, out int order);
                    foreach (Canvas mCanvas in canvas)
                    {
                        order += 5;
                        mCanvas.sortingOrder = order;

                        planeDistance -= 5;
                        planeDistance = Mathf.Max(5, planeDistance);// 不低于5
                        mCanvas.planeDistance = planeDistance;
                    }
                }
            }
            else
            {
                Debug.LogError(" UI has no Canvas  " + name);
                UIOrderInfo uIOrderInfo = uiInfo.OrderInfo[UIOrderInfo.MainCanvas];
                Canvas newCanvas = CreateCanvas(name, uIOrderInfo.OrderInLayer);
                newCanvas.planeDistance = uIOrderInfo.PlaneDistance;
                Transform canvasTsf = newCanvas.transform;
                baseUi.transform.SetParent(canvasTsf, false);
            }
            m_OpenUIStack.Add(baseUi);
            baseUi.Init(name, uiInfo);
            RefreshCommonUIOrder();
            return baseUi;
        }
        public void CloseUI(string panelName)
        {
            LDBaseUI baseUi = GetClosePanel(panelName);
            if (baseUi != null)
            {
                Debug.Log("CloseUI " + panelName);

                WndUICfg uiInfo = LDUICfg.gUIInfo[panelName];
                m_OpenUIStack.Remove(baseUi);
                CloseUiImp(uiInfo, baseUi);
                RefreshCommonUIOrder();

                // FightUI 不要release
                //if(uiInfo.CloseRelease && uiInfo.ResSceneType != ResSceneType.FightUI)
                //{
                //    Global.gApp.gResMgr.UnloadRes(uiInfo.ResSceneType, uiInfo.ResPath);
                //}
            }
        }
        private LDBaseUI GetClosePanel(string panelName)
        {
            LDBaseUI baseUi = GetOpenPanelCompent(panelName);
            return baseUi;
        }
        private void CreateCanvasNode()
        {
            if (m_RootTsf == null)
            {
                GameObject rootNode = new GameObject("UiRootNode");
                m_RootTsf = rootNode.transform;
                m_RootTsf.SetParent(Global.gApp.gKeepNode.transform, false);

                Canvas topCanva = CreateCanvas("TopCanvas", 300);
                topCanva.planeDistance = 50;
                m_TopCanvasTsf = topCanva;
            }
        }

        private Canvas CreateCanvas(string name, int order)
        {
            string path = LDUICfg.CanvasPath;
            GameObject canvasGo = Global.gApp.gResMgr.InstantiateLoadObj(path, ResSceneType.Resident);
            canvasGo.name = name;
            canvasGo.transform.SetParent(m_RootTsf, false);
            Canvas cvs = canvasGo.GetComponent<Canvas>();

            // GameAdapterUtils.AdaptCanvas(cvs);

            cvs.worldCamera = Global.gApp.gUICameraCmpt;
            cvs.planeDistance = 50;
            cvs.sortingOrder = order;
            return cvs;
        }
        public void CloseAllUI()
        {
            if (m_LockRef == 0)
            {
                m_LockRef++;
                List<LDBaseUI> openUIStack = new List<LDBaseUI>(m_OpenUIStack);
                foreach (LDBaseUI panel in openUIStack)
                {
                    panel?.TouchClose();
                }
                m_LockRef--;
            }
        }
        private void CloseUiImp(WndUICfg uIInfo, LDBaseUI baseUi)
        {
            baseUi.Release();
            m_DelUIPreTime = 0;
        }
        public void ClossAllNormalUI()
        {
            if (m_LockRef == 0)
            {
                m_LockRef++;
                List<LDBaseUI> openUIStack = new List<LDBaseUI>(m_OpenUIStack);
                foreach (LDBaseUI panel in openUIStack)
                {
                    WndUICfg uIInfo = panel.GetUiInfo();
                    if (uIInfo.ResSceneType == ResSceneType.FightUI || uIInfo.ResSceneType == ResSceneType.NormalUI)
                    {
                        panel?.TouchClose();
                    }
                }
                m_LockRef--;
            }
        }
        public void CloseAllUI(int lv)
        {
            if (m_LockRef == 0)
            {
                m_LockRef++;
                List<LDBaseUI> openStack = new List<LDBaseUI>(m_OpenUIStack);
                foreach (LDBaseUI panel in openStack)
                {
                    if (panel != null)
                    {
                        WndUICfg uIInfo = panel.GetUiInfo();
                        if (uIInfo.UILevel >= lv)
                        {
                            panel?.TouchClose();
                        }
                    }
                }
                m_LockRef--;
            }
        }
        // 从后往前拿 最快了
        public LDBaseUI GetOpenPanelCompent(string panelName)
        {
            for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            {
                if (panelName.Equals(m_OpenUIStack[i].GetName()))
                {
                    return m_OpenUIStack[i];
                }
            }
            return null;
        }
        // 有展示UI在打开状态
        public bool PerformanceUIOpened()
        {
            for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            {
                if (LDUICfg.PerformanceUIs.Contains(m_OpenUIStack[i].GetName()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 有展示的功能UI打开状态
        /// </summary>
        /// <returns></returns>
        public bool PerformanceFunctionUIOpened()
        {
            for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            {
                if (LDUICfg.PerformaceFunctionUIs.Contains(m_OpenUIStack[i].GetName()))
                {
                    return true;
                }
            }

            return false;
        }

        public Transform GetTopCanvasTsf()
        {
            return m_TopCanvasTsf.transform;
        }
        public Transform GetRootNodeTsf()
        {
            return m_RootTsf;
        }
        public void GetLastPanelMaxOrder(out int planeDistance, out int order)
        {
            planeDistance = 100;
            order = 30;

            LDBaseUI lastUI = GetTopUI();
            if (lastUI != null)
            {
                lastUI.GetMaxOrder(out planeDistance, out order);
            }

            RefreshCommonUIOrder();
            // CommonUI commonUI = Global.gApp.gUiMgr.GetOpenPanelCompent(LDUICfg.CommonUI) as CommonUI;
            // if (commonUI != null)
            // {
            //     commonUI.GetMaxOrder(out int commonDistance, out int commonOrder);
            //     order = Mathf.Max(order, commonOrder);
            //     planeDistance = Mathf.Min(planeDistance, commonDistance);
            //     planeDistance = Mathf.Max(5, planeDistance);
            // }
        }

        public LDBaseUI GetTopUI()
        {
            for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            {
                LDBaseUI lastUI = m_OpenUIStack[i];
                if (!LDUICfg.IgnoreTopUIs.Contains(lastUI.GetName()))
                {
                    return lastUI;
                }
            }
            return null;
        }

        public LDBaseUI GetGuideTopUI()
        {
            for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            {
                LDBaseUI lastUI = m_OpenUIStack[i];
                if (!LDUICfg.IgnoreGuideTopUIs.Contains(lastUI.GetName()))
                {
                    return lastUI;
                }
            }
            return null;
        }

        public void RefreshCommonUIOrder()
        {
            // for (int i = m_OpenUIStack.Count - 1; i >= 0; i--)
            // {
            //     LDBaseUI lastUI = m_OpenUIStack[i];
            //     if (lastUI.TryShowTabUI())
            //     {
            //         break;
            //     }
            // }
        }

        public void SetTabUI(int tab, int order, int dis)
        {
            // CommonUI commonUI = Global.gApp.gUiMgr.GetOpenPanelCompent(LDUICfg.CommonUI) as CommonUI;
            // if (commonUI != null)
            // {
            //     commonUI.ShowCurrencyTab(tab, order, dis);
            // }
        }

        public void HideTabUI()
        {
            // CommonUI commonUI = Global.gApp.gUiMgr.GetOpenPanelCompent(LDUICfg.CommonUI) as CommonUI;
            // if (commonUI != null)
            // {
            //     commonUI.HideAllCurrencyTab();
            // }
        }

        public void SetMenuTab(int order, int dis)
        {
            // CommonUI commonUI = Global.gApp.gUiMgr.GetOpenPanelCompent(LDUICfg.CommonUI) as CommonUI;
            // if (commonUI != null)
            // {
            //     commonUI.ShowMenuTab(order, dis);
            // }
        }

        private void CacheUI(string uiName,bool open)
        {
            if (open)
            {
                LDBaseUI baseUI = Global.gApp.gUiMgr.OpenUI(uiName) ;
                if(baseUI != null)
                {
                    baseUI.transform.localScale = Vector3.zero;
                    baseUI.SetPreloadTrue();
                }
            }
            else
            {
                Global.gApp.gUiMgr.CloseUI(uiName);
            }

        }
        //
        // public void SetMainSceneJumpSys(LDSystemEnum sys, PageJumpInfo jumpInfo = null)
        // {
        //     m_MainSceneJumpSys = sys;
        //     m_MainSceneJumpSysParam = jumpInfo;
        // }
        //
        // public LDSystemEnum GetMainSceneJumpSys()
        // {
        //     LDSystemEnum sys = m_MainSceneJumpSys;
        //     m_MainSceneJumpSys = LDSystemEnum.None;
        //     return sys;
        // }
        //
        // public PageJumpInfo GetMainSceneJumpSysInfo()
        // {
        //     return m_MainSceneJumpSysParam;
        // }

        // 刷新UI层级   切记  避免循环调用
        // 目前只给新手引导用
        public void TryRefreshUIOrder(string uiName)
        {
            LDBaseUI baseUI = GetOpenPanelCompent(uiName);
            if (baseUI == null)
            {
                return;
            }

            if (!baseUI.GetUiInfo().AutoOrder)
            {
                return;
            }

            Canvas[] canvas = baseUI.gameObject.GetComponentsInChildren<Canvas>();
            if (canvas.Length > 0)
            {
                GetLastPanelMaxOrder(out int planeDistance, out int order);
                foreach (Canvas mCanvas in canvas)
                {
                    order += 5;
                    mCanvas.sortingOrder = order;

                    planeDistance -= 5;
                    planeDistance = Mathf.Max(5, planeDistance);// 不低于5
                    mCanvas.planeDistance = planeDistance;
                }
            }
        }
    }
}