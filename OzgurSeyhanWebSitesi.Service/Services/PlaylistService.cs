using AutoMapper;
using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Bussinies.Services
{
    public class PlaylistService : GenericService<Playlist>, IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IYoutubeApiService _youtubeApiService;
        private readonly IMapper _mapper;
        private readonly PlaylistCacheService _cacheService;

        public PlaylistService(
            IGenericRepository<Playlist> repository,
            IUnitOfWorks unitOfWorks,
            IPlaylistRepository playlistRepository,
            IYoutubeApiService youtubeApiService,
            IMapper mapper,
            PlaylistCacheService cacheService) : base(repository, unitOfWorks)
        {
            _playlistRepository = playlistRepository;
            _youtubeApiService = youtubeApiService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PlaylistDto> CreateFromYouTubePlaylistAsync(string playlistUrl, int ogretmenId)
        {
            try
            {
                // 1. Playlist ID'sini çıkar
                var playlistId = _youtubeApiService.ExtractPlaylistId(playlistUrl);

                // 2. Bu playlist daha önce eklenmiş mi kontrol et
                var existingPlaylist = await _playlistRepository.GetByPlaylistIdAsync(playlistId);
                if (existingPlaylist != null)
                {
                    return _mapper.Map<PlaylistDto>(existingPlaylist);
                }

                // 3. YouTube'dan sadece playlist bilgisi için ilk videoyu çek
                var playlistVideos = await _youtubeApiService.GetPlaylistVideosAsync(playlistUrl);
                var firstVideo = playlistVideos.FirstOrDefault();

                // 4. SADECE PLAYLIST KAYDET - VİDEOLARI KAYDETME
                var playlist = new Playlist
                {
                    PlaylistId = playlistId,
                    Baslik = firstVideo?.Baslik ?? "YouTube Playlist",
                    OgretmenId = ogretmenId,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                await _repository.AddAsync(playlist);
                await _unitOfWorks.CommitAsync();

                return _mapper.Map<PlaylistDto>(playlist);
            }
            catch (Exception ex)
            {
                throw new Exception($"Playlist oluşturulamadı: {ex.Message}", ex);
            }
        }

        public async Task<PlaylistWithVideosDto> GetPlaylistWithVideosAsync(int playlistId)
        {
            try
            {
                // 1. Veritabanından playlist'i getir
                var playlist = GetByIdAsync(playlistId);
                if (playlist == null)
                    throw new Exception("Playlist bulunamadı");

                // 2. CACHE KONTROLÜ - Önce cache'den bak
                var cachedVideos = _cacheService.GetCachedVideos(playlist.PlaylistId);
                
                List<YoutubeVideoDto> videos;
                
                if (cachedVideos != null)
                {
                    // Cache'den geldi, YouTube API kullanmadık!
                    videos = cachedVideos;
                }
                else
                {
                    // Cache'de yok, YouTube'dan çek
                    var playlistUrl = $"https://www.youtube.com/playlist?list={playlist.PlaylistId}";
                    var youtubeVideos = await _youtubeApiService.GetPlaylistVideosAsync(playlistUrl);

                    // DTO'ya dönüştür
                    videos = youtubeVideos.Select(v => new YoutubeVideoDto
                    {
                        VideoId = v.VideoId,
                        Baslik = v.Baslik,
                        Url = v.Url,
                        OgretmenId = playlist.OgretmenId
                    }).ToList();

                    // Cache'e kaydet (60 dakika)
                    _cacheService.SetCachedVideos(playlist.PlaylistId, videos);
                }

                // 3. DTO'ya map et
                var playlistDto = _mapper.Map<PlaylistWithVideosDto>(playlist);
                playlistDto.Videos = videos;

                return playlistDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Playlist videoları alınamadı: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PlaylistDto>> GetByOgretmenIdAsync(int ogretmenId)
        {
            var playlists = await _playlistRepository.GetByOgretmenIdAsync(ogretmenId);
            return _mapper.Map<IEnumerable<PlaylistDto>>(playlists);
        }
    }
}
