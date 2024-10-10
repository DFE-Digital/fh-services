namespace FamilyHubs.ServiceDirectory.Shared.Display;

public static class AgeDisplayExtensions
{
    public const int TwentyFivePlus = 127;
    public static string AgeToString(int age)
    {
        return age == TwentyFivePlus ? "25+" : age.ToString();
    }
}
