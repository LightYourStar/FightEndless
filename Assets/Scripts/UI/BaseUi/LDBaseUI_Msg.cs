// using LD.Protocol;
using System.Collections.Generic;

namespace LD
{
    /// <summary>
    /// 刷新方法可以扩展
    /// </summary>
    public abstract partial class LDBaseUI
    {
        public abstract void OnFreshUI();
        //public virtual void OnAdCallBack(bool result,int param)
        //{

        //}
        public virtual void OnFreshUI(int val)
        {

        }
        public virtual void OnFreshUI(string val)
        {

        }
        public virtual void OnFreshUI(LDUIDataBase val)
        {

        }
        // public virtual void OnFreshUI(opcode opcode)
        // {
        //
        // }
        // public virtual void OnFreshUI(opcode opcode, int val)
        // {
        //
        // }
        // public virtual void OnFreshUI(opcode opcode,string val)
        // {
        //
        // }
        public virtual void OnExternResLoaded()
        {

        }
        public void AddExternRes(List<string> preloadRes)
        {
            OnExternResLoaded();
        }
    }
}