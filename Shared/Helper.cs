namespace Shared
{
    public static class Helper
    {
        public static bool AgeIsNullOrMinusOrEqualZero(int? age)
        {
            return age == null || age <= 0;
        }
    }
}
