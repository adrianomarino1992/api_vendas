using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarSolicitacaoCompraResponse 
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }       
        public Guid SolicitacaoId { get; set; }
        
        public bool Sucesso{ get; set; }
        
    }
}

