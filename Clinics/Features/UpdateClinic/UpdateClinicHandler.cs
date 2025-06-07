using Clinics.Domain;
using FluentValidation;
using SharedKernal.Mediator.Interfaces;
using Marten;

namespace Clinics.Features.UpdateClinic
{
    public class UpdateClinicHandler : ICommandHandler<UpdateClinicCommand, bool>
    {
        private readonly IDocumentSession _session;
        private readonly IValidator<UpdateClinicCommand> _validator;

        public UpdateClinicHandler(
            IDocumentSession session,
            IValidator<UpdateClinicCommand> validator)
        {
            _session = session;
            _validator = validator;
        }

        public async Task<bool> HandleAsync(
            UpdateClinicCommand command, 
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var clinic = await _session.LoadAsync<Clinic>(command.Id, cancellationToken);
            if (clinic == null) return false;

            clinic.Name = command.Name;
            clinic.Address = command.Address;
            clinic.PhoneNumber = command.PhoneNumber;
            clinic.UpdatedAt = DateTime.UtcNow;

            _session.Update(clinic);
            await _session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}