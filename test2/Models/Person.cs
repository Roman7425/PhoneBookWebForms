using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person() { }
        public Person(int id, string name, string surname, string phone, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Phone = phone;
            this.Email = email;
        }
    }
}