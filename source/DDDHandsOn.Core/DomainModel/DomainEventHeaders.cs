using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDHandsOn.Core.DomainModel
{
    [JsonConverter(typeof(DomainEventHeadersConverter))]
    public class DomainEventHeaders
    {
        private readonly Dictionary<String, String> _headers;
        private readonly Dictionary<String, Object> _customHeaders;

        public IReadOnlyDictionary<String,Object> CustomHeaders
        {
            get { return new ReadOnlyDictionary<string, object>(_customHeaders); }
        } 
        
        public Guid CorrelationId
        {
            get
            {
                String correlationId = _headers[HeaderKeys.CorrelationId];
                return Guid.Parse(correlationId);
            }
            set 
            { 
                _headers[HeaderKeys.CorrelationId] = value.ToString(); 
            }
        }

        public String Who
        {
            get
            {
                return _headers[HeaderKeys.Who];
            }
            set
            {
                _headers[HeaderKeys.Who] = value;
            }
        }

        public DateTime When
        {
            get
            {
                var when = _headers[HeaderKeys.When];
                return DateTime.ParseExact(when, "yyyyMMddHHmmsss", null);
            }
            set
            {
                var when = value;
                _headers[HeaderKeys.When] = when.ToString("yyyyMMddHHmmsss");
                _headers[HeaderKeys.Sequence] = when.Ticks.ToString();
            }
        }

        public Int64 Sequence
        {
            get { return Int64.Parse(_headers[HeaderKeys.Sequence]); }
        }

        public String AggregateType
        {
            get
            {
                return _headers[HeaderKeys.AggregateType];
            }
            set
            {
                _headers[HeaderKeys.AggregateType] = value;
            }
        }

        public IEnumerable<String> CustomKeys
        {
            get { return _customHeaders.Select(s => s.Key); }
        }

        public DomainEventHeaders()
        {
            _headers = new Dictionary<String, String>();
            _customHeaders = new Dictionary<String, Object>();
        }

        public Boolean ContainsKey(String key)
        {
            return _headers.ContainsKey(key) || _customHeaders.ContainsKey(key);
        }

        public T Get<T>(String key)
        {
            return (T) _customHeaders[key];
        }

        public void Set<T>(String key, T value)
        {
            _customHeaders[key] = value;
        }

        public void CopyFrom(IDictionary<String, Object> source)
        {
            // TODO : togliere header "tick"
            source
                .Where(s => !HeaderKeys.Mandatory.Contains(s.Key) )
                .ToDictionary(s => s.Key, s => s.Value)
                .CopyTo(_customHeaders);
        }

        public void CopyTo(IDictionary<String, Object> destination)
        {
            destination[HeaderKeys.CorrelationId] = CorrelationId;
            destination[HeaderKeys.When] = When;
            destination[HeaderKeys.Who] = Who;
            destination[HeaderKeys.Sequence] = Sequence;
            destination[HeaderKeys.AggregateType] = AggregateType;

            _customHeaders.CopyTo(destination);
        }
    }
}
