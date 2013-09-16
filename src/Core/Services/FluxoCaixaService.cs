namespace Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Models;
    using Repositories;

    public class FluxoCaixaService
    {
        private readonly ITransacaoRepository transacaoRepository;

        public FluxoCaixaService(ITransacaoRepository transacaoRepository)
        {
            this.transacaoRepository = transacaoRepository;
        }

        public FluxoCaixa[] Obter(DateTime dataInicial, DateTime dataFinal)
        {
            var dataAnteriorQueInicial = dataInicial.AddDays(-1);
            var transacoes = this.transacaoRepository.AllByDate(dataAnteriorQueInicial, dataFinal);
            var grouped = transacoes.Select(x => new { x.Data, x.Valor }).GroupBy(x => x.Data);
            var fluxos = new List<FluxoCaixa>();
            var saldoAnterior = 0m;

            foreach (var group in grouped)
            {
                var fluxo = new FluxoCaixa { SaldoAnterior = saldoAnterior, Data = group.Key };

                foreach (var item in group)
                {
                    if (item.Valor > 0)
                    {
                        fluxo.Entradas += item.Valor;
                    }
                    else
                    {
                        fluxo.Saidas += item.Valor;
                    }
                }

                saldoAnterior = fluxo.Saldo;

                if (fluxo.Data != dataAnteriorQueInicial)
                {
                    fluxos.Add(fluxo);
                }
            }

            return fluxos.OrderByDescending(x => x.Data).ToArray();
        }
    }
}