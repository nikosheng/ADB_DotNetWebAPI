using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIProjectNet22.Models;
using WebAPIProjectNet22.Repository;

namespace WebAPIProjectNet22.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }


        //Get: api/Property/<id>
        [HttpGet("{id}")]
        public IActionResult GetProperty(string id)
        {
            var property = _propertyRepository.GetProperty(id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property); ;
        }

        //Get: api/Property
        [HttpGet]
        public IActionResult GetAllProperties()
        {
            List<Property> properties = _propertyRepository.GetAllProperties();

            if (properties.Count == 0)
            {
                return NotFound();
            }

            return Ok(properties);
        }

        [HttpPut]
        public IActionResult PutProperty(string id, string po_document)
        {
            Property property = new Property()
            {
                id = id,
                po_document = po_document
            };

            _propertyRepository.InsertProperty(property);

            return Ok(property);
        }

        [HttpPost]
        public IActionResult UpdateProperty(string id, string po_document)
        {
            Property property = new Property()
            {
                id = id,
                po_document = po_document
            };

            _propertyRepository.UpdateProperty(id, property);

            return Ok(property);
        }


        [HttpDelete]
        public IActionResult DeleteProperty(string id)
        {
            _propertyRepository.DeleteProperty(id);

            return Ok();
        }
    }
}

