using System;
using System.Collections.Generic;
using TotalBattle.FileSystem;
using TotalBattle.Objects;

namespace TotalBattle
{
    class UnitManager : FileTemplate, IDataReceiver<List<Unit>>
    {
        private List<Unit> _units;
        
        private Dictionary<string, Weapon> _weapon =
            new Dictionary<string, Weapon>()
            {
                { WeaponArsenal.sword.Name, WeaponArsenal.sword},
                { WeaponArsenal.spear.Name, WeaponArsenal.spear},
                { WeaponArsenal.mace.Name, WeaponArsenal.mace},
                { WeaponArsenal.axe.Name, WeaponArsenal.axe},
            };
        private Dictionary<string, Armor> _armor =
            new Dictionary<string, Armor>()
            {
                {ArmorArsenal.leather.Name, ArmorArsenal.leather },
                {ArmorArsenal.chainmail.Name, ArmorArsenal.chainmail },
                {ArmorArsenal.plate.Name, ArmorArsenal.plate }
            };
        
        public string fileName { get; set; }
        public List<Unit> Data { get => _units;  set => _units = value; }
        protected override string filePath => @"C:\Users\PC\source\repos\TotalBattle\TotalBattle\Data\" + fileName;

        protected override string[] GetData()
        {
            string[] data = new string[_units.Count];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = $"{_units[i].Name}{DataSplitter}" +
                    $"{_units[i].Health}{DataSplitter}" +
                    $"{_units[i].Damage - _units[i].Weapon.Damage}{DataSplitter}" +
                    $"{_units[i].Weapon.Name}{DataSplitter}" +
                    $"{_units[i].Armor.Name}";
            }
            return data;
        }

        protected override void SetData(string[] data)
        {
            if (string.IsNullOrEmpty(data[0])) return;

            List<Unit> res = new List<Unit>();
            foreach (var info in data)
            {   var unit = info.Split(DataSplitter);
                res.Add(new Unit(unit[0], 
                    ParseToFloat(unit[1]),
                    ParseToFloat(unit[2]),
                    _weapon[unit[3]],
                    _armor[unit[4]]));
            }
            _units.Clear();
            _units = res;
        }
    }
}
