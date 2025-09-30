namespace ComputerSystem.Common
{
    public static class ComponentExtensions
    {
        public static string ToShortString(this Component c)
        {
            return $"{c.Manufacturer} {c.Model}";
        }
    }
}
