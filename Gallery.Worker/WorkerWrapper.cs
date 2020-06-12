﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gallery.Worker.Interfaces;

namespace Gallery.Worker
{
    public class WorkerWrapper
    {
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private readonly IReadOnlyCollection<IWork> _works;

        public WorkerWrapper(params IWork[] works)
        {
            _works = works ?? throw new ArgumentNullException(nameof(works));
        }

        public async Task StartAsync()
        {
            foreach (var work in _works)
            {
                await Task.Factory.StartNew(work.StartAsync,
                    _cancelTokenSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Current);
            }
        }  

        public void Stop()
        {
            _cancelTokenSource.Cancel();
        }
    }
}