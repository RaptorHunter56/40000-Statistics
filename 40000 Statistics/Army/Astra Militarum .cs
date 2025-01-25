using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40000_Statistics
{
    static class AstraMilitarum 
    {
        public static List<ModelBase> Units = new List<ModelBase>()
        {
            new ModelBase(){ Name = "‘Iron Hand’ Straken", Movement = 6, Toughness = 3, ArmorSave = 3, InvulnerableSave = 4, Wounds = 4, Leadership = 7, ObjectiveControl = 1,
                Attacks = new List<AttackBase>()
                {
                    new AttackBase() { NoMax = 1, Name = "Auto shotgun",                Range = 12, Attacks = 3, BallisticSkill = 3, Strength = 4, ArmorPenetration = 0, Damage = 2, Modifiers = Modifiers.Assault },
                    new AttackBase() { NoMax = 1, Name = "Plasma pistol – standard",    Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 7, ArmorPenetration = 2, Damage = 1, Modifiers = Modifiers.Pistol },
                    new AttackBase() { NoMax = 1, Name = "Plasma pistol – supercharge", Range = 12, Attacks = 1, BallisticSkill = 3, Strength = 8, ArmorPenetration = 3, Damage = 2, Modifiers = Modifiers.Hazardous | Modifiers.Pistol },
                    new AttackBase() { NoMax = 1, Name = "Bionic arm with devil’s claw",            Attacks = 6, WeaponSkill = 2,    Strength = 6, ArmorPenetration = 2, Damage = 2, Modifiers = Modifiers.Anti_Monster_4 }
                }
            },
            new UnitBase() { Name = "Catachan Jungle Fighter - 10",
                Models = new Dictionary<int, ModelBase>()
                {
                    { 9, new ModelBase() { Name = "Jungle Fighters", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            new AttackBase() { NoMax = 2, NoMin = 0, Name = "Flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                            new AttackBase() { NoMax = 9, NoMin = 7, Name = "Lasgun",              Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                            new AttackBase() { NoMax = 9,            Name = "Close combat weapon",             Attacks = 1,         WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        }
                    } },
                    { 1, new ModelBase() { Name = "Jungle Fighter Sergeant", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            new AttackBase() { NoMax = 1, Name = "Laspistol",           Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                            new AttackBase() { NoMax = 1, Name = "Close combat weapon",             Attacks = 1, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        }
                    } }
                }
            },
            new UnitBase() { Name = "Catachan Jungle Fighter - 20",
                Models = new Dictionary<int, ModelBase>()
                {
                    { 18, new ModelBase() { Name = "Jungle Fighters", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            new AttackBase() { NoMax = 4,  NoMin = 0,  Name = "Flamer",              Range = 12, AttacksExtra = "D6",                     Strength = 4, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Ignores_Cover | Modifiers.Torrent },
                            new AttackBase() { NoMax = 18, NoMin = 14, Name = "Lasgun",              Range = 24, Attacks = 1,         BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Rapid_Fire_1 },
                            new AttackBase() { NoMax = 18,             Name = "Close combat weapon",             Attacks = 1,         WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        }
                    } },
                    { 2, new ModelBase() { Name = "Jungle Fighter Sergeant", Movement = 6, Toughness = 3, ArmorSave = 5, Wounds = 1, Leadership = 7, ObjectiveControl = 2,
                        Attacks = new List<AttackBase>()
                        {
                            new AttackBase() { NoMax = 2, Name = "Laspistol",           Range = 12, Attacks = 1, BallisticSkill = 4, Strength = 3, ArmorPenetration = 0, Damage = 1, Modifiers = Modifiers.Pistol },
                            new AttackBase() { NoMax = 2, Name = "Close combat weapon",             Attacks = 1, WeaponSkill = 4,    Strength = 3, ArmorPenetration = 0, Damage = 1 }
                        }
                    } }
                }
            }
        };
    }
}
