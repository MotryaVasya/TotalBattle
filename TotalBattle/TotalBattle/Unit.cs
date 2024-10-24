using System;
using System.Collections.Generic;
using TotalBattle.Interfaces;
using TotalBattle.Objects;

namespace TotalBattle
{
    class Unit : IUnit
    {
        public event Action<IUnit> onKill;

        #region fields
        private string _name;
        private float _health;
        private float _maxHealth;
        private bool _block;
        private float _baseDamage;
        private IWeapon _weapon;
        private IArmor _armor;

        private Dictionary<Armors, Dictionary<Weapons, float>> _resistances =
            new Dictionary<Armors, Dictionary<Weapons, float>>()
            {
                { Armors.light, new Dictionary<Weapons, float>()
                {
                    { Weapons.slashing, 1.2f },
                    { Weapons.piercing, 1f },
                    { Weapons.bludgeoning, 0.8f }
                }},
                { Armors.medium, new Dictionary<Weapons, float>()
                {
                    { Weapons.slashing, 0.8f },
                    { Weapons.piercing, 1.2f },
                    { Weapons.bludgeoning, 1f }
                }},
                { Armors.heavy, new Dictionary<Weapons, float>()
                {
                    { Weapons.slashing, 0.8f },
                    { Weapons.piercing, 1f },
                    { Weapons.bludgeoning, 1.2f }
                }}

            };
        #endregion

        #region properties
        
        public string Name { get => _name; }
        public float Health { get => _health; set => _health = value; }

        public bool IsBlocks { get => _block; }

        public float Damage
        {
            get
            {
                return _baseDamage + Weapon.Damage;
            }
        }

        public IWeapon Weapon => _weapon;
        public IArmor Armor => _armor;
        #endregion

        #region constructors
        public Unit(string name, float health, float damage, IWeapon weapon,
            IArmor armor)
        {
            _name = name;
            _health = health;
            _baseDamage = damage;
            _weapon = weapon;
            _armor = armor;
            _maxHealth = health;
        }

        public Unit(string name) : this(name, 25, 5, WeaponArsenal.sword,
            ArmorArsenal.leather) { }
        #endregion
        public bool Attack(IUnit enemy)
        {
            if ((IsBlocks) || (enemy.IsBlocks) || (Health <= 0f)) return false;

            enemy.Health -= RealDamage(enemy.Armor);

            if (enemy.Health <= 0f)
            {
                enemy.Health = 0f;
                onKill?.Invoke(enemy);
            }
            return true;
        }
        public void Block()
        {
            Random rnd = new Random();
            _block = rnd.Next(0, 100) <= 30;
        }
        public void Heal(float value)
        {
            if ((Health <= 0f) || (Health >= _maxHealth)) return;
            
            Health += value;
            if (Health > _maxHealth) Health = _maxHealth;
        }
        private float RealDamage(IArmor armor)
        {
            return _resistances[armor.Type][Weapon.Type];
        }
    }
}
