﻿using System.Net.Http.Headers;
using TuneSpace.Core.Interfaces.IClients;

namespace TuneSpace.Infrastructure.Clients;

internal class SpotifyClient(HttpClient httpClient) : ISpotifyClient
{
    async Task<HttpResponseMessage> ISpotifyClient.GetToken(FormUrlEncodedContent parameters)
    {
        const string tokenEndpoint = "https://accounts.spotify.com/api/token";
        
        return await httpClient.PostAsync(tokenEndpoint, parameters);
    }
    
    async Task<HttpResponseMessage> ISpotifyClient.GetUserInfo(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await httpClient.GetAsync("https://api.spotify.com/v1/me");
    }

    async Task<HttpResponseMessage> ISpotifyClient.GetUserTopArtists(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await httpClient.GetAsync("https://api.spotify.com/v1/me/top/artists?time_range=short_term&limit=5&offset=0");
    }
    
    async Task<HttpResponseMessage> ISpotifyClient.GetUserTopSongs(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await httpClient.GetAsync("https://api.spotify.com/v1/me/top/tracks?time_range=short_term&limit=5&offset=0");
    }
    
    async Task<HttpResponseMessage> ISpotifyClient.GetSongsBySearch(string token, string search)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        return await httpClient.GetAsync($"https://api.spotify.com/v1/search?q={search}&type=track&limit=5");
    }
    
    async Task<HttpResponseMessage> ISpotifyClient.CreatePlaylist(string token, string userId, StringContent requestContent)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        return await httpClient.PostAsync($"https://api.spotify.com/v1/users/{userId}/playlists", requestContent);
    }

    async Task<HttpResponseMessage> ISpotifyClient.GetArtist(string token, string artistId)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await httpClient.GetAsync($"https://api.spotify.com/v1/artists/{artistId}");
    }
}