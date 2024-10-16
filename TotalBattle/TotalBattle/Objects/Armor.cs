using System.Collections.Generic;
using TotalBattle.Interfaces;

namespace TotalBattle.Objects
{
    class Armor : IArmor
    {
        #region fields
        private string _name;
        private Armors _type;
        private float _defense;
        #endregion

        public string Name => _name;

        public Armors Type => _type;

        public float Defense => _defense;

        public Armor(string name, float defense, Armors type)
        {
            _name = name;
            _defense = defense;
            _type = type;
        }
    }
}
