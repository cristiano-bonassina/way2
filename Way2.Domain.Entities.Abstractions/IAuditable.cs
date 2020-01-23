using System;

namespace Way2.Domain.Entities.Abstractions
{
    public interface IAuditable
    {
        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset? ModifiedAt { get; set; }        
    }
}
