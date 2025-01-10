using FamilyHubs.SharedKernel.Razor.ErrorNext;

namespace FamilyHubs.SharedKernel.Razor.Header;

public interface IHasErrorStatePageModel
{
    public IErrorState Errors { get; }
}
