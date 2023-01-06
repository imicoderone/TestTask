using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Abstractions;

namespace TestTask.Application.UseCases.Products.Command
{
    public class RemoveProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new Exception("Product not found");
            }

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
