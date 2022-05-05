using PapaBlog.Shared.Utilities.Results.ComplexTypes;

namespace PapaBlog.Shared.Dtos
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
    }
}
