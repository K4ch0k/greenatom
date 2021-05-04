using System;

namespace GreenatomAPI.Models
{
    public class CreateOrderModel
    {
        /// <summary>
        /// Пользователь, к которому прикреплен заказ
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Стоимость всего заказа
        /// </summary>
        public float Amount { get; set; }
    }
}