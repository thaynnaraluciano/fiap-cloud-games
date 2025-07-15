using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Api.Utils
{
    /* Esta classe é um comportamento de pipeline (pipeline behavior), 
     * usado com o MediatR e FluentValidation para aplicar validações automáticas nas requisições (TRequest)
     * antes que elas cheguem ao handler.*/
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull 
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                List<ValidationFailure> errors = _validator.Validate(context).Errors;
                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }
            }

            return next();
        }
    }
}
