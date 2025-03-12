using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    static class Tau
    {
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            new ModelBase(){ Name = "Cadre Fireblade", Movement = 6, Toughness = 3, ArmorSave = 4, Wounds = 3, Leadership = 7, ObjectiveControl = 1,
                Attacks = new List<AttackBase>()
                {
                    new AttackBase() { /*NoMax = 1,           */ Name = "Fireblade pulse rifle", Range = 30, Attacks = 1, BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Rapid_Fire_1 },
                    new AttackBase() { /*NoMax = 1,           */ Name = "Close combat weapon",               Attacks = 3, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 },
                    new AttackBase() { /*NoMax = 2, NoMin = 0,*/ Name = "Twin pulse carbine",    Range = 20, Attacks = 2, BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                }
            },
            new ModelBase() { Name = "Commander Farsight", Movement = 10, Toughness = 5, ArmorSave = 2, InvulnerableSave = 4, Wounds = 8, Leadership = 6, ObjectiveControl = 2,
                Attacks = new List<AttackBase>()
                {
                    new AttackBase() { /*NoMax = 1,*/ Name = "High-intensity plasma rifle", Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 8,  ArmorPenetration = 3, Damage = 3 },
                    new AttackBase() { /*NoMax = 1,*/ Name = "Dawn Blade – strike",                     Attacks = 4, WeaponSkill = 2,    Strength = 10, ArmorPenetration = 2, Damage = 3 },
                    new AttackBase() { /*NoMax = 1,*/ Name = "Dawn Blade – sweep",                      Attacks = 8, WeaponSkill = 2,    Strength = 6,  ArmorPenetration = 1, Damage = 1 }
                }
            },
            new ModelBase() { Name = "Commander in Coldstar Battlesuit", Movement = 12, Toughness = 5, ArmorSave = 3, Wounds = 6, Leadership = 7, ObjectiveControl = 2,
                Attacks = new List<AttackBase>()
                {
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1},
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Cyclic ion blaster – standard",       Range = 18, Attacks = 3,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1},
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Cyclic ion blaster – overcharge",     Range = 18, Attacks = 3,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2,         Modifiers = Modifiers.Hazardous },
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Fusion blaster",                      Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "High-output burst cannon",            Range = 18, Attacks = 8,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Missile pod",                         Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Plasma rifle",                        Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "T’au flamer",                         Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent},
                    new AttackBase() { /*NoMax = 1,           */ Name = "Battlesuit fists",                                Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1},
                    new AttackBase() { /*NoMax = 2, NoMin = 0,*/ Name = "Twin pulse carbine",                  Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                //AttackGroups = new List<AttackGroupBase>()
                //{
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8,9}}*/,
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8,9}}*/,
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8,9}}*/,
                //    new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8,9}}
                //}
            },
            new ModelBase() { Name = "Commander in Enforcer Battlesuit", Movement = 8, Toughness = 5, ArmorSave = 2, Wounds = 6, Leadership = 7, ObjectiveControl = 2,
                Attacks = new List<AttackBase>()
                {
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Airbursting fragmentation projector", Range = 24, AttacksExtra = "D6", BallisticSkill = 3, Strength = 3, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Blast | Modifiers.Indirect_Fire},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Burst cannon",                        Range = 18, Attacks = 4,         BallisticSkill = 3, Strength = 5, ArmorPenetration = 0, Damage = 1},
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Cyclic ion blaster – standard",       Range = 18, Attacks = 3,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 1},
                    new AttackBase() { /*NoMax = 1, NoMin = 0,*/ Name = "Cyclic ion blaster – overcharge",     Range = 18, Attacks = 3,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 2, Damage = 2,         Modifiers = Modifiers.Hazardous },
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Fusion blaster",                      Range = 12, Attacks = 1,         BallisticSkill = 3, Strength = 9, ArmorPenetration = 4, DamageExtra = "D6", Modifiers = Modifiers.Melta_2 },
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Missile pod",                         Range = 30, Attacks = 2,         BallisticSkill = 3, Strength = 7, ArmorPenetration = 1, Damage = 2},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "Plasma rifle",                        Range = 18, Attacks = 1,         BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 3},
                    new AttackBase() { /*NoMax = 4, NoMin = 0,*/ Name = "T’au flamer",                         Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent},
                    new AttackBase() { /*NoMax = 1,           */ Name = "Battlesuit fists",                                Attacks = 3,         WeaponSkill = 4,    Strength = 5, ArmorPenetration = 0, Damage = 1},
                    new AttackBase() { /*NoMax = 2, NoMin = 0,*/ Name = "Twin pulse carbine",                  Range = 20, Attacks = 2,         BallisticSkill = 5, Strength = 5, ArmorPenetration = 0, Damage = 1,         Modifiers = Modifiers.Assault | Modifiers.Twin_Linked }
                },
                //AttackGroups = new List<AttackGroupBase>()
                //{
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8}}*/,
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8}}*/,
                //    /*new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8}}*/,
                //    new AttackGroupBase() {Attacks = new List<int>(){1,2,3,4,5,6,7,8}}
                //}
            },
            new ModelBase() { Name = "Commander Shadowsun", Movement = 10, Toughness = 4, ArmorSave = 3, InvulnerableSave = 5, Wounds = 6, Leadership = 6, ObjectiveControl = 1,
                Attacks = new List<AttackBase>()
                {
                    //new AttackBase() { NoMax = 1, Name = "Flechette launcher",         Range = 18, Attacks = 5, BallisticSkill = 2, Strength = 3,  ArmorPenetration = 0, Damage = 1 },
                    new AttackBase() { /*NoMax = 2,*/ Name = "High-energy fusion blaster", Range = 18, Attacks = 1, BallisticSkill = 2, Strength = 10, ArmorPenetration = 4, DamageExtra = "D6" },
                    new AttackBase() { /*NoMax = 1,*/ Name = "Light missile pod",          Range = 24, Attacks = 2, BallisticSkill = 2, Strength = 7,  ArmorPenetration = 0, Damage = 2 },
                    new AttackBase() { /*NoMax = 1,*/ Name = "Pulse pistol",               Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 5,  ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                    new AttackBase() { /*NoMax = 1,*/ Name = "Battlesuit fists",                       Attacks = 3, WeaponSkill = 4,    Strength = 5,  ArmorPenetration = 0, Damage = 1 }
                }
            },
        };
    }
}
