using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using WebAPIProjectNet22.Models;

namespace WebAPIProjectNet22.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private IPropertyContext _propertyContext;

        public PropertyRepository(IPropertyContext propertyContext)
        {
            _propertyContext = propertyContext;
        }

        public int DeleteProperty(string id)
        {
            using (OracleConnection con = _propertyContext.GetConnection())
            {
                using (OracleCommand cmd = _propertyContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "DELETE FROM RICACORP.PROPERTY_SAMPLE p WHERE p.id = :id";

                        OracleParameter param_id = new OracleParameter("id", id);
                        param_id.OracleDbType = OracleDbType.Varchar2;
                        cmd.Parameters.Add(param_id);

                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public Property GetProperty(string id)
        {
            using (OracleConnection con = _propertyContext.GetConnection())
                {
                    using (OracleCommand cmd = _propertyContext.GetCommand())
                    {
                        try
                        {
                            con.Open();
                            cmd.BindByName = true;

                            cmd.CommandText = "SELECT p.* FROM RICACORP.PROPERTY_SAMPLE p WHERE p.id = :id";

                            OracleParameter param_id = new OracleParameter("id", id);
                            param_id.OracleDbType = OracleDbType.Varchar2;
                            cmd.Parameters.Add(param_id);

                            //Execute the command and use DataReader to display the data
                            OracleDataReader reader = cmd.ExecuteReader();

                            Property prop = new Property();
                            while (reader.Read())
                            {
                                prop.id = reader["ID"].ToString();
                                prop.po_document = reader["PO_DOCUMENT"].ToString();
                            }
                            reader.Dispose();
                            return prop;
                        }
                        catch (Exception ex)
                        {
                            throw (ex);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            
        }

        public Property InsertProperty(Property property)
        {
            using (OracleConnection con = _propertyContext.GetConnection())
            {
                using (OracleCommand cmd = _propertyContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        // Serialize the object back to a JSON-formatted string
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
                        };
                        string jsonOutput = JsonSerializer.Serialize(property, options);
                        //Console.WriteLine(jsonOutput);
                        cmd.CommandText = @"INSERT INTO RICACORP.PROPERTY_SAMPLE(ID, PO_DOCUMENT) VALUES (:id, :po_document)";

                        OracleParameter param_id = new OracleParameter("id", property.id);
                        param_id.OracleDbType = OracleDbType.Varchar2;
                        cmd.Parameters.Add(param_id);

                        OracleParameter param_doc = new OracleParameter("po_document", property.po_document);
                        param_doc.OracleDbType = OracleDbType.Json;
                        cmd.Parameters.Add(param_doc);

                        cmd.ExecuteNonQuery();

                        return property;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public Property UpdateProperty(string id, Property property)
        {
            using (OracleConnection con = _propertyContext.GetConnection())
            {
                using (OracleCommand cmd = _propertyContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        // Serialize the object back to a JSON-formatted string
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
                        };
                        string jsonOutput = JsonSerializer.Serialize(property, options);
                        //Console.WriteLine(jsonOutput);
                        cmd.CommandText = @"UPDATE RICACORP.PROPERTY_SAMPLE p SET p.po_document = :po_document WHERE p.id = :id";

                        OracleParameter param_id = new OracleParameter("id", property.id);
                        param_id.OracleDbType = OracleDbType.Varchar2;
                        cmd.Parameters.Add(param_id);

                        OracleParameter param_doc = new OracleParameter("po_document", property.po_document);
                        param_doc.OracleDbType = OracleDbType.Json;
                        cmd.Parameters.Add(param_doc);

                        cmd.ExecuteNonQuery();

                        return property;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        List<Property> IPropertyRepository.GetAllProperties()
        {
            List<Property> properties = new List<Property>();

            using (OracleConnection con = _propertyContext.GetConnection())
            {
                using (OracleCommand cmd = _propertyContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT p.* FROM RICACORP.PROPERTY_SAMPLE p";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Property prop = new Property
                            {
                                id = reader["ID"].ToString(),
                                po_document = reader["PO_DOCUMENT"].ToString()
                            };
                            properties.Add(prop);
                        }
                        reader.Dispose();
                        return properties;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
