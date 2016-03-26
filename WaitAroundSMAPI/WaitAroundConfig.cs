using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitAroundSMAPI
{
    class WaitAroundConfig : Config
    {
        public override string ConfigLocation
        {
            get
            {
                return "WaitAround\\config.cfg";
            }
        }
        public string menuKey { get; set; }
        public override T GenerateDefaultConfig<T>()
        {
            menuKey = "K";
            return this as T;
        }
    }
}
