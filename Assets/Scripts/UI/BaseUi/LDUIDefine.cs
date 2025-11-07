using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LD
{
    [System.Serializable]
    public class RectTransform_Animator_Button_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_Button_Animator_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_Animator_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        public void SetActive(bool val)
        {
            m_GameObject.SetActive(val);
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_Text_Animator_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Text m_text;

        public Text text
        {
            get { return m_text; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        public void SetActive(bool val)
        {
            m_GameObject.SetActive(val);
        }
    }

    [System.Serializable]
    public class Transform_Animator_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private Transform m_transform;

        public Transform transform
        {
            get { return m_transform; }
        }

        public void SetActive(bool val)
        {
            m_GameObject.SetActive(val);
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class Transform_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private Transform m_transform;

        public Transform transform
        {
            get { return m_transform; }
        }

        public void SetActive(bool val)
        {
            m_GameObject.SetActive(val);
        }
    }

    [System.Serializable]
    public class RectTransform_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }

    [System.Serializable]
    public class RectTransform_Animator_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_Animator_RawImage_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private RawImage m_rawImage;

        public RawImage RawImage
        {
            get { return m_rawImage; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }
    }

    [System.Serializable]
    public class RectTransform_RawImage_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private RawImage m_rawImage;

        public RawImage rawImage
        {
            get { return m_rawImage; }
        }
    }

    [System.Serializable]
    public class RectTransform_Button_RawImage_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }

        [SerializeField] private RawImage m_rawImage;

        public RawImage rawImage
        {
            get { return m_rawImage; }
        }
    }

    [System.Serializable]
    public class RectTransform_Text_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Text m_text;

        public Text text
        {
            get { return m_text; }
        }
    }

    [System.Serializable]
    public class RectTransform_Slider_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Slider m_slider;

        public Slider slider
        {
            get { return m_slider; }
        }
    }

    [System.Serializable]
    public class RectTransform_Slider_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Slider m_slider;

        public Slider slider
        {
            get { return m_slider; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }


    [System.Serializable]
    public class RectTransform_TextMeshProUGUI_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private TMPro.TextMeshProUGUI m_textMeshProUGUI;

        public TMPro.TextMeshProUGUI text
        {
            get { return m_textMeshProUGUI; }
        }
    }

    [System.Serializable]
    public class RectTransform_Button_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }


    [System.Serializable]
    public class RectTransform_Text_Button_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }

        [SerializeField] private Text m_text;

        public Text text
        {
            get { return m_text; }
        }
    }


    [System.Serializable]
    public class RectTransform_Scrollbar_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }

        [SerializeField] private Scrollbar m_scrollbar;

        public Scrollbar scrollBar
        {
            get { return m_scrollbar; }
        }
    }

    [System.Serializable]
    public class RectTransform_Button_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }
    }

    [System.Serializable]
    public class RectTransform_UIParticle_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        //[SerializeField]
        //private UIParticleSystem m_UIParticleSystem;
        //public UIParticleSystem UIParticleSystem { get { return m_UIParticleSystem; } }
    }

    [System.Serializable]
    public class RectTransform_Image_TMP_InputField_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private TMPro.TMP_InputField m_tMP_InputField;

        public TMPro.TMP_InputField inputField
        {
            get { return m_tMP_InputField; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }

    [System.Serializable]
    public class RectTransform_TMP_InputField_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private TMPro.TMP_InputField m_tMP_InputField;

        public TMPro.TMP_InputField inputField
        {
            get { return m_tMP_InputField; }
        }
    }

    [System.Serializable]
    public class RectTransform_InputField_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private InputField m_inputField;

        public InputField inputField
        {
            get { return m_inputField; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }

    [System.Serializable]
    public class RectTransform_Toggle_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Toggle m_toggle;

        public Toggle toggle
        {
            get { return m_toggle; }
        }
    }

    [System.Serializable]
    public class RectTransform_Toggle_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Toggle m_toggle;

        public Toggle toggle
        {
            get { return m_toggle; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }
    }

    [System.Serializable]
    public class RectTransform_Button_Animator_Image_Container
    {
        [SerializeField] private GameObject m_GameObject;

        public GameObject gameObject
        {
            get { return m_GameObject; }
        }

        [SerializeField] private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        [SerializeField] private Image m_image;

        public Image image
        {
            get { return m_image; }
        }

        [SerializeField] private Animator m_animator;

        public Animator animator
        {
            get { return m_animator; }
        }

        [SerializeField] private Button m_button;

        public Button button
        {
            get { return m_button; }
        }
    }
}