using System;
using System.ServiceProcess;


namespace Gallery.Worker
{
    partial class GalleryWorkerService : ServiceBase
    {
        private readonly WorkerWrapper _workerWrapper;

        public GalleryWorkerService(WorkerWrapper workerWrapper)
        {
            _workerWrapper = workerWrapper ?? throw new ArgumentNullException(nameof(workerWrapper));
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
             await _workerWrapper.StartAsync();
        }

        protected override void OnStop()
        {
            _workerWrapper.Stop();
        }
    }
}
