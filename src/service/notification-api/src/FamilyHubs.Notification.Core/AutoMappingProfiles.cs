using AutoMapper;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Data.Entities;

namespace FamilyHubs.Notification.Core;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<MessageDto, SentNotification>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Notified, opt => opt.MapFrom(src => CreateNotifiedList(src.Id,src.NotificationEmails)))
            .ForMember(dest => dest.TokenValues, opt => opt.MapFrom(src => CreateTokenValues(src.Id, src.TemplateTokens)));

        CreateMap<SentNotification, MessageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.NotificationEmails, opt => opt.MapFrom(src => src.Notified.Select(x => x.Value)))
            .ForMember(dest => dest.TemplateTokens, opt => opt.MapFrom(src => CreateTokenDictionary(src.TokenValues)));

    }

    private List<Notified> CreateNotifiedList(long id, List<string> notifications)
    {
        List<Notified> notifieds = new List<Notified>();
        foreach (var notified in notifications) 
        {
            notifieds.Add( new Notified { NotificationId = id, Value = notified });
        }
        return notifieds;
    }

    private List<TokenValue> CreateTokenValues(long id, Dictionary<string,string?> tokens)
    {
        List<TokenValue> tokenValues = new List<TokenValue>();
        foreach (var token in tokens) 
        {
            tokenValues.Add(new TokenValue
            {
                NotificationId = id,
                Key = token.Key,
                Value = token.Value
            });
        }
        return tokenValues;
    }

    private static Dictionary<string,string?> CreateTokenDictionary(IList<TokenValue> tokenValues)
    {
        var keyValuePairs = new Dictionary<string, string?>();
        foreach (var  token in tokenValues)
        {
            keyValuePairs[token.Key] = token.Value;
        }
        return keyValuePairs;
    }
}
