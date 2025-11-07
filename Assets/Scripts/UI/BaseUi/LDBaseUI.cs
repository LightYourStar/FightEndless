using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class LDUIDataBase
    {
    }

    public partial class LDBaseUI : LDBaseResUI
    {
        private WndUICfg m_UiInfo;
        protected string m_UIName = null;
        private bool m_TouchMaskFlag = false;
        private bool m_Inited = false;
        private TouchMask m_TouchMask;
        private List<int> m_TimerIds = new List<int>();
        private List<int> m_FrameTimerIds = new List<int>();
        private int m_CurrencyTab;
        private bool m_Preload = false;
        private double EnterTime;


        public void Init(string name, WndUICfg info)
        {
            m_UIName = name;
            m_UiInfo = info;
            TryInitOnce();
            AddMsgListener();
            StartCoroutine(EndOfFrame());

            OnInitImp();
        }

        /// <summary>
        /// 初始化工作
        /// 按钮监听  Item创建  等
        /// </summary>
        protected abstract void OnInitImp();

        /// <summary>
        /// UI关闭回调
        /// </summary>
        protected abstract void OnCloseImp();
        public void SetPreloadTrue()
        {
            m_Preload = true;
        }

        IEnumerator EndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            OnEndOfFrameCall();

            if (!m_UiInfo.MaskBlack)
            {
                if (m_TouchMask != null)
                {
                    m_TouchMask.SetGuassState(this, m_UiInfo.GaussMask);
                }
            }
        }

        public virtual void OnEndOfFrameCall()
        {
        }

        private void TryInitOnce()
        {
            if (!m_Inited)
            {
                CreateTouchMask();
                InitOnceInfo();
            }
        }

        private void InitOnceInfo()
        {
            m_Inited = true;
        }

        private void CreateTouchMask()
        {
        }


        protected void CloseUI()
        {
            RemoveMsgListener();
            RemoveTouchMask();
            RemoveTimeTouchMask();
            if (!LDUICfg.IgnoreCloseBroadcastUIs.Contains(GetName()))
            {
                Global.gApp.gMsgDispatcher.Broadcast<string>(MsgIds.OnCloseUI, m_UIName);
            }
            OnCloseImp();
        }

        public void Release()
        {
            CloseUI();
            // release Content
            Global.gApp.gResMgr.DestroyGameObj(GetRootNode());
        }

        public GameObject GetRootNode()
        {
            GameObject rootNode = null;
            ;
            if (m_UiInfo != null && GetComponentInChildren<Canvas>() == null)
            {
                rootNode = gameObject.transform.parent.gameObject;
            }
            else
            {
                rootNode = gameObject;
            }

            return rootNode;
        }

        public virtual void TouchClose()
        {
            Global.gApp.gUiMgr.CloseUI(m_UIName);
        }

        public TouchMask GetTouchMask()
        {
            return m_TouchMask;
        }

        public string GetName()
        {
            return m_UIName;
        }

        public WndUICfg GetUiInfo()
        {
            return m_UiInfo;
        }

        private void AddMsgListener()
        {
            RemoveMsgListener();
            RegEvent(true);
        }

        private void RemoveMsgListener()
        {
            RegEvent(false);
        }

        private void OnCloseOtherUI(string uiName)
        {
            if (m_UiInfo.OnUICloseListener.Count == 0 || m_UiInfo.OnUICloseListener.Contains(uiName))
            {
                OnOtherUICloseFresh(uiName);
            }
        }

        protected virtual void OnOtherUICloseFresh(string uiName)
        {
        }

        protected void RegEvent(bool addListener)
        {
            if (m_UiInfo.OnUICloseListener != null)
            {
                Global.gApp.gMsgDispatcher.RegEvent<string>(MsgIds.OnCloseUI, OnCloseOtherUI, addListener);
            }

            RegEventImp(addListener);
        }

        protected virtual void RegEventImp(bool addListener)
        {
        }

        /// <summary>
        /// //跨天
        /// </summary>
        protected virtual void OnCrossDay()
        {
            Global.LogEditor("跨天了");
        }

        public void RemoveTouchMask()
        {
            if (m_TouchMaskFlag)
            {
                m_TouchMaskFlag = false;
                Global.gApp.gGameCtrl.RemoveGlobalTouchMask();
            }
        }

        public void AddTouchMask()
        {
            if (!m_TouchMaskFlag)
            {
                m_TouchMaskFlag = true;
                Global.gApp.gGameCtrl.AddGlobalTouchMask();
            }
        }

        private GameObject m_TouchGo;

        protected void AddTimeTouchMask(float time)
        {
            // m_TouchGo = Global.gApp.gResMgr.InstantiateLoadObj(LDUICfg.TouchMaskUi, ResSceneType.Resident);
            // m_TouchGo.transform.SetParent(Global.gApp.gUiMgr.GetTopCanvasTsf(), false);
            // m_TouchGo.transform.SetAsFirstSibling();
            // GameObject.Destroy(m_TouchGo, time);
        }

        protected void RemoveTimeTouchMask()
        {
            if (m_TouchGo != null)
            {
                Global.gApp.gResMgr.DestroyGameObj(m_TouchGo);
                m_TouchGo = null;
            }
        }


        public void GetMaxOrder(out int planeDistance, out int order)
        {
            planeDistance = 100;
            order = 30;

            Canvas[] canvas = GetRootNode().GetComponentsInChildren<Canvas>();
            int minPlaneDistance = 500;
            int maxOrder = -500;
            foreach (Canvas _cvs in canvas)
            {
                if (_cvs.planeDistance < minPlaneDistance)
                {
                    minPlaneDistance = (int)_cvs.planeDistance;
                }

                if (_cvs.sortingOrder > maxOrder)
                {
                    maxOrder = _cvs.sortingOrder;
                }
            }

            if ((gameObject.layer) != 0)
            {
                ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
                Renderer renderer;
                foreach (ParticleSystem _ps in particleSystems)
                {
                    if (_ps.TryGetComponent(out renderer))
                    {
                        if (renderer.sortingOrder > maxOrder)
                        {
                            maxOrder = renderer.sortingOrder;
                        }
                    }
                }
            }

            planeDistance = Mathf.Min(planeDistance, minPlaneDistance);
            planeDistance = Mathf.Max(5, planeDistance); // 不低于5
            order = Mathf.Max(order, maxOrder);
        }

        public void GetNextOrder(out int planeDistance, out int order)
        {
            GetMaxOrder(out planeDistance, out order);
            order += 5;
            planeDistance -= 5;
            planeDistance = Mathf.Max(5, planeDistance); // 不低于5
        }


    }
}