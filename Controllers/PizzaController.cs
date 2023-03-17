using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase {
        [HttpGet]
        public IActionResult GetAll() {
            IActionResult   respuesta;
            List<Pizza>     entityList;

            entityList  = BD.GetAll();
            respuesta   = Ok(entityList);
            return respuesta;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            IActionResult   respuesta = null;
            Pizza           entity;

            entity = BD.GetById(id);
            if (entity == null) {
                respuesta = NotFound();
            } else {
                respuesta = Ok(entity);
            }

            return respuesta;
        }


        [HttpPost]
        public IActionResult Create(Pizza pizza) {

            int             intRowsAffected;
            intRowsAffected = BD.Insert(pizza);
            //intRowsAffected = BD.Insert_ReturnNewId(pizza);

            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }


    }

    
}