using System.Collections.Generic;

namespace LD
{
    public partial class LDUiMgr
    {
        private List<PerformanceTaskBase> m_PerformanceTasks;
        private void InitPerformanceTasks()
        {
            m_PerformanceTasks = new List<PerformanceTaskBase>
            {
            };
        }

        public T GetPerformanceTask<T>() where T:PerformanceTaskBase
        {
            foreach (PerformanceTaskBase task in m_PerformanceTasks)
            {
                if (task is T tTask)
                {
                    return tTask;
                }
            }

            return null;
        }
    }
}