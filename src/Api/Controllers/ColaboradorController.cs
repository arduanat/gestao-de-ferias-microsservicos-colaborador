﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dominio.Context;
using Dominio.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Dominio.ValueObjects.Extensions;
using Api;
using Microsoft.Extensions.Logging;

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
            var nomes = new List<string>() { "Alexandre", "Eduardo", "Henrique", "Murilo", "Theo", "André", "Enrico", "Henry", "Nathan", "Thiago", "Antônio", "Enzo", "Ian", "Otávio", "Thomas", "Augusto", "Erick", "Isaac", "Pietro", "Vicente", "Breno", "Felipe", "João", "Rafael", "Vinícius", "Bruno", "Fernando", "Kaique", "Raul", "Vitor", "Caio", "Francisco", "Leonardo", "Rian", "Yago", "Cauã", "Frederico", "Luan", "Ricardo", "Ygor", "Daniel", "Guilherme", "Lucas", "Rodrigo", "Yuri", "Danilo", "Gustavo", "Mathias", "Samuel", "Agatha", "Camila", "Esther", "Isis", "Maitê", "Natália", "Alícia", "Carolina", "Fernanda", "Joana", "Malu", "Nicole", "Amanda", "Catarina", "Gabriela", "Laís", "Maria", "Olívia", "Ana", "Cecília", "Gabrielle", "Lara", "Mariah", "Pietra", "Antonela", "Clara", "Giovanna", "Larissa", "Mariana", "Rafaela", "Aurora", "Clarice", "Giulia", "Lavínia", "Marina", "Rebeca", "Bárbara", "Eduarda", "Heloísa", "Letícia", "Maya", "Sara", "Beatriz", "Elisa", "Isabel", "Liz", "Melissa", "Sophie", "Bianca", "Emanuelly", "Isabelly", "Lorena", "Milena", "Stella", "Bruna", "Emilly", "Isadora", "Luana", "Mirella", "Vitória", "Yasmin" };
            var sobreNomes = new List<string>() { "Abreu", "Adães", "Adorno", "Aguiar", "Albuquerque", "Alcântara", "Aleluia", "Alencar", "Almeida", "Altamirano", "Alvarenga", "Álvares", "Alves", "Alvim", "Amaral", "Amigo", "Amor", "Amorim", "Anchieta", "Andrada", "Andrade", "Anes", "Anjos", "Antunes", "Anunciação", "Aragão", "Araújo", "Arruda", "Ascensão", "Assis", "Azeredo", "Azevedo", "Bandeira", "Barbosa", "Barros", "Barroso", "Bastos", "Batista", "Bermudes", "Bernades", "Bernardes", "Bicalho", "Bispo", "Bocaiuva", "Bolsonaro", "Borba", "Borges", "Borsoi", "Botelho", "Braga", "Bragança", "Brandão", "Brasil", "Brasiliense", "Bueno", "Cabral", "Café", "Camacho", "Camargo", "Caminha", "Camões", "Cardoso", "Carmo", "Carnaval", "Carneiro", "Carvalhal", "Carvalho", "Carvalhosa", "Castilho", "Castro", "Cerejeira", "Chaves", "Coelho", "Coentrão", "Coimbra", "Constante", "Cordeiro", "Costa", "Cotrim", "Couto", "Coutinho", "Cruz", "Cunha", "Curado", "Dambros", "Dias", "Diegues", "Dorneles", "Duarte", "Eça", "Encarnação", "Esteves", "Evangelista", "Exaltação", "Fagundes", "Faleiros", "Falópio", "Falqueto", "Faria", "Farias", "Faro", "Ferrão", "Ferraz", "Ferreira", "Ferrolho", "Fernandes", "Figo", "Figueira", "Figueiredo", "Figueiroa", "Fioravante", "Fonseca", "Fontes", "Fortaleza", "França", "Freire", "Freitas", "Frota", "Furquim", "Furtado", "Galvão", "Gama", "Garrastazu", "Gato", "Gomes", "Gonçales", "Gonçalves", "Gonzaga", "Gouveia", "Guimarães", "Gusmão", "Henriques", "Hernandes", "Holanda", "Homem", "Hora", "Hungria", "Jardim", "Junqueira", "Lacerda", "Lange", "Leitão", "Leite", "Leme", "Lins", "Locatelli", "Lopes", "Luz", "Macedo", "Machado", "Madureira", "Maduro", "Magalhães", "Mairinque", "Malafaia", "Malta", "Mariz", "Marques", "Martins", "Massa", "Matos", "Médici", "Meireles", "Mello", "Melo", "Mendes", "Mendonça", "Menino", "Mesquita", "Miranda", "Moraes", "Morais", "Morato", "Moreira", "Moro", "Monteiro", "Muniz", "Namorado", "Nantes", "Nascimento", "Navarro", "Naves", "Negreiros", "Negrete", "Neves", "Nóbrega", "Nogueira", "Noronha", "Nunes", "Oliva", "Oliveira", "Outeiro", "Pacheco", "Padrão", "Paes", "Pais", "Paiva", "Paixão", "Papanicolau", "Parga", "Pascal", "Pascoal", "Pasquim", "Patriota", "Peçanha", "Pedrosa", "Pedroso", "Peixoto", "Pensamento", "Penteado", "Pereira", "Peres", "Pessoa", "Pestana", "Pimenta", "Pimentel", "Pinheiro", "Pinto", "Pires", "Poeta", "Policarpo", "Porto", "Portugal", "Prado", "Prudente", "Quaresma", "Queirós", "Queiroz", "Ramalhete", "Ramalho", "Ramires", "Ramos", "Rangel", "Reis", "Resende", "Ribeiro", "Rios", "Rodrigues", "Roma", "Romão", "Sá", "Sacramento", "Sampaio", "Sampaulo", "Sampedro", "Sanches", "Santacruz", "Santana", "Santander", "Santarrosa", "Santiago", "Santos", "Saragoça", "Saraiva", "Saramago", "Seixas", "Serra", "Serrano", "Silva", "Silveira", "Simões", "Siqueira", "Soares", "Soeiro", "Sousa", "Souza", "Tavares", "Teixeira", "Teles", "Torquato", "Trindade", "Uchoa", "Uribe", "Ustra", "Valadares", "Valença", "Valente", "Varela", "Vasconcelos", "Vasques", "Vaz", "Veiga", "Velasques", "Veloso", "Viana", "Vieira", "Vilela", "Vilhena" };
            List<Colaborador> novosColaboradores = new List<Colaborador>();

            for (int i = 0; i < quantidade; i++)
            {
                var nomeCompletoAleatorio = $"{nomes.PickRandom()} {sobreNomes.PickRandom()} {sobreNomes.PickRandom()}";
                var matriculaAleatoria = $"100{new Random().Next(100000, 999999)}";

                var colaboradorAleatorio = new Colaborador(nomeCompletoAleatorio, matriculaAleatoria);
                novosColaboradores.Add(colaboradorAleatorio);
            }

            await contexto.AddRangeAsync(novosColaboradores);
            await contexto.SaveChangesAsync();
        }
    }
}