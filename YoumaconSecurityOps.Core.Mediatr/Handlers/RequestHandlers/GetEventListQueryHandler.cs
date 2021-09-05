using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.EventStore.Storage;
using YoumaconSecurityOps.Core.Mediatr.Queries;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers
{
    internal sealed class GetEventListQueryHandler : RequestHandler<GetEventListQuery, IAsyncEnumerable<EventReader>>
    {
        private readonly IEventStoreRepository _eventStore;
        
        private readonly ILogger<GetEventListQueryHandler> _logger;
        
        public GetEventListQueryHandler(IEventStoreRepository eventStore, IEventStoreRepository events, ILogger<GetEventListQueryHandler> logger, IMapper mapper, IMediator mediator)
        {
            _eventStore = eventStore;
            _logger = logger;
        }
        

        protected override IAsyncEnumerable<EventReader> Handle(GetEventListQuery request)
        {
            //_logger.LogInformation("User : {userName} Queried the event list", Guid.NewGuid().ToString());

            return _eventStore.GetAll();
        }
    }
}
