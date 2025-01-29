using FamilyHubs.SharedKernel.Services.Sanitizers;

namespace FamilyHubs.SharedKernel.Factories;

public static class SanitizerFactory
{
    public static IStringSanitizer CreateDedsTextSanitizer()
    {
        return new StringSanitizer()
            .RemoveHtml()
            .RemoveJs()
            .RemoveProfanity();
    }
}