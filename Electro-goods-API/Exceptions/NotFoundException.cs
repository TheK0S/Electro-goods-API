﻿namespace Electro_goods_API.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
    }
}
