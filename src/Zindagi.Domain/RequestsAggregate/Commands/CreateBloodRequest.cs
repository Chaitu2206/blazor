using MediatR;
using Zindagi.Domain.RequestsAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate.Commands
{
    public class CreateBloodRequest : IRequest<Result<BloodRequestDto>>
    {
        public long RequestorId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public int DonationType { get; set; }
        public int BloodGroup { get; set; }
        public int Priority { get; set; }
        public double QuantityInUnits { get; set; }
    }
}
