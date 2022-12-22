namespace DependencyChecker.Tests;

public class FailedValidation
{
    public Severity Severity { get; }
    public Type ServiceType { get; }
    
    public string Message { get; }

    public FailedValidation(Severity severity, Type serviceType, string message)
    {
        Severity = severity;
        ServiceType = serviceType;
        Message = message;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not FailedValidation fv)
            return false;

        return ServiceType == fv.ServiceType && Message == fv.Message && Severity == fv.Severity;
    }

    public override int GetHashCode()
    {
        return Message.GetHashCode() ^ ServiceType.GetHashCode();
    }
}