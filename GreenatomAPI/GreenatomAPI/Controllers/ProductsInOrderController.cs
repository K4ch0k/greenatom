using GreenatomAPI.Entities;
using GreenatomAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GreenatomAPI.Controllers
{
    /// <summary>
    /// (Работа с продуктами в заказе)
    /// </summary>
    public class ProductsInOrderController : ApiController
    {
        /// <summary>
        /// Получение списка продуктов из заказов
        /// </summary>
        /// <returns>Строка Json, содержащая информацию о всех предметах в заказах из БД</returns>
        [HttpGet, Route("AllProductsInOrders")]
        public IHttpActionResult GetProductsInOrders()
        {
            try
            {
                return Ok(Core.db.ProductsInOrder.ToList().ConvertAll(item => new ProductsInOrderModel(item)));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Получение информации о продукте из заказа
        /// </summary>
        /// <param name="ID">ID записи продукта из заказа</param>
        /// <returns>Строка Json, содержащая подробную информацию о предмете из заказа</returns>
        [HttpGet, Route("DetailsProductsInOrders")]
        public IHttpActionResult DetailsProductsInOrders(int ID)
        {
            try
            {
                ProductsInOrder Search = Core.db.ProductsInOrder.Find(ID);
                if (Search == null)
                {
                    ModelState.AddModelError("Error", "Такой записи не существует");
                    return BadRequest(ModelState);
                }
                return Ok(new ProductsInOrderModel(Search));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Создает заказ и добавляет в заказ продукт
        /// </summary>
        /// <param name="NewOrder">Объект, содержазий все параметры для создания заказа и продукта в заказе</param>
        /// <returns>Созданная запись о продукте в заказе</returns>
        [HttpPost, Route("CreateProductInNewOrder")]
        public IHttpActionResult CreateProductInNewOrder(CreateProductInNewOrderModel NewOrder)
        {
            try
            {
                if (Core.db.Users.Find(NewOrder.UserID) == null)
                    ModelState.AddModelError("Errors", "Пользователя с таким ID не существует");
                if (Core.db.Status.Find(NewOrder.StatusID) == null)
                    ModelState.AddModelError("Errors", "Такого статуса не существует ");
                if (NewOrder.Quantity <= 0)
                    ModelState.AddModelError("Errors", "Количество продукта в заказе должно быть больше нуля");

                Products SelectProduct = Core.db.Products.Find(NewOrder.ProductID);
                if (SelectProduct == null)
                    ModelState.AddModelError("Errors", "Такого продукта не существует");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Orders CreateOrder = new Orders();
                CreateOrder.UserID = NewOrder.UserID;
                CreateOrder.Amount = SelectProduct.Price * (NewOrder.Discount / 100);
                CreateOrder.Datetime = DateTime.Now;
                Core.db.Orders.Add(CreateOrder);
                Core.db.SaveChanges();

                ProductsInOrder CreateProdInOrder = new ProductsInOrder();
                CreateProdInOrder.OrderID = CreateOrder.ID;
                CreateProdInOrder.ProductID = NewOrder.ProductID;
                CreateProdInOrder.Quantity = NewOrder.Quantity;
                CreateProdInOrder.StatusID = NewOrder.StatusID;
                CreateProdInOrder.Amoount = (SelectProduct.Price - SelectProduct.Price * (NewOrder.Discount / 100)) * NewOrder.Quantity;
                CreateProdInOrder.Discount = NewOrder.Discount;

                Core.db.ProductsInOrder.Add(CreateProdInOrder);

                Core.db.SaveChanges();
                return Ok(new ProductsInOrderModel(CreateProdInOrder));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Добавление продукта в уже созданный заказ
        /// </summary>
        /// <param name="NewProduct">Объект, содержазий все параметры для создания продукта в заказе</param>
        /// <returns>Созданная запись о продукте в заказе</returns>
        [HttpPost, Route("CreateProductInOrder")]
        public IHttpActionResult CreateProductInOrder(CreateProductInOrderModel NewProduct)
        {
            try
            {
                if (Core.db.Orders.Find(NewProduct.OrderID) == null)
                    ModelState.AddModelError("Errors", "Заказ с выбранным ID не найден");
                if (Core.db.Status.Find(NewProduct.StatusID) == null)
                    ModelState.AddModelError("Errors", "Такого статуса не существует");
                if (NewProduct.Quantity <= 0)
                    ModelState.AddModelError("Errors", "Количество продукта в заказе должно быть больше нуля");

                Products SelectProduct = Core.db.Products.Find(NewProduct.ProductID);
                if (SelectProduct == null)
                    ModelState.AddModelError("Errors", "Такого продукта не существует");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Orders SelectOrder = Core.db.Orders.Find(NewProduct.OrderID);
                SelectOrder.Amount += (SelectProduct.Price - SelectProduct.Price * (NewProduct.Discount / 100))* NewProduct.Quantity;
                SelectOrder.Datetime = DateTime.Now;

                ProductsInOrder CreateProdInOrder = new ProductsInOrder();
                CreateProdInOrder.OrderID = NewProduct.OrderID;
                CreateProdInOrder.ProductID = NewProduct.ProductID;
                CreateProdInOrder.Quantity = NewProduct.Quantity;
                CreateProdInOrder.Discount = NewProduct.Discount;
                CreateProdInOrder.StatusID = NewProduct.StatusID;
                CreateProdInOrder.Amoount = (SelectProduct.Price - SelectProduct.Price * (NewProduct.Discount / 100)) * NewProduct.Quantity;

                Core.db.ProductsInOrder.Add(CreateProdInOrder);

                Core.db.SaveChanges();
                return Ok(new ProductsInOrderModel(CreateProdInOrder));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Удаляет запись о продукте из заказа
        /// </summary>
        /// <param name="ID">ID записи о продукте из заказа</param>
        /// <returns>Удаленная запись о продукте в заказе</returns>
        [HttpDelete, Route("DeleteProductInOrder")]
        public IHttpActionResult DeleteProductInOrder(int ID)
        {
            try
            {
                ProductsInOrder DeleteProduct = Core.db.ProductsInOrder.Find(ID);
                if (DeleteProduct == null)
                    ModelState.AddModelError("Errors", "Продукта в заказе с таким ID не существует");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Orders EditOrder = Core.db.Orders.Find(DeleteProduct.OrderID);
                EditOrder.Amount -= DeleteProduct.Amoount;
                EditOrder.Datetime = DateTime.Now;

                Core.db.ProductsInOrder.Remove(DeleteProduct);
                Core.db.SaveChanges();
                return Ok(DeleteProduct);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Изменяет продукт в заказе
        /// </summary>
        /// <param name="EditProduct">Измененнный объект</param>
        /// <returns>Измененнный объект</returns>
        [HttpPut, Route("EditProductInOrder")]
        public IHttpActionResult EditProductInOrder(EditProductInOrderModel EditProduct)
        {
            try
            {
                ProductsInOrder SearchProdInOrd = Core.db.ProductsInOrder.Find(EditProduct.EditID);
                Products SearchProd = Core.db.Products.Find(EditProduct.ProductID);
                Orders SearchOrd = Core.db.Orders.Find(Core.db.ProductsInOrder.Find(EditProduct.EditID).OrderID);

                if (SearchProdInOrd == null)
                    ModelState.AddModelError("Errors", "Продукт в заказе не найден");
                if (SearchProd == null)
                    ModelState.AddModelError("Errors", "Продукт не найден");
                if (SearchOrd == null)
                    ModelState.AddModelError("Errors", "Заказ не найден");
                if (Core.db.Status.Find(EditProduct.StatusID) == null)
                    ModelState.AddModelError("Errors", "Такого статуса не существует");
                if (EditProduct.Quantity <= 0)
                    ModelState.AddModelError("Errors", "Количество продукта в заказе должно быть больше нуля");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                SearchProdInOrd.ProductID = EditProduct.ProductID;
                SearchProdInOrd.Quantity = EditProduct.Quantity;
                SearchProdInOrd.StatusID = EditProduct.StatusID;
                SearchProdInOrd.Discount = EditProduct.Discount;
                SearchProdInOrd.Amoount = (SearchProd.Price - SearchProd.Price * (EditProduct.Discount / 100)) * EditProduct.Quantity;

                SearchOrd.Amount = 0;
                foreach (var item in SearchOrd.ProductsInOrder)
                {
                    SearchOrd.Amount += item.Amoount;
                }

                Core.db.SaveChanges();
                return Ok(new ProductsInOrderModel(SearchProdInOrd));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Errors", ex.InnerException);
                ModelState.AddModelError("Errors", ex.InnerException.InnerException);
                return BadRequest(ModelState);
            }
        }

    }
}