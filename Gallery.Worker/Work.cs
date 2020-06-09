using System;
using System.IO;
using System.Threading.Tasks;
using FileStorageProvider.Interfaces;
using Gallery.DAL;
using Gallery.MessageQueues;
using Gallery.Service;
using Gallery.Service.Contract;

namespace Gallery.Worker
{
    public class Work : IWork
    {
        private readonly IConsumer _consumer;
        private readonly IFileStorage _storage;
        private readonly IImageService _imgService;
        private readonly IMediaRepository _mediaRepo;
        public Work(IConsumer consumer, IFileStorage storage, IImageService imgService, IMediaRepository mediaRepo)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _imgService = imgService ?? throw new ArgumentNullException(nameof(imgService));
            _mediaRepo = mediaRepo ?? throw new ArgumentNullException(nameof(mediaRepo));
        }

        public async Task Upload()
        {
            _consumer.SetFormat(new Type[]
            {
                typeof(MessageDto)
            });

            var body = _consumer.GetFirstMessageBody();;
            var messageDto = (body as MessageDto) ?? throw new ArgumentNullException(nameof(body));

            if (!File.Exists(messageDto.TempPath))
                throw new FileNotFoundException("File not found", messageDto.TempPath);

            var isMediaUploadAttemptExist = await _mediaRepo.IsMediaUploadAttemptExistByLabelAndProgressStatus(messageDto.Label, true);
            if (isMediaUploadAttemptExist)
            {
                var mediaUploadAttempt = await _mediaRepo.GetMediaUploadAttemptByLabelAndProgressStatus(messageDto.Label, true);
                var newUploadAttempt = mediaUploadAttempt;
                newUploadAttempt.IsInProgress = false;
                newUploadAttempt.IsSuccess = true;
                await _mediaRepo.UpdateMediaUploadAttemptAsync(mediaUploadAttempt, newUploadAttempt);
                var fileBytes = _storage.ReadBytes(messageDto.TempPath);
                await _imgService.UploadImageAsync(messageDto.UserId, fileBytes, messageDto.Path);
            }
        }
    }
}