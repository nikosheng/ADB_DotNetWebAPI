using System.Collections.Generic;
using WebAPIProjectNet22.Models;

namespace WebAPIProjectNet22.Repository
{
    public interface IPropertyRepository
    {
        Property GetProperty(string id);

        List<Property> GetAllProperties();

        Property InsertProperty(Property property);

        Property UpdateProperty(string id, Property property);

        int DeleteProperty(string id);

    }
}
