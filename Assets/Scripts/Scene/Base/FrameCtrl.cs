namespace LD
{
    public class FrameCtrl
    {
        protected BaseScene m_Scene;
        public float m_WorldTime = 0;
        public FrameCtrl(BaseScene scene)
        {
            m_Scene = scene;
        }
        public virtual void Init()
        {
            m_Scene.Init();
        }
        public BaseScene GetScene()
        {
            return m_Scene;
        }
        public float GetWroldTime()
        {
            return m_WorldTime;
        }

        public virtual void OnDestroy()
        {
            m_Scene.OnDestroy();
        }
        public virtual void Update(float dt)
        {
            if (m_WorldTime > 0)
            {
                m_WorldTime = m_WorldTime + dt;
                m_Scene.Update(dt);
            }
            else
            {
                m_WorldTime = m_WorldTime + dt;
            }
        }
    }
}
