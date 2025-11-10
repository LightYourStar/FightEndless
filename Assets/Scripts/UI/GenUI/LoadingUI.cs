using UnityEngine;
using UnityEngine.UI;

namespace LD {

	public partial class LoadingUI : LDBaseUI {

		[SerializeField]
		private RectTransform_Container m_Default;
		public RectTransform_Container Default { get { return m_Default; } }

		[SerializeField]
		private RectTransform_Image_Container m_maskBg;
		public RectTransform_Image_Container maskBg { get { return m_maskBg; } }

		[SerializeField]
		private RectTransform_Image_Container m_maskBg1;
		public RectTransform_Image_Container maskBg1 { get { return m_maskBg1; } }

		[SerializeField]
		private RectTransform_Text_Container m_txt_Tips;
		public RectTransform_Text_Container txt_Tips { get { return m_txt_Tips; } }

		[SerializeField]
		private RectTransform_Container m_Progress;
		public RectTransform_Container Progress { get { return m_Progress; } }

		[SerializeField]
		private RectTransform_Image_Container m_ProgressBarValue;
		public RectTransform_Image_Container ProgressBarValue { get { return m_ProgressBarValue; } }

		[SerializeField]
		private RectTransform_Text_Container m_txt_Progress;
		public RectTransform_Text_Container txt_Progress { get { return m_txt_Progress; } }

	}

}
