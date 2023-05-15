using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using System.Security.Permissions;
using MoreSlugcats;
using RWCustom;

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
            On.LizardAI.DoIWantToBiteThisCreature += BiteCreatureHook;
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

        bool BiteCreatureHook(On.LizardAI.orig_DoIWantToBiteThisCreature orig, LizardAI self, Tracker.CreatureRepresentation creature)
        {
            // If it is a slugpup
            if (creature.representedCreature != null && creature.representedCreature.creatureTemplate.type == MoreSlugcatsEnums.CreatureTemplateType.SlugNPC)
            {
                // Why does this game mode exist
                if (ModManager.CoopAvailable && Custom.rainWorld.options.friendlyLizards)
                {
                    bool playersFriendly = false; // Will turn true if any player is the lizard's friend

                    // Loop through all alive players
                    foreach (AbstractCreature checkCrit in self.lizard.abstractCreature.world.game.NonPermaDeadPlayers)
                    {
                        // If the player is the lizard's friend, break the loop and set the variable
                        if (self.friendTracker.friend == checkCrit.realizedCreature) { 
                            playersFriendly = true;
                            break;
                        }
                    }
                    // If any of the players are a lizard's friend, dont eat the slugpup
                    if (playersFriendly)
                    {
                        return false;
                    }
                }

                // If the parent is a player
                if (creature.representedCreature.abstractAI.parent.realizedCreature is Player)
                {
                    // Typecast the creature's parent to a player
                    Player player = creature.representedCreature.abstractAI.parent.realizedCreature as Player;
                    
                    // Why dont they use RELATIONSHIPS INSTEAD
                    if (self.friendTracker.friend == player)
                    {
                        return false;
                    }
                }
            }
            return orig(self, creature);
        }
    }
}