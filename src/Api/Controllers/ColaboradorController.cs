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
        public async Task<List<Colaborador>> Get()
        {
            var colaboradores = await contexto.Colaborador.AsNoTracking().ToListAsync();
            return colaboradores;
        }

        [HttpPost]
        public async Task<Response> Post([FromBody] int quantidade)
        {
            try
            {
                await CriarMultiplosAleatoriamente(quantidade);
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }

        [HttpDelete]
        public async Task<Response> Delete([FromBody] int id)
        {
            try
            {
                if(id == 0)
                {
                    var colaboradores = await contexto.Colaborador.ToListAsync();
                    contexto.RemoveRange(colaboradores);
                    await contexto.SaveChangesAsync();
                }

                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }

        private async Task CriarMultiplosAleatoriamente(int quantidade)
        {
            var novosColaboradores = new List<Colaborador>();

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