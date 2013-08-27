namespace Core.Services
{
    using System;
    using Core.Models;
    using Repositories;

    public class AbreMovimentoService
    {
        private readonly IMovimentoRepository movimentoRepository;

        public AbreMovimentoService(IMovimentoRepository movimentoRepository)
        {
            this.movimentoRepository = movimentoRepository;
        }

        public Movimento Abrir(DateTime data)
        {
            var movimentoAnterior = this.movimentoRepository.GetAnterior();
            var movimento = new Movimento(data, movimentoAnterior);

            this.movimentoRepository.Save(movimento);

            return movimento;
        }
    }
}