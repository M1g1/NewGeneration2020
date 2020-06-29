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
        private readonly IQueueParser _queueParser;
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(1);
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UploadImageWork(IConsumer consumer, IFileStorage storage, IImageService imgService, IMediaRepository mediaRepo, IQueueParser queueParser)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _imgService = imgService ?? throw new ArgumentNullException(nameof(imgService));
            _mediaRepo = mediaRepo ?? throw new ArgumentNullException(nameof(mediaRepo));
            _queueParser = queueParser ?? throw new ArgumentNullException(nameof(queueParser));;
        }

        public async Task StartAsync()
        {
            _logger.Info("Started " + nameof(UploadImageWork) + ".");

            while (!_cancelTokenSource.IsCancellationRequested)
            {
                var queueNames = _queueParser.ParseQueueNames();
                _logger.Info("Waiting for new message...");
                var msgBody = _consumer.GetFirstMessage<MessageDto>(queueNames[0]);
                _logger.Info("New message received...");

                var isOk = await _imgService.MoveImageFromTempToMainAsync(msgBody);

                _logger.Info(isOk ? "Image uploaded successfully." : "Failed to upload image.");

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