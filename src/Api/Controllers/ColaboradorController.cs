using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio.Context;
using Dominio.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Api;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly Contexto contexto;
        public ColaboradorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<List<Colaborador>> Get(int id)
        {
            List<Colaborador> colaboradores = new List<Colaborador>();

            if (id > 0)
            {
                var colaborador = await contexto.Colaborador.AsNoTracking().FirstAsync(x => x.Id == id);
                colaboradores.Add(colaborador);
            }
            else
            {
                colaboradores = await contexto.Colaborador.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
            }

            return colaboradores;
        }

        [HttpPost]
        public async Task<Response> Post(Colaborador colaborador, int quantidade)
        {
            try
            {
                if (quantidade > 1)
                {
                    await CriarMultiplosAleatoriamente(quantidade);
                }
                else
                {
                    contexto.Add(colaborador);
                    await contexto.SaveChangesAsync();
                }
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);

            }
        }

        [HttpPut]
        public async Task<Response> Put(Colaborador colaborador)
        {
            try
            {
                contexto.Update(colaborador);
                await contexto.SaveChangesAsync();
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }

        private async Task CriarMultiplosAleatoriamente(int quantidade)
        {
            List<Colaborador> novosColaboradores = new List<Colaborador>();

            for (int i = 0; i < quantidade; i++)
            {
                var matriculaAleatoria = $"1{new Random().Next(10000000, 99999999)}";

                var colaboradorAleatorio = new Colaborador(Guid.NewGuid().ToString(), matriculaAleatoria);
                novosColaboradores.Add(colaboradorAleatorio);
            }

            await contexto.AddRangeAsync(novosColaboradores);
            await contexto.SaveChangesAsync();
        }
    }
}