using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using System.Security.Permissions;
using MoreSlugcats;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace BetterSlugPups
{
    [BepInPlugin("brandenstober.better_slugpuppies", "Better Slugpups", "0.1.0")] // (GUID, mod name, mod version)
    public class BetterSlugPups : BaseUnityPlugin
    {
        public void OnEnable()
        {
            /* This is called when the mod is loaded. */
            On.TempleGuardAI.ThrowOutScore += ThrowOutScoreHook;
        }

        // 1st argument is a reference to the original function, 2nd argument is a reference to the parent class calling the function, and anything after is the base function's arguments
        // The function must return the same type as the original function
        float ThrowOutScoreHook(On.TempleGuardAI.orig_ThrowOutScore orig, TempleGuardAI self, Tracker.CreatureRepresentation creature)
        {
            // Check if the targeted creature is a Slugpup
            if (creature.representedCreature != null && creature.representedCreature.creatureTemplate.type == MoreSlugcatsEnums.CreatureTemplateType.SlugNPC) // What the fuck?
            {
                // Check player's Karma
                if ((self.guard.room.game.session as StoryGameSession).saveState.deathPersistentSaveData.karma >= 9)
                {
                    // Make Guardians not aggressive if karma is high enough
                    return 0f;
                }
            }
            // Original function here
            return orig(self, creature); // Return value of original game's code
        }
    }
}