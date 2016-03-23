using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitAroundSMAPI
{
    public class WaitAroundMod : Mod
    {
        private static int LatestTime = 2550;
        private int _timeToWait;
        public int timeToWait
        {
            get { return _timeToWait; }
            set
            {
                if (value < 0)
                {
                    return;
                }
                int newTime = getTimeFromOffset(Game1.timeOfDay, value);
                if (newTime > LatestTime)
                {
                    _timeToWait = getOffsetFromTimes(Game1.timeOfDay, LatestTime);
                }
                else
                {
                    _timeToWait = value;
                }
            }
        }
        private Keys menuKey { get; set; }
        private WaitAroundMenu waitMenu { get; set; }

        public override void Entry(params object[] objects)
        {
            //menuKey = (Keys)Enum.Parse(typeof(Keys), ModConfig.menuKey.ToUpper());
            menuKey = Keys.K;
            KeyboardInput.KeyDown += KeyPressed;
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(menuKey) && Game1.hasLoadedGame)
            {
                if (Game1.activeClickableMenu == null)
                {
                    // this will bug animation frames if you don't prevent it
                    if (!Game1.player.usingTool)
                    {
                        Game1.activeClickableMenu = new WaitAroundMenu(this);
                    }
                }
                else if(Game1.activeClickableMenu is WaitAroundMenu)
                {
                    ((WaitAroundMenu)Game1.activeClickableMenu).Close();
                }
            }
        }

        public static int getTimeFromOffset(int startTime, int offset)
        {
            int time = startTime;
            for (; offset > 0; offset -= 10)
            {
                time += 10;
                if (time % 100 == 60)
                {
                    time += 40;
                }
            }
            return time;
        }

        public static int getOffsetFromTimes(int startTime, int endTime)
        {
            int offset = 0;
            for (int i = startTime + 10; i - 10 < endTime; i += 10)
            {
                offset += 10;
                if (i % 100 == 60)
                {
                    i += 40;
                }
            }
            return offset;
        }
    }
}
