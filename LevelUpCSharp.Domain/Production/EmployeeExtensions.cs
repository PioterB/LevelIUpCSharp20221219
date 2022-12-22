namespace LevelUpCSharp.Production
{
    public static class EmployeeExtensions
    {
        public static IEmployee AsRabbit(this IEmployee source)
        {
            return new Rabbit(source);
        }
    }
}