using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DDDHandsOn.Core.DomainModel
{
    class DomainEventHeadersConverter : JsonConverter
    {
        public override Boolean CanConvert(Type objectType)
        {
            return objectType == typeof(DomainEventHeaders);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            var headers = serializer.Deserialize<Dictionary<String, Object>>(reader);

            var domainEventHeaders = new DomainEventHeaders();
            domainEventHeaders.CorrelationId = Guid.Parse(headers[HeaderKeys.CorrelationId].As<String>());
            domainEventHeaders.Who = (String)headers[HeaderKeys.Who];
            domainEventHeaders.When = (DateTime)headers[HeaderKeys.When];
            domainEventHeaders.AggregateType = (String)headers[HeaderKeys.CorrelationId];
            domainEventHeaders.CopyFrom(headers);

            return domainEventHeaders;
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            var message = (DomainEventHeaders)value;

            var headers = new Dictionary<String, Object>();
            message.CopyTo(headers);

            serializer.Serialize(writer, headers);
            //writer.WriteStartObject();
            //writer.WritePropertyName("Headers");
            //writer.WriteValue();
            //writer.WriteEndObject();
        }
    }
}
