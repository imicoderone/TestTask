using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Abstractions;
using TestTask.Domain.Entities;

namespace TestTask.Application.UseCases.Products.Query
{
    public class GetProductQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IApplicationDbContext _context;

        public GetProductQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }

}
