using UnityEngine;
using UnityEngine.UI;

namespace LD {

	public partial class CommonUI : LDBaseUI {

		[SerializeField]
		private RectTransform_Container m_Canvas1;
		public RectTransform_Container Canvas1 { get { return m_Canvas1; } }

		[SerializeField]
		private RectTransform_Button_Image_Container m_Btn_Start;
		public RectTransform_Button_Image_Container Btn_Start { get { return m_Btn_Start; } }

	}

}
