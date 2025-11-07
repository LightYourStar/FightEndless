using LD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMask : LDBaseResUI {

    // Use this for initialization
    public GameObject BlackMask;
    private int m_Ref = 0;
    private string m_PanelName;
    private LDBaseUI m_Panel;
    private Action m_Action;
    private RenderTexture m_RenderTex;
    private float m_DelayCloseTime;
    public void Awake()
    {
        m_PanelName = null;
        m_Panel = null;
        m_Action = null;
        SetMaskEnable(false);
    }
    public void SetMaskEnable(bool maskEnable)
    {
        if (BlackMask != null)
        {
            BlackMask.SetActive(maskEnable);
        }
    }
    public void SetGuassState(LDBaseUI baseUI,bool guassState)
    {
        // BlackMask.GetComponent<RawImage>().SetGaussian(guassState);
        // if (guassState)
        // {
        //     if (m_RenderTex == null)
        //     {
        //         BlackMask.gameObject.SetActive(true);
        //         m_RenderTex = UiTools.CreatureTexture(2);
        //         BlackMask.GetComponent<RawImage>().texture = m_RenderTex;
        //         BlackMask.GetComponent<RawImage>().color = Color.white;
        //         Global.gApp.gUiMgr.SetGaussBg(baseUI, m_RenderTex, true);
        //     }
        // }
    }
    private void Update()
    {
        m_DelayCloseTime -= Time.deltaTime;
    }
    public void AddCloseListener(string name,float closeTime,LDBaseUI panel)
    {
        m_DelayCloseTime = closeTime;
        m_PanelName = name;
        m_Panel = panel;
        AddBtnListener();
    }
    private void ClickCallBack()
    {
        if(m_DelayCloseTime > 0)
        {
            return;
        }
        if(m_PanelName != null)
        {
            if (m_Panel)
            {
                m_Panel.TouchClose();
            }
            else
            {
                Global.gApp.gUiMgr.CloseUI(m_PanelName);
            }
        }
        else if(m_Action != null)
        {
            m_Action();
        }
    }

    private void AddBtnListener()
    {
        Button btn = transform.GetComponent<Button>();
        // btn.AddListenerNoAudio(ClickCallBack);
    }
    public void AddRef()
    {
        m_Ref = m_Ref + 1;
        if(m_Ref == 1)
        {
            gameObject.SetActive(true);
        }
    }
    public void ClearRef()
    {
        m_Ref = 0;
        gameObject.SetActive(false);
    }
    public void RemoveRef()
    {
        m_Ref = m_Ref - 1;
        m_Ref = Mathf.Max(m_Ref,0);
        if(m_Ref == 0)
        {
            gameObject.SetActive(false);
        }
    }
    protected override void OnDestroy()
    {
        if(m_RenderTex != null)
        {
            // Global.gApp.gUiMgr.SetGaussBg(null, null, false);
            // UiTools.ReleaseRenderTexture(m_RenderTex);
            m_RenderTex = null;
        }
        base.OnDestroy();
    }
}