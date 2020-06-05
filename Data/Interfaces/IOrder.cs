using Data.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IOrder : IRepository<Orders>
    {
        void CreateOrder(Orders orders);
        void Paid(Orders orders);
        void UpdateBakeryQuantity(Orders orders);
    }
}
