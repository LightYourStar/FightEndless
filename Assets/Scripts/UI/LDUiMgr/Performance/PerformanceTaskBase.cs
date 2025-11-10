namespace LD
{
    /// <summary>
    /// 拍脸任务接口：如果能打开，就执行并返回 true；否则返回 false。
    /// </summary>
    public interface IPerformanceTask
    {
        bool TryRun();
    }

    /// <summary>
    /// 拍脸任务基类,持有ui管理类对象 m_UIMgr
    /// </summary>
    public abstract class PerformanceTaskBase : IPerformanceTask
    {
        protected readonly LDUIMgr m_UIMgr;
        protected PerformanceTaskBase(LDUIMgr uiMgr)
        {
            m_UIMgr = uiMgr;
        }
        public abstract bool TryRun();
    }
}