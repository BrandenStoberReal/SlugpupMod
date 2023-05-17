using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlugpupMod.Classes
{
    public class CreatureSeenTracker
    {
        Player AssociatedPlayer { get; set; }

        int GreenLizardsSeen{ get; set; } = 0;
        int BlueLizardsSeen { get; set; } = 0;
        int PinkLizardsSeen { get; set; } = 0;
        int WhiteLizardsSeen { get; set; } = 0;
        int YellowLizardsSeen { get; set; } = 0;
        int CentipedesSeen { get; set; } = 0;
        int DropwingsSeen { get; set; } = 0;
        int VulturesSeen { get; set; } = 0;

        // Class instantiation
        public CreatureSeenTracker(Player plr)
        {
            AssociatedPlayer = plr;
        }
    }
}
