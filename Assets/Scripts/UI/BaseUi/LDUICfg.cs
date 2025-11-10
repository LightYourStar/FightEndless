using System.Collections.Generic;

namespace LD
{

    public class UIOrderInfo
    {
        public static string MainCanvas = "RootCanvas";
        public static string Canvas1 = "Canvas1";
        public static string Canvas2 = "Canvas2";
        public static string Canvas3 = "Canvas3";
        public static string Canvas4 = "Canvas4";
        public static string Canvas5 = "Canvas5";
        public static List<string> CanvasList = new List<string>() { Canvas5, Canvas4, Canvas3, Canvas2, Canvas1, MainCanvas };
        public int PlaneDistance = -1;
        public int OrderInLayer = -1;
    }

    public class WndUICfg
    {
        //资源路径
        public string ResPath;
        // 点击 特效。暂时没用
        public bool NoTouchEffect = false;
        // 是否创建 屏蔽层。战斗ui 就没有屏蔽层
        public bool NoTouchMask = false;
        // 半透明遮罩
        public bool MaskBlack = false;
        // 告诉模糊
        public bool GaussMask = false;
        // 废弃 用UILV 更准确
        public bool HalfUi = false;
        // 响应空白区域 关闭 与NoTouchMask 搭配使用
        public bool TouchEmptyClose = false;
        public float TouchEmptyCloseDelayTime = 0;
        //ui 层级关系 一般都是2级界面
        public int UILevel = 2;
        // 是否不响应 返回键 事件
        public bool UnRespondEscape = true;
        //
        public bool UnLoadRes = false;
        public ResSceneType ResSceneType = ResSceneType.NormalUI;
        public bool CloseRelease = true;
        public bool AutoOrder = true; // 自动层级
        public int ShowCurrencyTab = 0; // 展示顶部资源栏类型
        public bool ShowMenuTab = false; // 展示底部大页签
        public string AudioClip = string.Empty;// 打开ui 的时候播放的声音
        public string AudioBgm = string.Empty;// 打开ui 的时候播放的背景音乐
        public float AudiuClipDelayTime = 0;// 打开ui 的时候播放的声音
        public Dictionary<string, UIOrderInfo> OrderInfo = new Dictionary<string, UIOrderInfo>();
        public List<string> OnUICloseListener = null;
        public bool AutoUnload = true; // 每隔3秒尝试卸载为打开的UI
        public WndUICfg()
        {
            OrderInfo[UIOrderInfo.MainCanvas] = new UIOrderInfo() { PlaneDistance = 100, OrderInLayer = 30 };
        }
        public int GeMaxOrder()
        {
            foreach (string canvasName in UIOrderInfo.CanvasList)
            {
                if (OrderInfo.ContainsKey(canvasName))
                {
                    return OrderInfo[canvasName].OrderInLayer;
                }
            }
            return -1;
        }
    }

    public class LDUICfg
    {
        public static string CanvasPath = "PrefabsN/UI/Resident/Canvas";

        public static string LogoUI = "LogoUI";
        public static string LoadingUI = "LoadingUI";


        // 用于各种表现的UI 各种获得UI  升级  解锁 平级  互斥
        public static HashSet<string> PerformanceUIs = new HashSet<string>
        {
        };

        // 主动弹窗功能互斥
        public static HashSet<string> PerformaceFunctionUIs = new HashSet<string>()
        {
        };

        // 不算顶层UI
        public static HashSet<string> IgnoreTopUIs = new HashSet<string>
        {
            LoadingUI,
        };

        // 不算新手引导顶层UI
        public static HashSet<string> IgnoreGuideTopUIs = new HashSet<string>
        {
        };

        // 在这些UI打开的时候 阻止战力UI弹出
        public static HashSet<string> PreventPowerUIs = new HashSet<string>
        {
        };

        // 关闭时不广播UIClose
        public static HashSet<string> IgnoreCloseBroadcastUIs = new HashSet<string>
        {
            LoadingUI,
        };

        public static Dictionary<string, WndUICfg> gUIInfo = new Dictionary<string, WndUICfg>()
        {
            {LogoUI,new WndUICfg(){ResPath = "PrefabsN/UI/Logo/LogoUi",UnRespondEscape = true}},
            {LoadingUI,new WndUICfg(){ResPath = "PrefabsN/UI/Resident/LoadingUI",AutoOrder = false,ResSceneType = ResSceneType.Resident}},
        };

        static LDUICfg()
        {
            gUIInfo[LoadingUI].OrderInfo[UIOrderInfo.Canvas1] = new UIOrderInfo() { PlaneDistance = 10, OrderInLayer = 90 };
        }
    }
}