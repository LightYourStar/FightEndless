using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{
    public class LDUIDependItem
    {
        public List<string> DependPrefab = new List<string>();
        public List<string> DependImage = new List<string>();
    }
    public class LDUIDependCfg
    {
        public static Dictionary<string, LDUIDependItem> UIDependItems = new Dictionary<string, LDUIDependItem>()
        {

        };
    }
}
