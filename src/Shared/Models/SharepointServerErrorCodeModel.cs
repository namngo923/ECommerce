using SPSVN.Shared.Exceptions;

namespace SPSVN.Shared.Models;
public class SharepointServerErrorCodeModel
{
    public int HResult { get; set; }
    public int ServerErrorCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public SharepointServerErrorCodeModel()
    {
    }

    public SharepointServerErrorCodeModel(SpServerException exception)
    {
        HResult = exception.HResult;
        ServerErrorCode = exception.ServerErrorCode;
        Message = exception.Message;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
            return false;

        var other = (SharepointServerErrorCodeModel)obj;
        return this.HResult == other.HResult && this.ServerErrorCode == other.ServerErrorCode;
    }

    public static bool operator ==(SharepointServerErrorCodeModel o1, SharepointServerErrorCodeModel o2)
    {
        return o1 is null ? o2 is null : o1.Equals(o2);
    }

    public static bool operator !=(SharepointServerErrorCodeModel o1, SharepointServerErrorCodeModel o2)
    {
        return !(o1 == o2);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(HResult, ServerErrorCode);
    }
}
