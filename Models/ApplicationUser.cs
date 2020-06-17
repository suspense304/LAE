using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostArkEng.Models
{
    public class ApplicationUser : IdentityUser
    {
        public enum CharacterClass
        {
            Berserker,
            Destroyer,
            Warlord,
            Arcana,
            Summoner,
            Bard,
            BattleMaster,
            Infighter,
            SoulMaster,
            LanceMaster,
            Hawkeye,
            DevilHunter,
            Blaster
        }

        public string GetClassIcon(CharacterClass charClass)
        {
            if (charClass == CharacterClass.Berserker) return "Subclass_berserker";
            if (charClass == CharacterClass.Destroyer) return "Subclass_destroyer";
            if (charClass == CharacterClass.Warlord) return "Subclass_warlord";
            if (charClass == CharacterClass.Arcana) return "Subclass_arcana";
            if (charClass == CharacterClass.Summoner) return "Subclass_summoner";
            if (charClass == CharacterClass.Bard) return "Subclass_bard";
            if (charClass == CharacterClass.BattleMaster) return "Subclass_battlemaster";
            if (charClass == CharacterClass.Infighter) return "Subclass_infighter";
            if (charClass == CharacterClass.SoulMaster) return "Subclass_soulmaster";
            if (charClass == CharacterClass.LanceMaster) return "Subclass_lancemaster";
            if (charClass == CharacterClass.Hawkeye) return "Subclass_hawkeye";
            if (charClass == CharacterClass.DevilHunter) return "Subclass_devilhunter";
            if (charClass == CharacterClass.Blaster) return "Subclass_blaster";

            return "";
        }

        public enum ServerName
        {
            Kratos,
            Sirion
        }
        public string DiscordName { get; set; }
        public CharacterClass CharClass { get; set; }
        public ServerName Server { get; set;  }
        public double ItemLevel { get; set; }

        public string CharacterName { get; set; }

        public string DisplayName
        {
            get
            {
                return "IGN: " + CharacterName + "\nClass: " + CharClass + "\nDiscord: " + DiscordName + "\nGear Score: " + ItemLevel;
            }
        }

        
    }
}
