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
using static _40000_Statistics.DictionaryExtensions;
using static System.Net.Mime.MediaTypeNames;

namespace _40000_Statistics
{
    public enum EffectsType
    {
        /// <summary>
        /// Func<List<Tuple<int, Dictionary<int, int>>>, List<Tuple<int, Dictionary<int, int>>>>
        /// </summary>
        AttackOption = 0,
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
    }
    public abstract class DelegatesBase
    {
        public Dictionary<EffectsType, List<Delegate>> Effects { get; } = new Dictionary<EffectsType, List<Delegate>>();
        public KeyValuePair<EffectsType, List<Delegate>> CreateEffects
        {
            set
            {
                List<Delegate> delegatesList = value.Value ?? new List<Delegate>();
                foreach (var del in delegatesList)
                {
                    AddEffect(value.Key, del);
                }
            }
        }
        public void AddEffect(EffectsType type, Delegate del)
        {
            if (!Effects.ContainsKey(type))
                Effects[type] = new List<Delegate>();
            Effects[type].Add(del);
        }
    }
    public class ModelBase : DelegatesBase
    {
        private bool complexAttacks = false;

        public string Name { get; set; }
        public virtual int ModelNo { get; set; }
        public virtual int Movement { get; set; }
        public virtual int Toughness { get; set; }
        public virtual int ArmorSave { get; set; }
        public virtual int InvulnerableSave { get; set; }
        public virtual int Wounds { get; set; }
        public virtual int Leadership { get; set; }
        public virtual int ObjectiveControl { get; set; }
        public virtual List<string> Keywords { get; set; }
        public virtual bool ComplexAttacks { get => complexAttacks; set => complexAttacks = value; }
        public List<AttackBase> Attacks { get; set; }
        public List<AttackGroupBase> AttackGroups { get; set; }
        public ModelBase() { 
            Attacks = new List<AttackBase>();
            AttackGroups = new List<AttackGroupBase>();
            ModelNo = 1;
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

        public static List<string> CreateKeywordList(params object[] items) => items.Select(item => item is string s ? s : item.ToString()).ToList();
    }
    public class UnitBase : ModelBase 
    { 
        public Dictionary<int, ModelBase> Models { get; set; }
        public UnitBase() => Models = new Dictionary<int, ModelBase>();
        public UnitBase(ModelBase model) => Models = new Dictionary<int, ModelBase> { { model.ModelNo, model } };
        public override string ToString() => Name ?? Models.Values.FirstOrDefault()?.Name ?? "None";
        public override int ModelNo => Models?.Keys.Sum() ?? 1;
        public override int Movement => Models.Values.FirstOrDefault()?.Movement ?? 0;
        public override int Toughness => Models.Values.FirstOrDefault()?.Toughness ?? 0;
        public override int ArmorSave => Models.Values.FirstOrDefault()?.ArmorSave ?? 0;
        public override int InvulnerableSave => Models.Values.FirstOrDefault()?.InvulnerableSave ?? 0;
        public override int Wounds => Models.Values.FirstOrDefault()?.Wounds ?? 0;
        public override int Leadership => Models.Values.FirstOrDefault()?.Leadership ?? 0;
        public override int ObjectiveControl => Models.Values.FirstOrDefault()?.ObjectiveControl ?? 0;
        public override List<string> Keywords => Models.Values.SelectMany(x => x.Keywords).Distinct().ToList() ?? new List<string>();

        public new IEnumerable<AttackOptionGroupBase> GetAttackOptionGroupBase() => Models.Select(model => {
            var (max, min) = GetAttackMaxMin(model);
            return new AttackOptionGroupBase(model.Value.AttackGroups, max, min);
        });
        public Tuple<int, int?> GetAttackMaxMin(KeyValuePair<int, ModelBase> model)
        {
            if (model.Value.ComplexAttacks)
                return new Tuple<int, int?>(model.Value.AttackGroups.Where(attack => attack.GetType() != typeof(WargearGroupBase)).Select(attack => attack.MaxNo == attack.MinNo ? attack.MaxNo : attack.MaxNo - attack.MinNo).Sum(), model.Value.AttackGroups.Select(attack => attack.MinNo).Sum());
            else
                return new Tuple<int, int?>(model.Key, null);
        }

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

    public class AttackGroupBase : DelegatesBase
    {
        private int minNo;
        private int maxNo;
        virtual public int MinNo { get => minNo; set => minNo = value; }
        virtual public int MaxNo { get => maxNo; set => maxNo = value; }
        public int[] Attacks { get; set; }

        public AttackGroupBase(int maxNo = 1, int? minNo = null) { MinNo = minNo??maxNo; MaxNo = maxNo; }

        public List<Tuple<int, Dictionary<int, int>>> GenerateSmallCombinations(AttackGroupBase attackGroup)
        {
            var smallCombinations = new List<Tuple<int, Dictionary<int, int>>>();
            for (int count = attackGroup.MinNo; count <= attackGroup.MaxNo; count++)
            {
                var combinations = new Dictionary<int, int>();
                foreach (var attack in attackGroup.Attacks)
                {
                    combinations[attack] = combinations.ContainsKey(attack) ? combinations[attack] + count : count;
                }
                smallCombinations.Add(new Tuple<int, Dictionary<int, int>>(count, combinations));
            }
            return smallCombinations;
        }
        public List<Tuple<int, Dictionary<int, int>>> MergeCombinations(IEnumerable<Tuple<int, Dictionary<int, int>>> allCombinations, IEnumerable<Tuple<int, Dictionary<int, int>>> newCombinations, bool wargear = false)
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
    public class AttackOptionGroupBase : AttackGroupBase
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

        public static List<Tuple<int, Dictionary<int, int>>> GetAttackGroups(IEnumerable<AttackOptionGroupBase> attackOptions, DelegatesBase Effects = null)
        {
            List<Tuple<int, Dictionary<int, int>>> outAttackGroupsList = null;
            foreach (var option in attackOptions)
            {
                var attackGroupsSingle = option.GetAttackGroupsSingle();
                outAttackGroupsList = outAttackGroupsList == null ? attackGroupsSingle : outAttackGroupsList.SelectMany(o => attackGroupsSingle, (o, t) => new Tuple<int, Dictionary<int, int>>(o.Item1 + t.Item1, o.Item2.CoJoin(t.Item2, (v1, v2) => v1 + v2).OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value))).ToList();
            }
            outAttackGroupsList = outAttackGroupsList.Distinct(new TupleComparer()).ToList() ?? new List<Tuple<int, Dictionary<int, int>>>();
            if (Effects.Effects != null && Effects.Effects.Any(x => x.Key == EffectsType.AttackOption))
                outAttackGroupsList = Effects.Effects.Where(x => x.Key == EffectsType.AttackOption).SelectMany(x => x.Value).ToList().Aggregate(outAttackGroupsList, (current, del) => (List<Tuple<int, Dictionary<int, int>>>)del.DynamicInvoke(current));
            return outAttackGroupsList;
        }
        public List<Tuple<int, Dictionary<int, int>>> GetAttackGroupsSingle()
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
                            ? new AttackOptionGroupBase(attackOption).GetAttackGroupsSingle()
                            : ((AttackOptionGroupBase)attackOption).GetAttackGroupsSingle();

                        allCombinations = MergeCombinations(allCombinations, tempAttackGroups);
                    }
                }
            }
            var output = allCombinations.Where(tuple => tuple.Item1 >= MinNo && tuple.Item1 <= MaxNo).Select(combination => new Tuple<int, Dictionary<int, int>>(combination.Item1, combination.Item2.Where(kvp => kvp.Value != 0).Distinct().ToDictionary(kvp => kvp.Key, kvp => kvp.Value).OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
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
    public class WargearGroupBase : AttackGroupBase
    {
        public WargearGroupBase(int maxNo = 1, int? minNo = null) : base(maxNo, minNo) { }
    }

    public class AttackBase : DelegatesBase
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
    public class AttackOption : AttackBase
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

    public enum Modifiers
    {
        None               = 0,
        Anti_Monster_4     = 1 << 0,
        Assault            = 1 << 1,
        Hazardous          = 1 << 2,
        Melta_2            = 1 << 3,
        Pistol             = 1 << 4,
        Rapid_Fire_1       = 1 << 5,
        Twin_Linked        = 1 << 6,
        Blast              = 1 << 7,
        Indirect_Fire      = 1 << 8,
        Ignores_Cover      = 1 << 9,
        Torrent            = 1 << 10,
        Heavy              = 1 << 11,
        Precision          = 1 << 12,
        Lance              = 1 << 13,
        Extra_Attacks      = 1 << 14,
        Anti_Infantry_3    = 1 << 15,
        Lethal_Hits        = 1 << 16,
        Anti_Vehicle_4     = 1 << 17,
        Devastating_Wounds = 1 << 18,
        Sustained_Hits_1   = 1 << 19,
        Rapid_Fire_2       = 1 << 20
    }
    public enum Keywords
    {
        Infantry     = 0,
        Character    = 1 << 0, 
        Grenades     = 1 << 1, 
        Vehicle      = 1 << 2, 
        Walker       = 1 << 3, 
        Fly          = 1 << 4, 
        Epic_Hero    = 1 << 5, 
        Battlesuit   = 1 << 6,
        Markerlight  = 1 << 7,
        Kroot        = 1 << 9,
        Shaper       = 1 << 10,
        Mounted      = 1 << 11,
        Battleline   = 1 << 12,
        Fire_Warrior = 1 << 13,
        Beasts       = 1 << 14
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
        public class TupleComparer : IEqualityComparer<Tuple<int, Dictionary<int, int>>>
        {
            public bool Equals(Tuple<int, Dictionary<int, int>> x, Tuple<int, Dictionary<int, int>> y)
            {
                if (x.Item1 != y.Item1) return false;
                if (x.Item2.Count != y.Item2.Count) return false;
                foreach (var kvp in x.Item2)
                {
                    if (!y.Item2.TryGetValue(kvp.Key, out var value) || value != kvp.Value)
                    {
                        return false;
                    }
                }
                return true;
            }
            public int GetHashCode(Tuple<int, Dictionary<int, int>> obj)
            {
                int hash = obj.Item1.GetHashCode();
                foreach (var kvp in obj.Item2)
                {
                    hash ^= kvp.Key.GetHashCode() ^ kvp.Value.GetHashCode();
                }
                return hash;
            }
        }
    }
}
