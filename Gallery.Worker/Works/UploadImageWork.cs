using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileStorageProvider.Interfaces;
using Gallery.DAL;
using Gallery.MessageQueues;
using Gallery.Service;
using Gallery.Service.Contract;
using Gallery.Worker.Interfaces;
using NLog;

namespace Gallery.Worker.Works
{
    public class UploadImageWork : IWork
    {
        private readonly IConsumer _consumer;
        private readonly IFileStorage _storage;
        private readonly IImageService _imgService;
        private readonly IMediaRepository _mediaRepo;
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(1);
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UploadImageWork(IConsumer consumer, IFileStorage storage, IImageService imgService, IMediaRepository mediaRepo)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _imgService = imgService ?? throw new ArgumentNullException(nameof(imgService));
            _mediaRepo = mediaRepo ?? throw new ArgumentNullException(nameof(mediaRepo));
        }

        public async Task StartAsync()
        {
            _logger.Info("Started " + nameof(UploadImageWork) + ".");

            while (!_cancelTokenSource.IsCancellationRequested)
            {
                var queuePaths = Parser.ParseQueuePaths();
                _logger.Info("Waiting for new message...");
                var msgBody = _consumer.GetFirstMessage<MessageDto>(queuePaths[0]);
                _logger.Info("New message received...");
                if (!File.Exists(msgBody.TempPath))
                    throw new FileNotFoundException("File not found", msgBody.TempPath);

                var isMediaUploadAttemptExist =
                    await _mediaRepo.IsMediaUploadAttemptExistByLabelAndProgressStatus(msgBody.Label, true);
                if (isMediaUploadAttemptExist)
                {
                    var mediaUploadAttempt =
                        await _mediaRepo.GetMediaUploadAttemptByLabelAndProgressStatus(msgBody.Label, true);
                    var newUploadAttempt = mediaUploadAttempt;
                    newUploadAttempt.IsInProgress = false;
                    newUploadAttempt.IsSuccess = true;
                    await _mediaRepo.UpdateMediaUploadAttemptAsync(mediaUploadAttempt, newUploadAttempt);
                    var fileBytes = _storage.ReadBytes(msgBody.TempPath);
                    await _imgService.UploadImageAsync(msgBody.UserId, fileBytes, msgBody.Path);
                    _logger.Info("Image uploaded successfully.");
                }
                else
                {
                    _logger.Info("Failed to upload image.");
                }

                await Task.Delay(_delay);
            }
        }

        public void Stop()
        {
            _logger.Info("Stopping " + nameof(UploadImageWork) + "...");
            _cancelTokenSource.Cancel();
            _logger.Info("Stopped " + nameof(UploadImageWork) + "successfully.");
        }
    }
}