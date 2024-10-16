using TotalBattle.Interfaces;

namespace TotalBattle.Objects
{
    class Weapon : IWeapon
    {
        #region fields
        private string _name;
        private float _baseDamage;
        private Weapons _type;
        #endregion

        public string Name => _name;

        public float Damage => _baseDamage;

        public Weapons Type => _type;

        public Weapon(string name, float damage, Weapons type)
        {
            _name = name;
            _baseDamage = damage;
            _type = type;
        }
    }
}
