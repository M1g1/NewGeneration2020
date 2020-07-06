using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IImageService _imgService;
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(1);
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UploadImageWork(IConsumer consumer, IImageService imgService)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _imgService = imgService ?? throw new ArgumentNullException(nameof(imgService));
        }

        public async Task StartAsync()
        {
            _logger.Info("Started " + nameof(UploadImageWork) + ".");

            var queueDictionary = Parser.ParseQueueNames();

            while (!_cancelTokenSource.IsCancellationRequested)
            {
                _consumer.Consume<MessageDto>(
                    queueDictionary[QueueType.UploadImage],
                    async msg => await _imgService.MoveImageFromTempToMainAsync(msg));
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