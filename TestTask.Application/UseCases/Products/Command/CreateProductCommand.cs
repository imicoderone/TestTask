using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TestTask.Application.Abstractions;
using TestTask.Domain.Entities;

namespace TestTask.Application.UseCases.Products.Command
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product()
                { Id = Guid.NewGuid(), Name = request.Name, Quantity = request.Quantity, Price = request.Price };
            
            await _context.Products.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
