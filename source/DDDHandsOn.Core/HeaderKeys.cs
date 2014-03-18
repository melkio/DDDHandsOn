using System;

namespace DDDHandsOn.Core
{
    public static class HeaderKeys
    {
        public static readonly String Who = "who";
        public static readonly String When = "when";
        public static readonly String Sequence = "when/ticks";
        public static readonly String CorrelationId = "correlation";
        public static readonly String AggregateType = "type";

        public static readonly String[] Mandatory = new String[] { Who, When, CorrelationId, AggregateType };
    }
}
