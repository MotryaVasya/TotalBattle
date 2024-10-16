namespace TotalBattle.Interfaces
{
    interface IUnit
    {
        #region properties
        string Name { get; }
        float Health { get; set; }
        bool IsBlocks { get; }
        float Damage { get; }
        IArmor Armor { get; }
        IWeapon Weapon { get; }
        #endregion

        #region methods
        bool Attack(IUnit enemy);

        void Block();
        void Heal(float value);
        #endregion
    }
}
