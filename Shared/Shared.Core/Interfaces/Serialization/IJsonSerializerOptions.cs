using System.Text.Json;

namespace OnlineShop.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; }
    }
}