﻿namespace OrdersApp.Exceptions
{
    class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message) : base(message) { }
    }
}
