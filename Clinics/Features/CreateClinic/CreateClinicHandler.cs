using Clinics.Domain;
using Clinics.Infrastructure;
using FluentValidation;
using SharedKernal.Mediator.Interfaces;

namespace Clinics.Features.CreateClinic
{
    public class CreateClinicHandler : ICommandHandler<CreateClinicCommand, Guid>
    {
        private readonly IClinicRepository _repository;
        private readonly IValidator<CreateClinicCommand> _validator;

        public CreateClinicHandler(
            IClinicRepository repository, 
            IValidator<CreateClinicCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Guid> HandleAsync(CreateClinicCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var clinic = new Clinic
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                // Example of using ExtendedData for flexible schema
                ExtendedData = new Dictionary<string, object>
                {
                    ["createdBy"] = "system",
                    ["source"] = "api"
                }
            };

            return await _repository.CreateAsync(clinic, cancellationToken);
        }
    }
}