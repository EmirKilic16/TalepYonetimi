using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TalepYonetimi.Domain.Entities;
using TalepYonetimi.Application.DTOs.Kullanicilar;

namespace TalepYonetimi.Web.Helpers
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return value == null ? default : JsonConvert.DeserializeObject<T>(value, settings);
        }

        public static KullaniciListeDto? KullaniciGetir(this ISession session)
        {
            return session.Get<KullaniciListeDto>("OturumAcmisKullanici");
        }
    }
}
