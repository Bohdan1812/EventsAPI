﻿using Domain.Common.Models;

namespace Domain.InviteAggregate.ValueObjects
{
    public sealed class InviteId : ValueObject
    {
        public Guid Value { get; }

        private InviteId(Guid value)
        {
            Value = value;
        }

        public static InviteId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
