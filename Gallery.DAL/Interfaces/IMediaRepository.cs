﻿using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public interface IMediaRepository
    {
        Task<bool> IsMediaExistByPathAsync(string path);
        Task AddMediaToDatabaseAsync(Media media);
        Task<Media> GetMediaByPathAsync(string path);
        Task UpdateMediaAsync(Media oldMedia, Media newMedia);

        Task<bool> IsMediaTypeExistAsync(string extension);
        Task AddMediaTypeToDatabaseAsync(MediaType mediaType);
        Task<MediaType> GetMediaTypeAsync(string extension);

        Task AddMediaUploadAttemptToDatabaseAsync(MediaUploadAttempt mediaUploadAttempt);
        Task<bool> IsMediaUploadAttemptExistByLabelAndProgressStatus(string label, bool progressStatus);
        Task<MediaUploadAttempt> GetMediaUploadAttemptByLabelAndProgressStatus(string label, bool progressStatus);
        Task UpdateMediaUploadAttemptAsync(MediaUploadAttempt oldMediaAttempt, MediaUploadAttempt newMediaAttempt);
    }
}