using System.Linq;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using TicTacFoo.Domain.Dto;
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;

namespace TicTacFoo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            // TODO: Move to separate mapping config classes
            config.NewConfig<Game, GamesAvailableDto>()
                .Map(dest => dest.CanJoin,
                    src => src.Players.Any(p => p == null))
                .Map(dest => dest.PlayerCount,
                    src => src.Players.Count(p => p != null));
            config.NewConfig<Player, PlayersAvailableDto>()
                .Map(dest => dest.Status,
                    src => MapPlayerStatus(src));
            services.AddSingleton(config);
            services.AddSingleton<IMapper, ServiceMapper>();
            return services;
        }
        private static PlayerStatus MapPlayerStatus(Player player) => player switch
        {
            {Name: null} => PlayerStatus.Registering,
            {GameId: null} => PlayerStatus.Available,
            {} when !string.IsNullOrEmpty(player.GameId) => PlayerStatus.Busy,
            _ => PlayerStatus.Registering
        };
    }
}