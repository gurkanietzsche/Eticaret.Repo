using Microsoft.AspNetCore.SignalR;

public class FavoritesHub : Hub
{
    public async Task UpdateFavoriteCount(int count)
    {
        // Tüm istemcilere favori sayısını gönder
        await Clients.All.SendAsync("UpdateFavoriteCount", count);
    }
}