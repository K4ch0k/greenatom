using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenatomAPI.Models
{
    public class EditProductInOrderModel
    {
        /// <summary>
        /// ID продукта в заказе, который необходимо изменить
        /// </summary>
        public int EditID { get; set; }

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