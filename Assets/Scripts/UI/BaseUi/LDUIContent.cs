using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class LDUIContent<T> where T :LDBaseUI
    {
        public string PanelName;//ui名字
        public WndUICfg WndUICfg;//ui配置文件
        public Action<T> OnComplete;//界面打开
        public bool IsComplete;//是否完成
        public T BaseUI;

        public void Init(string panelName)
        {
            PanelName = panelName;
            if (!LDUICfg.gUIInfo.TryGetValue(panelName, out WndUICfg))
            {
                Debug.LogError("error ====== ui not exit ==== " + panelName);
                return;
            }
            BaseUI = Global.gApp.gUiMgr.OpenUI(panelName) as T;
            if (BaseUI == null)
            {
                Global.LogError($"panelName = {panelName}  BaseUI == Null !!!!!!!!!!!!!!!!!!!!!!");
            }
            LoadedComplete();
        }
        //加载完成
        public void LoadedComplete()
        {
            IsComplete = true;
            OnUILoadedComplite();
        }
        public void SetLoadedCall(Action<T> action)
        {
            OnComplete = action;
            OnUILoadedComplite();
        }
        private void OnUILoadedComplite()
        {
            if (IsComplete)
            {
                if (BaseUI != null)
                {
                    OnComplete?.Invoke(BaseUI);
                }
                OnComplete = null;
            }
        }

        public void AddExternPreloadList(List<string> preloadRes)
        {
            if (BaseUI != null)
            {
                (BaseUI as LDBaseUI)?.OnExternResLoaded();
            }
        }
        //判断是否加载完成
        public bool IsLoaded()
        {
            return true;
        }
    }
}