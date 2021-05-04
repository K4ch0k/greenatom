using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenatomAPI.Models
{
    public class CreateProductInOrderModel
    {
        /// <summary>
        /// ID заказа, в который необходимо добавить продукт
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// ID продукта
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Статус оформления продукта в заказе
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// Количество продукта с заданным ID в заказе
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Скидка на продукт
        /// </summary>
        public float Discount { get; set; }
    }
}