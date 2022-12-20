using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterCompra
{
    public class ObterSolicitacaoCompraPorIdQuery : IRequest<ObterSolicitacaoCompraPorIdResponse>
    {
        public Guid Id { get; set; }
    }
}
