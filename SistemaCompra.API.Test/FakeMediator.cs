using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SistemaCompra.API.Test
{
    internal class FakeMediator : IMediator
    {
        Task _getSendValueTask;
        public FakeMediator(Task getSendValueTask)
        {
            _getSendValueTask = getSendValueTask;
        }
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var r = _getSendValueTask as Task<TResponse>;
            r.Start();
            return r;
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            var r = _getSendValueTask as Task<object>;
            r.Start();
            return r;
        }
    }
}
