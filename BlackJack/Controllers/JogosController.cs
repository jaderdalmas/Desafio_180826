using System.Collections.Generic;
using System.Linq;
using BlackJack.Models;
using BlackJack.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Controllers
{
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly BlackJackContext _context;
        private readonly JogosServices _service;

        public JogosController(BlackJackContext context)
        {
            _context = context;
            _service = new JogosServices();
        }  

        [HttpGet]
        public List<Jogo> GetAll()
        {
            return _context.Jogos.ToList();
        }

        [HttpGet("{id}", Name = "GetJogo")]
        public IActionResult GetById(int id)
        {
            var item = _context.Jogos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var jogo = _service.NovoJogo();
            _context.Jogos.Add(jogo);
            _context.SaveChanges();

            return CreatedAtRoute("GetJogo", new { id = jogo.Id }, jogo);
        }

        [HttpPut("{id}&{encerrar}")]
        public IActionResult Update(int id, bool encerrar)
        {
            var jogo = _context.Jogos.Find(id);
            if (jogo == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(jogo.Status))
            {
                return NoContent();
            }

            if(encerrar)
            {
                jogo = _service.EncerrarJogo(jogo);
            }
            else
            {
                jogo = _service.NovaCarta(jogo);
            }

            _context.Jogos.Update(jogo);
            _context.SaveChanges();
            return Ok(jogo);
        }
    }
}