using Agatha.Common;
using Agatha.ServiceLayer;
using DDDHandsOn.Core.DomainModel;
using DDDHandsOn.Core.Persistence.ComponentModel;
using DDDHandsOn.Core.Security.ComponentModel;
using System;

namespace DDDHandsOn.Core.Command
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse, new()
    {
        private readonly IOperationContext _context;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseRequestHandler(IOperationContext context, IUnitOfWorkFactory factory)
        {
            _context = context;
            _unitOfWork = factory.CreateUnitOfWork();
        }

        public override void BeforeHandle(TRequest request)
        {
            base.BeforeHandle(request);

            //var identity = _context.GetCurrentUser();
            //identity.ThrowSecurityExceptionIfCantExecute<TRequest>();
        }

        public override Response Handle(Request request)
        {
            var typedRequest = (TRequest)request;
            
            var response = (TResponse)base.Handle(request);
            response.CorrelationId = typedRequest.CorrelationId;

            _unitOfWork.Commit(typedRequest.CorrelationId, _context);
            
            return response;
        }

        protected TAggregateRoot Get<TAggregateRoot>(String id) where TAggregateRoot : IAggregateRoot
        {
            return _unitOfWork.Get<TAggregateRoot>(id);
        }

        protected void Add<TAggregateRoot>(TAggregateRoot aggregate) where TAggregateRoot : IAggregateRoot
        {
            _unitOfWork.Add(aggregate);
        }

        protected override void DisposeManagedResources()
        {
            _unitOfWork.Dispose();
        }
    }
}
