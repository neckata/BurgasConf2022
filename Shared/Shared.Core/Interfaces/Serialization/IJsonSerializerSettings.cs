using Newtonsoft.Json;

namespace OnlineShop.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}