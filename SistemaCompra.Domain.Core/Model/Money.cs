using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Domain.Core.Model
{
    public class Money : ValueObject<Money>
    {
        public readonly decimal Value;

        public Money()
                : this(0m)
        {
        }

        public Money(decimal value)
        {
            Value = value;
        }

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        public Money Subtract(Money money)
        {
            return new Money(Value - money.Value);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Value };
        }

        public static explicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static explicit operator Money(decimal value)
        {
            return new Money(value);
        }
    }
}
