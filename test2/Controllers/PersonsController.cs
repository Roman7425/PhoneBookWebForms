using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using test2.Models;

namespace test2.Controllers
{
    public class PersonsController : ApiController
    {
        List<Person> persons = new List<Person>();

        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            using (var connection = new SqlConnection(Db.connectionString))
            {
                connection.Open();

                string allPersonsString = "SELECT * FROM PersonTest";
                using (var commandRead = new SqlCommand(allPersonsString, connection))
                {
                    var reader = commandRead.ExecuteReader();
                    while (reader.Read())
                    {
                        Person newPerson = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        persons.Add(newPerson);
                    }
                    reader.Close();
                }
            }
            return persons;
        }

        [HttpPost]
        public void PostNewPerson(Person person)
        {
            string addNewPerson = "INSERT INTO PersonTest (Name, Surname, Phone, Email) VALUES (@Name, @Surname, @Phone, @Email)";

            using(var connection = new SqlConnection(Db.connectionString))
            {
                connection.Open();

                using(var update = new SqlCommand(addNewPerson, connection))
                {
                    update.Parameters.Add("@Name", SqlDbType.NChar, 20).Value = person.Name;
                    update.Parameters.Add("@Surname", SqlDbType.NChar, 20).Value = person.Surname;
                    update.Parameters.Add("@Phone", SqlDbType.NChar, 20).Value = person.Phone;
                    update.Parameters.Add("@Email", SqlDbType.NChar, 20).Value = person.Email;
                    update.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete]

        public void DeletePerson(int id)
        {
            using (var connection = new SqlConnection(Db.connectionString))
            {
                connection.Open();
                string delete = $"DELETE FROM PersonTest WHERE Id = '{id}'";

                using (var deleteCommand = new SqlCommand(delete, connection))
                {
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
