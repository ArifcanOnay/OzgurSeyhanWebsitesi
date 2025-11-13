using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Bussinies.Services
{
    public class YoutubeVideoService : GenericService<YoutubeVideo>, IYoutubeVideoService
    {
        private readonly IYoutubeApiService _youtubeApiService;
        private readonly IMapper _mapper;

        public YoutubeVideoService(
            IGenericRepository<YoutubeVideo> repository, 
            IUnitOfWorks unitOfWorks,
            IYoutubeApiService youtubeApiService,
            IMapper mapper) : base(repository, unitOfWorks)
        {
            _youtubeApiService = youtubeApiService;
            _mapper = mapper;
        }

        public async Task<YoutubeVideoDto> CreateFromYouTubeUrlAsync(string youtubeUrl, int ogretmenId)
        {
            try
            {
                // 1. YouTube API'den video bilgilerini çek
                var videoInfo = await _youtubeApiService.GetVideoInfoAsync(youtubeUrl);

                // 2. YoutubeVideo entity'si oluştur
                var youtubeVideo = new YoutubeVideo
                {
                    Baslik = videoInfo.Baslik,
                    Url = videoInfo.Url,
                    VideoId = videoInfo.VideoId,
                    OgretmenId = ogretmenId,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                // 3. Veritabanına kaydet
                await _repository.AddAsync(youtubeVideo);
                await _unitOfWorks.CommitAsync();

                // 4. DTO'ya map et ve döndür
                return _mapper.Map<YoutubeVideoDto>(youtubeVideo);
            }
            catch (Exception ex)
            {
                throw new Exception($"YouTube'dan video kaydedilemedi: {ex.Message}", ex);
            }
        }

        public async Task<List<YoutubeVideoDto>> CreateFromPlaylistAsync(string playlistUrl, int ogretmenId)
        {
            try
            {
                // 1. YouTube API'den playlist'teki tüm videoları çek
                var playlistVideos = await _youtubeApiService.GetPlaylistVideosAsync(playlistUrl);

                var savedVideos = new List<YoutubeVideoDto>();

                // 2. Her video için entity oluştur ve kaydet
                foreach (var videoInfo in playlistVideos)
                {
                    var youtubeVideo = new YoutubeVideo
                    {
                        Baslik = videoInfo.Baslik,
                        Url = videoInfo.Url,
                        VideoId = videoInfo.VideoId,
                        OgretmenId = ogretmenId,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };

                    await _repository.AddAsync(youtubeVideo);
                    savedVideos.Add(_mapper.Map<YoutubeVideoDto>(youtubeVideo));
                }

                // 3. Tüm değişiklikleri tek seferde kaydet
                await _unitOfWorks.CommitAsync();

                return savedVideos;
            }
            catch (Exception ex)
            {
                throw new Exception($"YouTube Playlist'ten videolar kaydedilemedi: {ex.Message}", ex);
            }
        }
    }
}
