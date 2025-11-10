using System;
using UnityEngine;

namespace LD
{
    public partial class LoadingUI
    {
        private float m_CurValue = 0;
        private float m_DistanceValue = 0.99f;

        protected override void OnInitImp()
        {
            txt_Tips.gameObject.SetActive(false);
            int randomVal = UnityEngine.Random.Range(0, 10000);
            m_maskBg.gameObject.SetActive(randomVal % 2 == 0);
            m_maskBg1.gameObject.SetActive(randomVal % 2 == 1);

            // Global.gApp.gSystemMgr.gMarqueeTipsMgr.TryCloseMarquee();
        }

        public override void OnFreshUI()
        {
        }

        public void RefreshUI(int type, Action cb)
        {

            cb?.Invoke();

            m_Default.gameObject.SetActive(type == 0);

            // 预留
            txt_Tips.gameObject.SetActive(false);

            RefreshProgressText();
        }

        protected override void OnCloseImp()
        {
            // Global.gApp.gSystemMgr.gMarqueeTipsMgr.TryShowMarquee();
        }

        protected override void RegEventImp(bool addListener)
        {
            base.RegEventImp(addListener);
            Global.gApp.gMsgDispatcher.RegEvent<float>(MsgIds.LoadingPercentValue, NotifyProgressChange, addListener);
        }

        private void NotifyProgressChange(float value)
        {
            if (value > m_DistanceValue)
                m_DistanceValue = value;
        }

        private void Update()
        {
            if (m_CurValue >= m_DistanceValue || m_CurValue >= 0.99)
            {
                return;
            }

            m_CurValue += (m_DistanceValue - m_CurValue) / 5;
            RefreshProgressText();
        }

        private void RefreshProgressText()
        {
            m_ProgressBarValue.image.fillAmount = m_CurValue;
            // m_txt_Progress.text.SetTips(91181, Mathf.Floor(m_CurValue * 100));
        }
    }
}