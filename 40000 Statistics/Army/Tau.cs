using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace _40000_Statistics
{
    static class Tau
    {
        public static Dictionary<string, int> Point = new Dictionary<string, int>()
        {
            // Epic Hero
            {"Commander Farsight",               95},
            {"Commander Shadowsun",              100},
            {"Darkstrider",                      60},
            // Characters
            {"Cadre Fireblade",                  50},
            {"Commander in Coldstar Battlesuit", 95},
            {"Commander in Enforcer Battlesuit", 80},
            {"Ethereal",                         50},
            {"Firesight Team",                   70},
            {"Kroot Flesh Shaper",               45},
            {"Kroot Lone-Spear",                 80},
            {"Kroot Trail Shaper",               55},
            {"Kroot War Shaper",                 50},
            // Battleline
            {"Breacher Team",                    100},
            {"Strike Team",                      75},
            // Infantry
            {"Kroot Carnivores - 10",            65},
            {"Kroot Carnivores - 20",            140},
            {"Kroot Farstalkers",                85},
            {"Pathfinder Team",                  90},
            {"Stealth Battlesuits - 3",          80},
            {"Stealth Battlesuits - 5",          120},
            {"Vespid Stingwings - 5",            65},
            {"Vespid Stingwings - 10",           130},
            // Mounted
            {"Krootox Rampagers - 3",            85},
            {"Krootox Rampagers - 6",            170},
            {"Krootox Riders - 1",               40},
            {"Krootox Riders - 2",               60},
            {"Krootox Riders - 3",               90},
            // Beast
            {"Kroot Hounds - 5",                 40},
            {"Kroot Hounds - 10",                80},
            // Vehicle
            {"AX-1-0 Tiger Shark",               315 },
            {"Broadside Battlesuits - 1",        90 },
            {"Broadside Battlesuits - 2",        180 },
            {"Broadside Battlesuits - 3",        300 },
            {"Crisis Fireknife Battlesuits",     130 },
            {"Crisis Starscythe Battlesuits",    110 },
            {"Crisis Sunforge Battlesuits",      150 },
            {"Ghostkeel Battlesuit",             160 },
            {"Hammerhead Gunship",               145 },
            {"Manta",                            2100 },
            {"Piranha - 1",                      60 },
            {"Piranha - 2",                      120 },
            {"Piranha - 3",                      180 },
            {"Razorshark Strike Fighter",        170 },
            {"Riptide Battlesuit",               190 },
            {"Sky Ray Gunship",                  140 },
            {"Stormsurge",                       400 },
            {"Sun Shark Bomber",                 160 },
            {"Ta'unar Supremacy Armour",         790 },
            {"Tiger Shark",                      325 },
        };
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            #region Epic Hero
            new ModelBase() { ModelNo = 1, Name = "Commander Farsight", Movement = 10, Toughness = 5, ArmorSave = 2, InvulnerableSave = 4, Wounds = 8, Leadership = 6, ObjectiveControl = 2,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Epic_Hero, Keywords.Battlesuit, "Commander Farsight"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "High-intensity plasma rifle", Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 8,  ArmorPenetration = 3, Damage = 3 },
                    /*1*/ new AttackOption() { SubType = "Dawn Blade",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Dawn Blade – strike", Attacks = 4, WeaponSkill = 2, Strength = 10, ArmorPenetration = 2, Damage = 3 },
                                new AttackBase() { Name = "Dawn Blade – sweep",  Attacks = 8, WeaponSkill = 2, Strength = 6,  ArmorPenetration = 1, Damage = 1 }
                            }
                          }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander Shadowsun", Movement = 10, Toughness = 4, ArmorSave = 3, InvulnerableSave = 5, Wounds = 6, Leadership = 6, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Character, Keywords.Epic_Hero, Keywords.Battlesuit, "Commander Shadowsun"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Flechette launcher",         Range = 18, Attacks = 5, BallisticSkill = 2, Strength = 3,  ArmorPenetration = 3, Damage = 3 },
                    /*1*/ new AttackBase() { Name = "High-energy fusion blaster", Range = 18, Attacks = 1, BallisticSkill = 2, Strength = 10, ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Light missile pod",          Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 7,  ArmorPenetration = 3, Damage = 3 },
                    /*3*/ new AttackBase() { Name = "Pulse pistol",               Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 5,  ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Pistol },
                    /*4*/ new AttackBase() { Name = "Battlesuit fists",                       Attacks = 3, WeaponSkill = 4,    Strength = 5,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 1, 2, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Darkstrider", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Epic_Hero, Keywords.Markerlight, "Darkstrider"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Shade",               Range = 18, Attacks = 2, BallisticSkill = 2, Strength = 5, ArmorPenetration = 0, Damage = 2 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            #endregion
            #region Characters
            new ModelBase(){ ModelNo = 1, Name = "Cadre Fireblade", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Grenades, "Cadre Fireblade"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Fireblade pulse rifle", Range = 30, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",               Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Twin pulse carbine",    Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } },
                    new WargearGroupBase(1, 0) { Attacks = new int[] { 2 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander in Coldstar Battlesuit", Movement = 12, Toughness = 5, ArmorSave = 3, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Battlesuit, "Commander In Coldstar Battlesuit"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() {Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    /*1*/ new AttackBase() {Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*3*/ new AttackBase() {Name = "Fusion blaster",           Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() {Name = "High-output burst cannon", Range = 18, Attacks = 8,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*5*/ new AttackBase() {Name = "Missile pod",              Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*6*/ new AttackBase() {Name = "Plasma rifle",             Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3 },
                    /*7*/ new AttackBase() {Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*8*/ new AttackBase() {Name = "Battlesuit fists",                     Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*9*/ new AttackBase() {Name = "Twin pulse carbine",       Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(5, 1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 5 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 6 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 7 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 8 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 9 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Commander in Enforcer Battlesuit", Movement = 8, Toughness = 5, ArmorSave = 2, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Character, Keywords.Battlesuit, "Commander In Enforcer Battlesuit"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() {Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    /*1*/ new AttackBase() {Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*3*/ new AttackBase() {Name = "Fusion blaster",           Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() {Name = "Missile pod",              Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*5*/ new AttackBase() {Name = "Plasma rifle",             Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3 },
                    /*6*/ new AttackBase() {Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*7*/ new AttackBase() {Name = "Battlesuit fists",                     Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*8*/ new AttackBase() {Name = "Twin pulse carbine",       Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(4, 0)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 4 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 5 } },
                            new AttackGroupBase(4, 0) { Attacks = new int[] { 6 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 7 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 8 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Ethereal", Movement = 6, Toughness = 3, ArmorSave = 5, InvulnerableSave = 5, Wounds = 3, Leadership = 6, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, "Ethereal"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Honour stave",                   Attacks = 2, WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1},
                    /*1*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Firesight Team", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 4, Leadership = 7, ObjectiveControl = 3,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Markerlight, "Firesight Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Longshot pulse rifles", Range = 36, Attacks = 3, BallisticSkill = 4, Strength = 5, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Heavy | Modifiers.Precision },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",          Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Close combat weapons",              Attacks = 4, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1}
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 2 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Flesh Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "Flesh Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot scattergun", Range = 12, Attacks = 2, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Twin ritualistic blades",      Attacks = 4, WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Twin_Linked }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Lone-Spear", Movement = 12, Toughness = 5, ArmorSave = 5, Wounds = 6, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Character, Keywords.Kroot, "Lone-Spear"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Blast javelin",  Range = 18, AttacksExtra = "D6", BallisticSkill = 4, Strength = 10, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault | Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot long gun", Range = 36, Attacks = 1,         BallisticSkill = 3, Strength = 6,  ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Heavy | Modifiers.Precision },
                    /*2*/ new AttackBase() { Name = "Close combat weapon",        Attacks = 3,         WeaponSkill = 3,    Strength = 4,  ArmorPenetration = 1, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Hunting javelin",            Attacks = 3,         WeaponSkill = 3,    Strength = 4,  ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Lance },
                    /*4*/ new AttackBase() { Name = "Kalamandra’s bite",          Attacks = 4,         WeaponSkill = 4,    Strength = 5,  ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Extra_Attacks }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 3 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot Trail Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "Trail Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot rifle", Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*1*/ new AttackBase() { Name = "Shaper’s blade",          Attacks = 4, WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 1}
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Kroot War Shaper", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 3, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Character, Keywords.Kroot, Keywords.Shaper, "War Shaper"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Dart-bow and tri-blade", Range = 24, AttacksExtra = "D3+1", BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Anti_Infantry_3 | Modifiers.Assault| Modifiers.Heavy },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",           Range = 12, Attacks = 1,           BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Bladestave and prey-hook",           Attacks = 4,           WeaponSkill = 2,    Strength = 5, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Lethal_Hits },
                    /*3*/ new AttackBase() { Name = "Shaper’s blade",                     Attacks = 4,           WeaponSkill = 2,    Strength = 5, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 1, 3 } }
                }
            },
            #endregion
            #region Battleline
            new UnitBase() { Name = "Breacher Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Battleline, Keywords.Grenades, Keywords.Markerlight, Keywords.Fire_Warrior, "Breacher Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Pulse blaster",      Range = 10, Attacks = 2, BallisticSkill = 3, Strength = 6, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Support turret",     Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 1, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Breacher Fire Warriors", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(9) { Attacks = new int[] { 0, 1, 4 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Breacher Fire Warrior Shas’ui", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 4 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Strike Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Battleline, Keywords.Grenades, Keywords.Markerlight, Keywords.Fire_Warrior, "Breacher Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Pulse carbine",      Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Pulse pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Pulse rifle",        Range = 30, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Support turret",     Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 1, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Fire Warriors", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(9, 0) { Attacks = new int[] { 2 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 1, 5 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Fire Warrior Shas’ui", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 5 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                }
            },
            #endregion
            #region Infantry
            new UnitBase() { Name = "Kroot Carnivores - 10",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Kroot, "Carnivores"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Tanglebomb launcher", Range = 24, AttacksExtra = "D3", BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",        Range = 12, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Kroot rifle",         Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Kroot carbine",       Range = 18, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2 },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 2,         WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Kroot Carnivores", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 8) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 4 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Long-quill", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 4 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Kroot Carnivores - 20",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Kroot, "Carnivores"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Tanglebomb launcher", Range = 24, AttacksExtra = "D3", BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast },
                    /*1*/ new AttackBase() { Name = "Kroot pistol",        Range = 12, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*2*/ new AttackBase() { Name = "Kroot rifle",         Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*3*/ new AttackBase() { Name = "Kroot carbine",       Range = 18, Attacks = 1,         BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 2 },
                    /*4*/ new AttackBase() { Name = "Close combat weapon",             Attacks = 2,         WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(19, new ModelBase() { ModelNo = 19, Name = "Kroot Carnivores", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(19)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(19, 17) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } }
                                }
                            },
                            new AttackGroupBase(19) { Attacks = new int[] { 4 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Long-quill", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 1, 4 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Kroot Farstalkers",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Kroot, Keywords.Grenades, "Farstalkers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Dvorgite skinner",   Range = 12, AttacksExtra = "D6",             Strength = 4, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*1*/ new AttackBase() { Name = "Farstalker firearm", Range = 24, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*2*/ new AttackBase() { Name = "Kroot pistol",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*3*/ new AttackBase() { Name = "Londaxi tribalest",  Range = 18, Attacks = 3, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Anti_Vehicle_4 | Modifiers.Devastating_Wounds | Modifiers.Heavy },
                    /*4*/ new AttackBase() { Name = "T’au-tech rifle",    Range = 30, Attacks = 1, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                    /*5*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 2, WeaponSkill = 3,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Ripping fangs",                  Attacks = 3, WeaponSkill = 3,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*7*/ new AttackBase() { Name = "Ritual blade",                   Attacks = 3, WeaponSkill = 3,    Strength = 5, ArmorPenetration = 0, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Kroot Farstalkers", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 7) { Attacks = new int[] { 1 } },
                                    new AttackOptionGroupBase(1, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 2, 5 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Kroot Kill-broker", Movement = 7, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2, 7 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Kroot Hounds", Movement = 12, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 7, ObjectiveControl = 0,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2) { Attacks = new int[] {6 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Pathfinder Team",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Grenades, Keywords.Markerlight, "Pathfinder Team"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Drone burst cannon", Range = 18, Attacks = 4, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Ion rifle",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion rifle – standard",   Range = 30, Attacks = 3, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Heavy },
                                new AttackBase() { Name = "Ion rifle – overcharge", Range = 30, Attacks = 3, BallisticSkill = 5, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous | Modifiers.Heavy }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Pulse pistol",  Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    /*4*/ new AttackBase() { Name = "Rail rifle",    Range = 30, Attacks = 1, BallisticSkill = 5, Strength = 10, ArmorPenetration = 4, Damage = 3, Modifiers = Modifiers.Devastating_Wounds | Modifiers.Heavy },
                    /*5*/ new AttackOption() { SubType = "Semi-automatic grenade launcher",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Semi-automatic grenade launcher – EMP",    Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Anti_Vehicle_4 | Modifiers.Devastating_Wounds },
                                new AttackBase() { Name = "Semi-automatic grenade launcher – fusion", Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 3 }
                            }
                          },
                    /*6*/ new AttackBase() { Name = "Close combat weapon",            Attacks = 2, WeaponSkill = 5,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    /*7*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Pathfinders", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 5) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 5 } },
                                    new AttackOptionGroupBase(3, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(3, 0) { Attacks = new int[] { 1 } },
                                            new AttackGroupBase(3, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 3, 6 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Pathfinder Shas’ui", Movement = 7, Toughness = 3, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 5 } },
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 3, 6 } },
                            new WargearGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    })
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 5 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(5) || x.Item2[5] <= 1).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Stealth Battlesuits - 3",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Battlesuit, "Stealth"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",       Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Fusion blaster",     Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 2, WeaponSkill = 5,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Stealth Shas’ui", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 1) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(2) { Attacks = new int[] { 2 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Stealth Shas’vre", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 1 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(1) || x.Item2[1] <= 1).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Stealth Battlesuits - 5",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, Keywords.Battlesuit, "Stealth"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",       Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Fusion blaster",     Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 2, WeaponSkill = 5,    Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(4, new ModelBase() { ModelNo = 4, Name = "Stealth Shas’ui", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(4)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(4, 2) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(4) { Attacks = new int[] { 2 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Stealth Shas’vre", Movement = 8, Toughness = 4, ArmorSave = 3, Wounds = 2, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make Attack 1 limit 1
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => !x.Item2.ContainsKey(1) || x.Item2[1] <= 2).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Vespid Stingwings - 5",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, "Vespid Stingwings"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Neutron blaster", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Stingwing claws",             Attacks = 1, WeaponSkill = 4,    Strength = 4,  ArmorPenetration = 1, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(4, new ModelBase() { ModelNo = 4, Name = "Vespid Stingwings", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(4) { Attacks = new int[] { 0, 1 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Vespid Strain Leader", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 1 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Vespid Stingwings - 10",
                Keywords = ModelBase.CreateKeywordList(Keywords.Infantry, Keywords.Fly, "Vespid Stingwings"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Neutron blaster",          Range = 18, Attacks = 4,         BallisticSkill = 4, Strength = 5,  ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Assault },
                    /*1*/ new AttackBase() { Name = "Neutron grenade launcher", Range = 18, AttacksExtra = "D6", BallisticSkill = 4, Strength = 4,  ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Anti_Infantry_3 | Modifiers.Blast},
                    /*2*/ new AttackBase() { Name = "Neutron rail rifle",       Range = 30, Attacks = 1,         BallisticSkill = 4, Strength = 10, ArmorPenetration = 4, Damage = 3, Modifiers = Modifiers.Melta_2 },
                    /*3*/ new AttackBase() { Name = "T’au flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Melta_2 },
                    /*4*/ new AttackBase() { Name = "Stingwing claws",                      Attacks = 1,         WeaponSkill = 4,    Strength = 4,  ArmorPenetration = 1, Damage = 1 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(9, new ModelBase() { ModelNo = 9, Name = "Vespid Stingwings", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(9)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(9, 6) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } }
                                }
                            },
                            new AttackGroupBase(9) { Attacks = new int[] { 4 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Vespid Strain Leader", Movement = 12, Toughness = 4, ArmorSave = 4, Wounds = 1, Leadership = 7, ObjectiveControl = 1,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 4 } }
                        }
                    })
                }
            },
            #endregion
            #region Mounted
            new ModelBase() { ModelNo = 3, Name = "Krootox Rampagers- 3", Movement = 7, Toughness = 6, ArmorSave = 5, Wounds = 5, Leadership = 7, ObjectiveControl = 2,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Grenades, Keywords.Kroot, "Krootox Rampagers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot pistol and hunting javelins", Range = 12, Attacks = 2, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",                           Attacks = 3, WeaponSkill = 3,    Strength = 4, ArmorPenetration = 1, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Krootox fists",                                 Attacks = 4, WeaponSkill = 3,    Strength = 6, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Extra_Attacks | Modifiers.Sustained_Hits_1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(3) { Attacks = new int[] { 0, 1, 2 } }
                }
            },
            new ModelBase() { ModelNo = 6, Name = "Krootox Rampagers- 6", Movement = 7, Toughness = 6, ArmorSave = 5, Wounds = 5, Leadership = 7, ObjectiveControl = 2,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Grenades, Keywords.Kroot, "Krootox Rampagers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Kroot pistol and hunting javelins", Range = 12, Attacks = 2, BallisticSkill = 4, Strength = 4, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Close combat weapon",                           Attacks = 3, WeaponSkill = 3,    Strength = 4, ArmorPenetration = 1, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Krootox fists",                                 Attacks = 4, WeaponSkill = 3,    Strength = 6, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Extra_Attacks | Modifiers.Sustained_Hits_1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(6) { Attacks = new int[] { 0, 1, 2 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Krootox Riders - 1", Movement = 7, Toughness = 6, ArmorSave = 5, Wounds = 5, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Grenades, Keywords.Kroot, "Krootox Rampagers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Repeater cannon", Range = 36, Attacks = 2,           BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Rapid_Fire_2 },
                    /*1*/ new AttackBase() { Name = "Tanglecannon",    Range = 36, AttacksExtra = "D6+1", BallisticSkill = 4, Strength = 6, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Heavy },
                    /*2*/ new AttackBase() { Name = "Close combat weapon",         Attacks = 3,           WeaponSkill = 3,    Strength = 4, ArmorPenetration = 1, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Krootox fists",               Attacks = 4,           WeaponSkill = 3,    Strength = 6, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Extra_Attacks }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 3 } }
                }
            },
            new ModelBase() { ModelNo = 2, Name = "Krootox Riders - 2", Movement = 7, Toughness = 6, ArmorSave = 5, Wounds = 5, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Grenades, Keywords.Kroot, "Krootox Rampagers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Repeater cannon", Range = 36, Attacks = 2,           BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Rapid_Fire_2 },
                    /*1*/ new AttackBase() { Name = "Tanglecannon",    Range = 36, AttacksExtra = "D6+1", BallisticSkill = 4, Strength = 6, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Heavy },
                    /*2*/ new AttackBase() { Name = "Close combat weapon",         Attacks = 3,           WeaponSkill = 3,    Strength = 4, ArmorPenetration = 1, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Krootox fists",               Attacks = 4,           WeaponSkill = 3,    Strength = 6, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Extra_Attacks }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(2)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } },
                        }
                    },
                    new AttackGroupBase(2) { Attacks = new int[] { 2, 3 } }
                }
            },
            new ModelBase() { ModelNo = 3, Name = "Krootox Riders - 3", Movement = 7, Toughness = 6, ArmorSave = 5, Wounds = 5, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Mounted, Keywords.Grenades, Keywords.Kroot, "Krootox Rampagers"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Repeater cannon", Range = 36, Attacks = 2,           BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Rapid_Fire_2 },
                    /*1*/ new AttackBase() { Name = "Tanglecannon",    Range = 36, AttacksExtra = "D6+1", BallisticSkill = 4, Strength = 6, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Heavy },
                    /*2*/ new AttackBase() { Name = "Close combat weapon",         Attacks = 3,           WeaponSkill = 3,    Strength = 4, ArmorPenetration = 1, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Krootox fists",               Attacks = 4,           WeaponSkill = 3,    Strength = 6, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Extra_Attacks }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(3)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(3, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(3, 0) { Attacks = new int[] { 1 } },
                        }
                    },
                    new AttackGroupBase(3) { Attacks = new int[] { 2, 3 } }
                }
            },
            #endregion
            #region Beast
            new ModelBase() { ModelNo = 5, Name = "Kroot Hounds - 5", Movement = 12, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 8, ObjectiveControl = 0,
                Keywords = ModelBase.CreateKeywordList(Keywords.Beasts, Keywords.Kroot, "Hounds"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Ripping fangs", Attacks = 3, WeaponSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(5) { Attacks = new int[] { 0 } }
                }
            },
            new ModelBase() { ModelNo = 10, Name = "Kroot Hounds - 10", Movement = 12, Toughness = 3, ArmorSave = 6, Wounds = 1, Leadership = 8, ObjectiveControl = 0,
                Keywords = ModelBase.CreateKeywordList(Keywords.Beasts, Keywords.Kroot, "Hounds"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Ripping fangs", Attacks = 3, WeaponSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(10) { Attacks = new int[] { 0 } }
                }
            },
            #endregion
            #region Vehicle
            new ModelBase() { ModelNo = 1, Name = "AX-1-0 Tiger Shark", Movement = 20, Toughness = 11, ArmorSave = 3, InvulnerableSave = 5, Wounds = 18, Leadership = 7, ObjectiveControl = 0, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Aircraft, Keywords.Fly, "AX-1-0 Tiger Shark"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Missile pod",            Range = 30,  Attacks = 2, BallisticSkill = 4, Strength = 7,  ArmorPenetration = 1, Damage = 2 },
                    /*3*/ new AttackBase() { Name = "Seeker missile",         Range = 48,  Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*4*/ new AttackBase() { Name = "Twin heavy rail cannon", Range = 120, Attacks = 1, BallisticSkill = 4, Strength = 26, ArmorPenetration = 5, Damage = 12, Modifiers = Modifiers.Devastating_Wounds | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Armoured hull",                       Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(2)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(6, 0) { Attacks = new int[] { 3 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 2, 4, 5 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Broadside Battlesuits - 1", Movement = 5, Toughness = 6, ArmorSave = 2, Wounds = 8, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Battlesuit, "Broadside"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Heavy rail rifle",          Range = 60, Attacks = 2, BallisticSkill = 4, Strength = 12, ArmorPenetration = 4, DamageExtra = "D6+1", Modifiers = Modifiers.Heavy | Modifiers.Devastating_Wounds },
                    /*1*/ new AttackBase() { Name = "High-yield missile pods",   Range = 30, Attacks = 6, BallisticSkill = 4, Strength = 7,  ArmorPenetration = 1, Damage = 2,           Modifiers = Modifiers.Twin_Linked },
                    /*2*/ new AttackBase() { Name = "Seeker missile",            Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin plasma rifle",         Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 8,  ArmorPenetration = 3, Damage = 3,           Modifiers = Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin smart missile system", Range = 30, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Crushing bulk",                         Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Twin pulse carbine",        Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*6*/ new AttackBase() { Name = "Missile pod",               Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 2 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackOptionGroupBase(2, 0)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackOptionGroupBase(1, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                }
                            }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 5 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 6 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make WargearGroupBase limit 2
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => (x.Item2.ContainsKey(6) ? x.Item2[6] : 0) + (x.Item2.ContainsKey(7) ? x.Item2[7] : 0) <= 2).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Broadside Battlesuits - 2",
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Battlesuit, "Broadside"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Heavy rail rifle",          Range = 60, Attacks = 2, BallisticSkill = 4, Strength = 12, ArmorPenetration = 4, DamageExtra = "D6+1", Modifiers = Modifiers.Heavy | Modifiers.Devastating_Wounds },
                    /*1*/ new AttackBase() { Name = "High-yield missile pods",   Range = 30, Attacks = 6, BallisticSkill = 4, Strength = 7,  ArmorPenetration = 1, Damage = 2,           Modifiers = Modifiers.Twin_Linked },
                    /*2*/ new AttackBase() { Name = "Seeker missile",            Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin plasma rifle",         Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 8,  ArmorPenetration = 3, Damage = 3,           Modifiers = Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin smart missile system", Range = 30, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Crushing bulk",                         Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Twin pulse carbine",        Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*6*/ new AttackBase() { Name = "Missile pod",               Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 2 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Broadside Shas’ui", Movement = 5, Toughness = 6, ArmorSave = 2, Wounds = 8, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackOptionGroupBase(2, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackOptionGroupBase(1, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 5 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 6 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Broadside Shas’vre", Movement = 5, Toughness = 6, ArmorSave = 2, Wounds = 8, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackOptionGroupBase(2, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackOptionGroupBase(1, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 5 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 6 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    })
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make WargearGroupBase limit 4
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => (x.Item2.ContainsKey(6) ? x.Item2[6] : 0) + (x.Item2.ContainsKey(7) ? x.Item2[7] : 0) <= 4).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Broadside Battlesuits - 3",
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Battlesuit, "Broadside"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Heavy rail rifle",          Range = 60, Attacks = 2, BallisticSkill = 4, Strength = 12, ArmorPenetration = 4, DamageExtra = "D6+1", Modifiers = Modifiers.Heavy | Modifiers.Devastating_Wounds },
                    /*1*/ new AttackBase() { Name = "High-yield missile pods",   Range = 30, Attacks = 6, BallisticSkill = 4, Strength = 7,  ArmorPenetration = 1, Damage = 2,           Modifiers = Modifiers.Twin_Linked },
                    /*2*/ new AttackBase() { Name = "Seeker missile",            Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin plasma rifle",         Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 8,  ArmorPenetration = 3, Damage = 3,           Modifiers = Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin smart missile system", Range = 30, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Crushing bulk",                         Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Twin pulse carbine",        Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked },
                    /*6*/ new AttackBase() { Name = "Missile pod",               Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 2 }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Broadside Shas’ui", Movement = 5, Toughness = 6, ArmorSave = 2, Wounds = 8, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackOptionGroupBase(4, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 2 } },
                                    new AttackOptionGroupBase(2, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(2, 0) { Attacks = new int[] { 3 } },
                                            new AttackGroupBase(2, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(2) { Attacks = new int[] { 5 } },
                            new WargearGroupBase(4, 0) { Attacks = new int[] { 6 } },
                            new WargearGroupBase(4, 0) { Attacks = new int[] { 7 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Broadside Shas’vre", Movement = 5, Toughness = 6, ArmorSave = 2, Wounds = 8, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(1)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackOptionGroupBase(2, 0)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                                    new AttackOptionGroupBase(1, 0)
                                    {
                                        AttackOption = new List<AttackGroupBase>()
                                        {
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                                        }
                                    }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 5 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 6 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    })
                },
                Effects = new Dictionary<EffectsType, List<Delegate>>()
                { {
                    EffectsType.AttackOption,
                    new List<Delegate>()
                    {
                        /// Make WargearGroupBase limit 4
                        (Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>)((outAttackGroupsList) => outAttackGroupsList.Where(x => (x.Item2.ContainsKey(6) ? x.Item2[6] : 0) + (x.Item2.ContainsKey(7) ? x.Item2[7] : 0) <= 6).ToList())
                    }
                } }
            },
            new UnitBase() { Name = "Crisis Fireknife Battlesuits",
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Battlesuit, Keywords.Crisis, "Fireknife"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Missile pod",        Range = 30, Attacks = 2, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*1*/ new AttackBase() { Name = "Plasma rifle",       Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 8, ArmorPenetration = 3, Damage = 3 },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 3, WeaponSkill = 5,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Crisis Fireknife Shas’ui", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(4)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(4, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(2) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(4, 0) { Attacks = new int[] { 3 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Crisis Fireknife Shas’vre", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Crisis Starscythe Battlesuits",
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Battlesuit, Keywords.Crisis, "Starscythe"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",       Range = 18, Attacks = 4,         BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "T’au flamer ",       Range = 12, AttacksExtra = "DG",                     Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                    /*2*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 3,         WeaponSkill = 5,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Crisis Starscythe Shas’ui", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(4)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(4, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(4, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(2) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(4, 0) { Attacks = new int[] { 3 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Crisis Starscythe Shas’vre", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackOptionGroupBase(2)
                            {
                                AttackOption = new List<AttackGroupBase>()
                                {
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                                    new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                                }
                            },
                            new AttackGroupBase(1) { Attacks = new int[] { 2 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 3 } }
                        }
                    })
                }
            },
            new UnitBase() { Name = "Crisis Sunforge Battlesuits",
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Battlesuit, Keywords.Crisis, "Starscythe"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Fusion blaster",     Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*1*/ new AttackBase() { Name = "Battlesuit fists",               Attacks = 3, WeaponSkill = 5,    Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*2*/ new AttackBase() { Name = "Twin pulse carbine", Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                Models = new List<Tuple<int, ModelBase>>()
                {
                    new Tuple<int, ModelBase>(2, new ModelBase() { ModelNo = 2, Name = "Crisis Sunforge Shas’ui", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2) { Attacks = new int[] { 0, 0, 1 } },
                            new WargearGroupBase(4, 0) { Attacks = new int[] { 2 } }
                        }
                    }),
                    new Tuple<int, ModelBase>(1, new ModelBase() { ModelNo = 1, Name = "Crisis Sunforge Shas’vre", Movement = 10, Toughness = 5, ArmorSave = 3, Wounds = 4, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                        AttackGroups = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1) { Attacks = new int[] { 0, 0, 1 } },
                            new WargearGroupBase(2, 0) { Attacks = new int[] { 2 } }
                        }
                    })
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Ghostkeel Battlesuit", Movement = 10, Toughness = 8, ArmorSave = 2, Wounds = 12, Leadership = 7, ObjectiveControl = 3, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Smoke, Keywords.Battlesuit, "Ghostkeel"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackOption() { SubType = "Cyclic ion raker",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion raker – standard",   Range = 36, Attacks = 6, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                                new AttackBase() { Name = "Cyclic ion raker – overcharge", Range = 36, Attacks = 6, BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*1*/ new AttackBase() { Name = "Fusion collider",      Range = 18, Attacks = 2, BallisticSkill = 4, Strength = 12, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    /*2*/ new AttackBase() { Name = "Twin burst cannon",    Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Twin_Linked },
                    /*3*/ new AttackBase() { Name = "Twin fusion blaster ", Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9,  ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 | Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin T’au flamer",     Range = 12, AttacksExtra = "D6",             Strength = 4,  ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Ghostkeel fists",                  Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 2 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 5 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Hammerhead Gunship", Movement = 10, Toughness = 10, ArmorSave = 3, Wounds = 14, Leadership = 7, ObjectiveControl = 3, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, "Hammerhead Gunship"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Accelerator burst cannon", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Ion cannon",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion cannon – standard",   Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Blast  },
                                new AttackBase() { Name = "Ion cannon – overcharge", Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Blast | Modifiers.Hazardous }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Railgun",              Range = 72, Attacks = 1, BallisticSkill = 4, Strength = 20, ArmorPenetration = 5, DamageExtra = "D6+6", Modifiers = Modifiers.Heavy | Modifiers.Devastating_Wounds },
                    /*3*/ new AttackBase() { Name = "Seeker missile",       Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*4*/ new AttackBase() { Name = "Twin pulse carbine",   Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Smart missile system", Range = 30, Attacks = 3, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Indirect_Fire },
                    /*6*/ new AttackBase() { Name = "Armoured hull",                    Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 5, 5 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4, 4 } }
                        }
                    },
                    new AttackGroupBase(2, 0) { Attacks = new int[] { 3 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 6 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Manta", Movement = 20, Toughness = 14, ArmorSave = 2, InvulnerableSave = 5, Wounds = 60, Leadership = 7, ObjectiveControl = 0, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Aircraft, Keywords.Fly, Keywords.Titanic, Keywords.Transport, Keywords.Markerlight, "Manta"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Heavy rail cannon", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Ion cannon",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion cannon – standard",   Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Blast  },
                                new AttackBase() { Name = "Ion cannon – overcharge", Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Blast | Modifiers.Hazardous }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Long-barrelled burst cannon array", Range = 72, Attacks = 1, BallisticSkill = 4, Strength = 20, ArmorPenetration = 5, DamageExtra = "D6+6", Modifiers = Modifiers.Heavy | Modifiers.Devastating_Wounds },
                    /*3*/ new AttackBase() { Name = "Missile pod",                       Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Seeker missile",                    Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*5*/ new AttackBase() { Name = "Armoured hull",                                 Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackGroupBase(2) { Attacks = new int[] { 0 } },
                    new AttackGroupBase(6) { Attacks = new int[] { 1 } },
                    new AttackGroupBase(2) { Attacks = new int[] { 2 } },
                    new AttackGroupBase(2) { Attacks = new int[] { 3 } },
                    new AttackGroupBase(10) { Attacks = new int[] { 4 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 5 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Piranha - 1", Movement = 14, Toughness = 7, ArmorSave = 4, Wounds = 7, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, "Piranha"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Piranha burst cannon",   Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Piranha fusion blaster", Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9,  ArmorPenetration = 4, DamageExtra = "D6",   Modifiers = Modifiers.Melta_4 },
                    /*2*/ new AttackBase() { Name = "Seeker missile",         Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine",     Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Twin_Linked | Modifiers.Assault },
                    /*4*/ new AttackBase() { Name = "Armoured hull",                      Attacks = 2, WeaponSkill = 5,    Strength = 4,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(2, 0) { Attacks = new int[] { 2 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 3, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 2, Name = "Piranha - 2", Movement = 14, Toughness = 7, ArmorSave = 4, Wounds = 7, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, "Piranha"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Piranha burst cannon",   Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Piranha fusion blaster", Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9,  ArmorPenetration = 4, DamageExtra = "D6",   Modifiers = Modifiers.Melta_4 },
                    /*2*/ new AttackBase() { Name = "Seeker missile",         Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine",     Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Twin_Linked | Modifiers.Assault },
                    /*4*/ new AttackBase() { Name = "Armoured hull",                      Attacks = 2, WeaponSkill = 5,    Strength = 4,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(2)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(4, 0) { Attacks = new int[] { 2 } },
                    new AttackGroupBase(2) { Attacks = new int[] { 3, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 3, Name = "Piranha - 3", Movement = 14, Toughness = 7, ArmorSave = 4, Wounds = 7, Leadership = 7, ObjectiveControl = 2, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, "Piranha"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Piranha burst cannon",   Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Piranha fusion blaster", Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9,  ArmorPenetration = 4, DamageExtra = "D6",   Modifiers = Modifiers.Melta_4 },
                    /*2*/ new AttackBase() { Name = "Seeker missile",         Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackBase() { Name = "Twin pulse carbine",     Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Twin_Linked | Modifiers.Assault },
                    /*4*/ new AttackBase() { Name = "Armoured hull",                      Attacks = 2, WeaponSkill = 5,    Strength = 4,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(3)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(3, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(3, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(6, 0) { Attacks = new int[] { 2 } },
                    new AttackGroupBase(3) { Attacks = new int[] { 3, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Razorshark Strike Fighter", Movement = 20, Toughness = 10, ArmorSave = 3, Wounds = 12, Leadership = 7, ObjectiveControl = 0, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Aircraft, Keywords.Fly, "Razorshark Strike Fighter"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Accelerator burst cannon", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Missile pod",              Range = 30, Attacks = 2, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*2*/ new AttackOption() { SubType = "Quad ion turret",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Quad ion turret – standard",   Range = 30, Attacks = 8, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Twin_Linked  },
                                new AttackBase() { Name = "Quad ion turret – overcharge", Range = 30, Attacks = 8, BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous | Modifiers.Twin_Linked }
                            }
                          },
                    /*3*/ new AttackBase() { Name = "Seeker missile", Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*4*/ new AttackBase() { Name = "Armoured hull",              Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 3, 3, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Riptide Battlesuit", Movement = 10, Toughness = 19, ArmorSave = 3, InvulnerableSave = 4, Wounds = 14, Leadership = 7, ObjectiveControl = 4, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker, Keywords.Fly, Keywords.Battlesuit, "Riptide"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Heavy burst cannon", Range = 36, Attacks = 12, BallisticSkill = 4, Strength = 6, ArmorPenetration = 1, Damage = 2 },
                    /*1*/ new AttackOption() { SubType = "Ion accelerator",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion accelerator – standard",   Range = 72, Attacks = 6, BallisticSkill = 4, Strength = 7, ArmorPenetration = 2, Damage = 3 },
                                new AttackBase() { Name = "Ion accelerator – overcharge", Range = 72, Attacks = 6, BallisticSkill = 4, Strength = 8, ArmorPenetration = 3, Damage = 4, Modifiers = Modifiers.Hazardous }
                            }
                          },
                    /*2*/ new AttackBase() { Name = "Twin fusion blaster",       Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 | Modifiers.Twin_Linked },
                    /*3*/ new AttackBase() { Name = "Twin plasma rifle",         Range = 18, Attacks = 1, BallisticSkill = 4, Strength = 8, ArmorPenetration = 3, Damage = 3,         Modifiers = Modifiers.Twin_Linked },
                    /*4*/ new AttackBase() { Name = "Twin smart missile system", Range = 30, Attacks = 3, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Riptide fists",                         Attacks = 6, WeaponSkill = 5,    Strength = 6, ArmorPenetration = 0, Damage = 1 },
                    /*6*/ new AttackBase() { Name = "Missile pod",               Range = 30, Attacks = 2, BallisticSkill = 5, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } },
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 5 } },
                    new WargearGroupBase(2, 0) { Attacks = new int[] { 6 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Sky Ray Gunship", Movement = 10, Toughness = 10, ArmorSave = 3, Wounds = 14, Leadership = 7, ObjectiveControl = 3, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, Keywords.Markerlight, "Sky Ray Gunship"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Accelerator burst cannon",  Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Seeker missile rack",       Range = 48, Attacks = 3, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.Twin_Linked },
                    /*2*/ new AttackBase() { Name = "Twin pulse carbine",        Range = 20, Attacks = 2, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Assault |Modifiers.Twin_Linked },
                    /*3*/ new AttackBase() { Name = "Twin smart missile system", Range = 30, Attacks = 3, BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Indirect_Fire },
                    /*4*/ new AttackBase() { Name = "Armoured hull",                         Attacks = 3, WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 2 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3, 3 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 1, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Stormsurge", Movement = 8, Toughness = 11, ArmorSave = 2, InvulnerableSave = 4, Wounds = 20, Leadership = 7, ObjectiveControl = 6, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Fly, Keywords.Markerlight, "Sky Ray Gunship"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Cluster rocket system", Range = 48, AttacksExtra = "4D6", BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,           Modifiers = Modifiers.Blast | Modifiers.Heavy },
                    /*1*/ new AttackBase() { Name = "Destroyer missiles",    Range = 72, Attacks = 1,          BallisticSkill = 4, Strength = 16, ArmorPenetration = 4, DamageExtra = "D6+2", Modifiers = Modifiers.Heavy },
                    /*2*/ new AttackOption() { SubType = "Pulse blast cannon",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Pulse blast cannon – focused",    Range = 24, Attacks = 2, BallisticSkill = 4, Strength = 24, ArmorPenetration = 6, Damage = 12, Modifiers = Modifiers.Heavy },
                                new AttackBase() { Name = "Pulse blast cannon – dispersed ", Range = 48, Attacks = 6, BallisticSkill = 4, Strength = 10, ArmorPenetration = 2, Damage = 4,  Modifiers = Modifiers.Heavy }
                            }
                          },
                    /*3*/ new AttackBase() { Name = "Pulse driver cannon",                      Range = 72, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 12, ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Blast | Modifiers.Heavy },
                    /*4*/ new AttackBase() { Name = "Twin airbursting fragmentation projector", Range = 24, AttacksExtra = "D6",   BallisticSkill = 4, Strength = 3,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Blast | Modifiers.Heavy | Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*5*/ new AttackBase() { Name = "Twin burst cannon",                        Range = 18, Attacks = 4,           BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Heavy |Modifiers.Twin_Linked },
                    /*6*/ new AttackBase() { Name = "Twin smart missile system",                Range = 30, Attacks = 3,           BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Heavy | Modifiers.Indirect_Fire | Modifiers.Twin_Linked },
                    /*7*/ new AttackBase() { Name = "Twin T’au flamer",                         Range = 12, AttacksExtra = "D6",                       Strength = 4,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent | Modifiers.Twin_Linked },
                    /*8*/ new AttackBase() { Name = "Armoured hull",                                        Attacks = 3,           WeaponSkill = 5,    Strength = 8,  ArmorPenetration = 1, Damage = 2 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 5 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 7 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 0, 1, 6, 6, 8 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Sun Shark Bomber", Movement = 20, Toughness = 9, ArmorSave = 3, Wounds = 12, Leadership = 7, ObjectiveControl = 0, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Aircraft, Keywords.Fly, "Sun Shark Bomber"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Missile pod",      Range = 30, Attacks = 2, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2 },
                    /*1*/ new AttackBase() { Name = "Twin missile pod", Range = 30, Attacks = 2, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Twin_Linked },
                    /*2*/ new AttackBase() { Name = "Seeker missile",   Range = 48, Attacks = 1, BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*3*/ new AttackOption() { SubType = "Twin ion rifle",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Twin ion rifle – standard",   Range = 30, Attacks = 3, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 1, Modifiers = Modifiers.Twin_Linked  },
                                new AttackBase() { Name = "Twin ion rifle – overcharge", Range = 30, Attacks = 3, BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Hazardous | Modifiers.Twin_Linked }
                            }
                          },
                    /*4*/ new AttackBase() { Name = "Armoured hull", Attacks = 3, WeaponSkill = 5, Strength = 6, ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1 } }
                        }
                    },
                    new AttackGroupBase(1) { Attacks = new int[] { 2, 2, 4 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Ta’unar Supremacy Armour", Movement = 8, Toughness = 13, ArmorSave = 2, InvulnerableSave = 5, Wounds = 30, Leadership = 7, ObjectiveControl = 10, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Walker,  Keywords.Titanic, Keywords.Towering, "Ta’unar Supremacy Armour"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon",                         Range = 18,  Attacks = 4,            BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackBase() { Name = "Fragmentation cluster shell launcher", Range = 24,  AttacksExtra = "2D6+6", BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1,         Modifiers = Modifiers.Blast },
                    /*2*/ new AttackBase() { Name = "Fusion eradicator",                    Range = 24,  Attacks = 5,            BallisticSkill = 4, Strength = 10, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_3 },
                    /*3*/ new AttackBase() { Name = "Heavy rail cannon array",              Range = 120, Attacks = 2,            BallisticSkill = 4, Strength = 26, ArmorPenetration = 5, Damage = 16,        Modifiers = Modifiers.Devastating_Wounds | Modifiers.Heavy },
                    /*4*/ new AttackBase() { Name = "Nexus missile launcher",               Range = 36,  Attacks = 8,            BallisticSkill = 4, Strength = 8,  ArmorPenetration = 3, Damage = 2 },
                    /*5*/ new AttackBase() { Name = "Pulse ordnance driver",                Range = 60,  Attacks = 8,            BallisticSkill = 4, Strength = 5,  ArmorPenetration = 1, Damage = 3,         Modifiers = Modifiers.Anti_Infantry_2 },
                    /*6*/ new AttackBase() { Name = "Smart missile system",                 Range = 30,  Attacks = 3,            BallisticSkill = 4, Strength = 5,  ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Indirect_Fire },
                    /*7*/ new AttackOption() { SubType = "Tri-axis ion cannon",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Tri-axis ion cannon – standard",   Range = 36, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Blast  },
                                new AttackBase() { Name = "Tri-axis ion cannon – overcharge", Range = 36, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 9, ArmorPenetration = 3, Damage = 3, Modifiers = Modifiers.Blast | Modifiers.Twin_Linked }
                            }
                          },
                    /*8*/ new AttackBase() { Name = "Crushing feet", Attacks = 6, WeaponSkill = 5, Strength = 8, ArmorPenetration = 1, Damage = 2 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(2)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 2 } },
                            new AttackGroupBase(2, 0) { Attacks = new int[] { 7 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 5, 5, 5 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 4, 4 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 3, 1 } }
                        }
                    },
                    new AttackGroupBase(4) { Attacks = new int[] { 0 } },
                    new AttackGroupBase(4) { Attacks = new int[] { 6 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 8 } }
                }
            },
            new ModelBase() { ModelNo = 1, Name = "Tiger Shark", Movement = 20, Toughness = 11, ArmorSave = 3, InvulnerableSave = 5, Wounds = 18, Leadership = 7, ObjectiveControl = 0, ComplexAttacks = true,
                Keywords = ModelBase.CreateKeywordList(Keywords.Vehicle, Keywords.Aircraft, Keywords.Fly, "Tiger Shark"),
                Attacks = new List<AttackBase>()
                {
                    /*0*/ new AttackBase() { Name = "Burst cannon", Range = 18, Attacks = 4, BallisticSkill = 4, Strength = 5, ArmorPenetration = 0, Damage = 1 },
                    /*1*/ new AttackOption() { SubType = "Cyclic ion blaster",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Cyclic ion blaster – standard",   Range = 18, Attacks = 3, BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 1 },
                                new AttackBase() { Name = "Cyclic ion blaster – overcharge", Range = 18, Attacks = 3, BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Twin_Linked }
                            }
                          },
                    /*2*/ new AttackOption() { SubType = "Ion cannon",
                            AttacksTypes = new List<AttackBase>()
                            {
                                new AttackBase() { Name = "Ion cannon – standard",   Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 7, ArmorPenetration = 1, Damage = 2, Modifiers = Modifiers.Blast  },
                                new AttackBase() { Name = "Ion cannon – overcharge", Range = 60, AttacksExtra = "D6+3", BallisticSkill = 4, Strength = 8, ArmorPenetration = 2, Damage = 3, Modifiers = Modifiers.Blast | Modifiers.Twin_Linked }
                            }
                          },
                    /*3*/ new AttackBase() { Name = "Missile pod",              Range = 30, Attacks = 2,           BallisticSkill = 4, Strength = 7,  ArmorPenetration = 1, Damage = 2 },
                    /*4*/ new AttackBase() { Name = "Seeker missile",           Range = 48, Attacks = 1,           BallisticSkill = 4, Strength = 14, ArmorPenetration = 3, DamageExtra = "D6+1", Modifiers = Modifiers.One_Shot },
                    /*5*/ new AttackBase() { Name = "Skyspear missile rack",    Range = 72, AttacksExtra = "D6+1", BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1,           Modifiers = Modifiers.Anti_Fly_3 | Modifiers.Blast },
                    /*6*/ new AttackBase() { Name = "Swiftstrike burst cannon", Range = 36, Attacks = 16,          BallisticSkill = 4, Strength = 6,  ArmorPenetration = 1, Damage = 1 },
                    /*7*/ new AttackBase() { Name = "Swiftstrike railgun",      Range = 72, Attacks = 1,           BallisticSkill = 4, Strength = 20, ArmorPenetration = 5, DamageExtra = "D6+6", Modifiers = Modifiers.Devastating_Wounds },
                    /*8*/ new AttackBase() { Name = "Armoured hull",                        Attacks = 3,           WeaponSkill = 5,    Strength = 6,  ArmorPenetration = 0, Damage = 1 }
                },
                AttackGroups = new List<AttackGroupBase>()
                {
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 0, 0 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 1, 1 } }
                        }
                    },
                    new AttackOptionGroupBase(1)
                    {
                        AttackOption = new List<AttackGroupBase>()
                        {
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 2, 2 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 6, 6 } },
                            new AttackGroupBase(1, 0) { Attacks = new int[] { 7, 7 } }
                        }
                    },
                    new AttackGroupBase(2, 0) { Attacks = new int[] { 5 } },
                    new AttackGroupBase(6, 0) { Attacks = new int[] { 4 } },
                    new AttackGroupBase(1) { Attacks = new int[] { 3, 3, 8 } }
                }
            },
            #endregion
        };
    }
}
