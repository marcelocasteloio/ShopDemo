namespace ShopDemo.SharedKernel.Exceptions;

[Serializable]
public class DateTimeShouldBeUtcException
    : Exception
{
    // Constructors
    protected DateTimeShouldBeUtcException(
        System.Runtime.Serialization.SerializationInfo serializationInfo, 
        System.Runtime.Serialization.StreamingContext streamingContext
    )
    {
        throw new NotImplementedException();
    }
    public DateTimeShouldBeUtcException()
    {
        
    }
}
