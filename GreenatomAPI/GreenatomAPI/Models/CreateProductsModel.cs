using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenatomAPI.Models
{
    public class CreateProductsModel
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Описание товара
        /// </summary>
        public string Description { get; set; }
    }
}