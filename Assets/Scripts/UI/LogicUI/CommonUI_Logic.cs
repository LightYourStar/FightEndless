using UnityEngine;

namespace LD
{
	public partial class CommonUI
	{
        protected override void OnInitImp()
        {
            m_Btn_Start.AddListener(OnStartClick);
        }

        protected override void OnCloseImp()
        {
        }

        public override void OnFreshUI()
        {
        }

        private void OnStartClick()
        {
        }
    }
}