using GreenatomAPI.Entities;
using GreenatomAPI.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace GreenatomAPI.Controllers
{
    /// <summary>
    /// (Работа с продуктами)
    /// </summary>
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <returns>Строка Json, содержащая информацию о всех продуктах из БД</returns>
        [HttpGet, Route("AllProducts")]
        public IHttpActionResult GetProducts()
        {
            try
            {
                return Ok(Core.db.Products.ToList().ConvertAll(item => new ProductsModel(item)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Получение информации о продукте
        /// </summary>
        /// <param name="ID">ID продукта</param>
        /// <returns>Строка Json, содержащая информацию о продукте из БД</returns>
        [HttpGet, Route("DetailsProduct")]
        public IHttpActionResult DetailsProduct(int ID)
        {
            try
            {
                Products Search = Core.db.Products.Find(ID);
                if (Search == null)
                {
                    ModelState.AddModelError("Error", "Такого товара не существует");
                    return BadRequest(ModelState);
                }
                return Ok(new ProductsModel(Search));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        /// <param name="NewProduct">Добавляемый продукт</param>
        /// <returns>Добавленный продукт</returns>
        [HttpPost, Route("CreateProduct")]
        public IHttpActionResult CreateProduct(CreateProductsModel NewProduct)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(NewProduct.Name))
                    ModelState.AddModelError("Errors", "Поле \"Name\" Обязательно к заполнению");
                if (NewProduct.Price <= 0)
                    ModelState.AddModelError("Errors", "Поле \"Price\" Должно быть больше нуля");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Products Prod = new Products();
                Prod.Name = NewProduct.Name;
                Prod.Price = NewProduct.Price;
                if (NewProduct.Description.ToLower() == "null" ||
                    String.IsNullOrWhiteSpace(NewProduct.Description) ||
                    String.IsNullOrEmpty(NewProduct.Description))
                    NewProduct.Description = null;
                Prod.Description = NewProduct.Description;
                Core.db.Products.Add(Prod);
                Core.db.SaveChanges();
                return CreatedAtRoute("Default", Prod.ID, new ProductsModel(Prod));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="ID">ID продукта</param>
        /// <returns>Удаленный продукт</returns>
        [HttpDelete, Route("DeleteProduct")]
        public IHttpActionResult DeleteProduct(int ID)
        {
            try
            {
                Products DeleteProduct = Core.db.Products.Find(ID);
                if (DeleteProduct == null)
                    ModelState.AddModelError("Errors", "Продукта с таким ID не существует");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Core.db.Products.Remove(DeleteProduct);
                Core.db.SaveChanges();
                return Ok(new ProductsModel(DeleteProduct));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Изменение продукта
        /// </summary>
        /// <param name="ID">ID изменяемого продукта</param>
        /// <param name="Product">Новые параметры продукта</param>
        /// <returns>Измененная запись о продукте</returns>
        [HttpPut, Route("EditProduct")]
        public IHttpActionResult EditProduct(int ID, CreateProductsModel Product)
        {
            try
            {
                Products EditProduct = Core.db.Products.Find(ID);
                if (EditProduct == null)
                    ModelState.AddModelError("Errors", "Продукта с таким ID не существует");
                if (String.IsNullOrWhiteSpace(Product.Name))
                    ModelState.AddModelError("Errors", "Наименование продукта не заполнено");
                if (Product.Price <= 0)
                    ModelState.AddModelError("Errors", "Цена продукта должна быть больше нуля");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                EditProduct.Name = Product.Name;
                EditProduct.Price = Product.Price;
                EditProduct.Description = Product.Description;

                Core.db.SaveChanges();
                return Ok(new ProductsModel(EditProduct));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }
    }
}
