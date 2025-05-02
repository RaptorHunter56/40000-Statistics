using ConsoleTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace _40000_Statistics
{
    internal class ModelBase
    {
        public string Name { get; set; }
        public virtual int ModelNo { get; set; }
        public virtual int Movement { get; set; }
        public virtual int Toughness { get; set; }
        public virtual int ArmorSave { get; set; }
        public virtual int InvulnerableSave { get; set; }
        public virtual int Wounds { get; set; }
        public virtual int Leadership { get; set; }
        public virtual int ObjectiveControl { get; set; }
        public List<AttackBase> Attacks { get; set; }
        public List<AttackGroupBase> AttackGroups { get; set; }
        public ModelBase() { 
            Attacks = new List<AttackBase>();
            AttackGroups = new List<AttackGroupBase>();
        }
        public override string ToString() => Name;
        public override bool Equals(object obj)
        {
            if (obj is ModelBase other)
            {
                return Toughness == other.Toughness &&
                       ArmorSave == other.ArmorSave &&
                       InvulnerableSave == other.InvulnerableSave &&
                       Wounds == other.Wounds;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + Toughness.GetHashCode();
            hash = hash * 31 + ArmorSave.GetHashCode();
            hash = hash * 31 + InvulnerableSave.GetHashCode();
            hash = hash * 31 + Wounds.GetHashCode();
            return hash;
        }

        public AttackOptionGroupBase GetAttackOptionGroupBase() => new AttackOptionGroupBase(AttackGroups);

        public static bool operator ==(ModelBase left, ModelBase right) => left is null ? right is null : left.Equals(right);
        public static bool operator !=(ModelBase left, ModelBase right) => !(left == right);
    }
    internal class UnitBase : ModelBase 
    { 
        public Dictionary<int, ModelBase> Models { get; set; }
        public UnitBase() => Models = new Dictionary<int, ModelBase>();
        public UnitBase(ModelBase model) => Models = new Dictionary<int, ModelBase> { { 1, model } };
        public override string ToString() => Name;
        public override int ModelNo => Models?.Keys.Sum() ?? 1;
        public override int Movement => Models.Values.FirstOrDefault()?.Movement ?? 0;
        public override int Toughness => Models.Values.FirstOrDefault()?.Toughness ?? 0;
        public override int ArmorSave => Models.Values.FirstOrDefault()?.ArmorSave ?? 0;
        public override int InvulnerableSave => Models.Values.FirstOrDefault()?.InvulnerableSave ?? 0;
        public override int Wounds => Models.Values.FirstOrDefault()?.Wounds ?? 0;
        public override int Leadership => Models.Values.FirstOrDefault()?.Leadership ?? 0;
        public override int ObjectiveControl => Models.Values.FirstOrDefault()?.ObjectiveControl ?? 0;

        public new IEnumerable<AttackOptionGroupBase> GetAttackOptionGroupBase() => Models.Select(x => new AttackOptionGroupBase(x.Value.AttackGroups, x.Key));

        public ModelBase this[int key]
        {
            get
            {
                Models.TryGetValue(key, out var value);
                return value;
            }
            set
            {
                Models[key] = value;
            }
        }
    }

    internal class AttackGroupBase
    {
        private int minNo;
        private int maxNo;
        virtual public int MinNo { get => minNo; set => minNo = value; }
        virtual public int MaxNo { get => maxNo; set => maxNo = value; }
        public int[] Attacks { get; set; }

        public AttackGroupBase(int maxNo = 1, int? minNo = null) { MinNo = minNo??maxNo; MaxNo = maxNo; }

        internal List<Tuple<int, Dictionary<int, int>>> GenerateSmallCombinations(AttackGroupBase attackGroup)
        {
            var smallCombinations = new List<Tuple<int, Dictionary<int, int>>>();
            for (int count = attackGroup.MinNo; count <= attackGroup.MaxNo; count++)
            {
                var combinations = new Dictionary<int, int>();
                foreach (var attack in attackGroup.Attacks)
                {
                    combinations[attack] = count;
                }
                smallCombinations.Add(new Tuple<int, Dictionary<int, int>>(count, combinations));
            }
            return smallCombinations;
        }
        internal List<Tuple<int, Dictionary<int, int>>> MergeCombinations(IEnumerable<Tuple<int, Dictionary<int, int>>> allCombinations, IEnumerable<Tuple<int, Dictionary<int, int>>> newCombinations, bool wargear = false)
        {
            if (allCombinations.Count() == 0) return newCombinations.ToList();

            var mergedCombinations = new List<Tuple<int, Dictionary<int, int>>>();
            foreach (var dictionary in allCombinations)
            {
                foreach (var combination in newCombinations)
                {
                    mergedCombinations.Add(new Tuple<int, Dictionary<int, int>>(dictionary.Item1 + (wargear ? 0 : combination.Item1), dictionary.Item2.CoJoin(combination.Item2, (value1, value2) => value1 + value2)));
                }
            }
            return mergedCombinations;
        }
    }
    internal class AttackOptionGroupBase : AttackGroupBase
    {
        public AttackOptionGroupBase(int maxNo = 1, int? minNo = null) : base(maxNo, minNo) { }
        public List<AttackGroupBase> AttackOption { get; set; }
        public AttackOptionGroupBase() => AttackOption = new List<AttackGroupBase>();
        public AttackOptionGroupBase(AttackGroupBase attackGroup)
        {
            AttackOption = new List<AttackGroupBase> { attackGroup };
            this.MinNo = attackGroup.MinNo;
            this.MaxNo = attackGroup.MaxNo;
        }
        public AttackOptionGroupBase(List<AttackGroupBase> attackGroupBases, int maxNo = 1, int? minNo = null) : base(maxNo, minNo) => AttackOption = attackGroupBases;

        public List<Tuple<int, Dictionary<int, int>>> GetAttackGroups()
        {
            var allCombinations = new List<Tuple<int,Dictionary<int, int>>>();

            foreach (var options in AttackOption)
            {
                if (options.GetType() == typeof(AttackGroupBase))
                {
                    var smallCombinations = GenerateSmallCombinations(options);
                    allCombinations = MergeCombinations(allCombinations, smallCombinations);
                }
                else if (options.GetType() == typeof(AttackOptionGroupBase))
                {
                    foreach (var attackOption in ((AttackOptionGroupBase)options).AttackOption)
                    {
                        var tempAttackGroups = (attackOption.GetType() == typeof(AttackGroupBase))
                            ? new AttackOptionGroupBase(attackOption).GetAttackGroups()
                            : ((AttackOptionGroupBase)attackOption).GetAttackGroups();

                        allCombinations = MergeCombinations(allCombinations, tempAttackGroups);
                    }
                }
            }
            var output = allCombinations.Where(tuple => tuple.Item1 >= MinNo && tuple.Item1 <= MaxNo).Select(combination => new Tuple<int, Dictionary<int, int>>(combination.Item1, combination.Item2.Where(kvp => kvp.Value != 0).Distinct().ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
            if (AttackOption.Where(attack => attack.GetType() == typeof(WargearGroupBase)).Count() > 0)
            {
                var wargearList = AttackOption.Where(attack => attack.GetType() == typeof(WargearGroupBase)).Select(attack => GenerateSmallCombinations(attack));
                foreach (var wargear in wargearList)
                { output = MergeCombinations(output, wargear, true); }
                output = output.Select(combination => new Tuple<int, Dictionary<int, int>>(combination.Item1, combination.Item2.Where(kvp => kvp.Value != 0).Distinct().ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
            }
            return output.ToList();
        }
    }
    internal class WargearGroupBase : AttackGroupBase
    {
        public WargearGroupBase(int maxNo = 1, int? minNo = null) : base(maxNo, minNo) { }
    }

    internal class AttackBase
    {
        public virtual string Name { get; set; }
        public virtual int Range { get; set; }
        public virtual int Attacks { get; set; }
        public virtual string AttacksExtra { get; set; }
        public virtual int? BallisticSkill { get; set; }
        public virtual int? WeaponSkill { get; set; }
        public virtual int? AttackSkill { get => ((Modifiers & Modifiers.Torrent) == Modifiers.Torrent) ? (int?)null : BallisticSkill ?? WeaponSkill.Value; }
        public virtual int Strength { get; set; }
        public virtual int ArmorPenetration { get; set; }
        public virtual int Damage { get; set; }
        public virtual string DamageExtra { get; set; }
        public virtual Modifiers Modifiers { get; set; }
        public AttackBase() { }
        public override string ToString() => $"{Name} - A {AttacksExtra ?? Attacks.ToString()}";
    }
    internal class AttackOption : AttackBase
    {
        public string SubType { get; set; }
        public List<AttackBase> AttacksTypes { get; set; }
        public AttackOption() => AttacksTypes = new List<AttackBase>();
        public AttackOption(AttackBase attack) => AttacksTypes = new List<AttackBase> { attack };
        public override string ToString() => Name;
        public override string Name => AttacksTypes.FirstOrDefault()?.Name ?? null;
        public override int Range => AttacksTypes.FirstOrDefault()?.Range ?? 0;
        public override int Attacks => AttacksTypes.FirstOrDefault()?.Attacks ?? 0;
        public override string AttacksExtra => AttacksTypes.FirstOrDefault()?.AttacksExtra ?? null;
        public override int? BallisticSkill => AttacksTypes.FirstOrDefault()?.BallisticSkill ?? null;
        public override int? WeaponSkill => AttacksTypes.FirstOrDefault()?.WeaponSkill ?? null;
        public override int? AttackSkill => AttacksTypes.FirstOrDefault()?.AttackSkill ?? null;
        public override int Strength => AttacksTypes.FirstOrDefault()?.Strength ?? 0;
        public override int ArmorPenetration => AttacksTypes.FirstOrDefault()?.ArmorPenetration ?? 0;
        public override int Damage => AttacksTypes.FirstOrDefault()?.Damage ?? 0;
        public override string DamageExtra => AttacksTypes.FirstOrDefault()?.DamageExtra ?? null;
        public override Modifiers Modifiers => AttacksTypes.FirstOrDefault()?.Modifiers ?? Modifiers.None;

        public AttackBase this[int index]
        {
            get
            {
                if (index < 0 || index >= AttacksTypes.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                return AttacksTypes[index];
            }
            set
            {
                if (index < 0 || index >= AttacksTypes.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                AttacksTypes[index] = value;
            }
        }
    }

    enum Modifiers
    {
        None            = 0,
        Anti_Monster_4  = 1 << 0,
        Assault         = 1 << 1,
        Hazardous       = 1 << 2,
        Melta_2         = 1 << 3,
        Pistol          = 1 << 4,
        Rapid_Fire_1    = 1 << 5,
        Twin_Linked     = 1 << 6,
        Blast           = 1 << 7,
        Indirect_Fire   = 1 << 8,
        Ignores_Cover   = 1 << 9,
        Torrent         = 1 << 10
    }

    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TResult> CoJoin<TKey, TValue, TResult>(
            this Dictionary<TKey, TValue> first,
            Dictionary<TKey, TValue> second,
            Func<TValue, TValue, TResult> joinFunc)
        {
            var result = new Dictionary<TKey, TResult>();
            foreach (var kvp in first)
            {
                if (second.TryGetValue(kvp.Key, out var secondValue))
                    result[kvp.Key] = joinFunc(kvp.Value, secondValue);
                else
                    result[kvp.Key] = (TResult)(object)kvp.Value;
            }
            foreach (var kvp in second)
            {
                if (!result.ContainsKey(kvp.Key))
                    result[kvp.Key] = (TResult)(object)kvp.Value;
            }
            return result;
        }
    }
}
