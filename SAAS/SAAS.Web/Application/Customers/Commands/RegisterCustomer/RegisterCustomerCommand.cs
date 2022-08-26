using AutoMapper;
using SAAS.Core.Entities;
namespace SAAS.Web.Application.Customers.Commands.RegisterCustomer;

public class RegisterCustomerCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public string PaymentDetails { get; set; }
}

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegisterCustomerCommandHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<int> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = new Customer()
        {
            Name = request.Name,
            Address = request.Address,
            ContactNumber = request.ContactNumber,
            PaymentDetails = request.PaymentDetails
        };

        _context.Customers.Add(Customer);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
