using System;

namespace BusinessApp
{
    public class Customer : Entity
    {
        private string email;
        private string phone;

        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }

        public Customer(int id, string name, string email, string phone)
            : base(id, name)
        {
            this.email = email;
            this.phone = phone;
        }

        public override string ToString()
        {
            return base.ToString() + $", Email: {email}, Phone: {phone}";
        }
    }
}
