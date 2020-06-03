﻿using Microsoft.AspNetCore.Identity;
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
                return DiscordName + "  " + CharacterName;
            }
        }
    }
}
