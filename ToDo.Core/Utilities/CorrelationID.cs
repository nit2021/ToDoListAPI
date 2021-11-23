using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoAPI.Core.Utilities
{
    public class CorrelationID
    {
        private readonly Dictionary<string, string> idList = new Dictionary<string, string>();
        private const string CorrelationIdKey = "x-correlation-id";
        public CorrelationID()
        {
            idList[CorrelationIdKey] = Guid.NewGuid().ToString();
        }
        public IReadOnlyDictionary<string, object> GetCurrentID()
        {
            return idList.ToDictionary(k => k.Key, k => (object)k.Value);
        }
        public object GetID()
        {
            return idList[CorrelationIdKey];
        }
        public void Update(string key, string value)
        {
            idList[key] = value;
        }
    }

}
