namespace TotalBattle.Interfaces
{
    interface IWeapon
    {
        string Name { get; }
        float Damage { get; }
        Weapons Type { get; }
    }
}
