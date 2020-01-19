using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using persistedXml.Models;

namespace persistedXml.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase xmlFile)
        {
            if (xmlFile.ContentType.Equals("application/xml") || xmlFile.ContentType.Equals("text/xml")) {

                try
                {
                    var xmlPath = Server.MapPath("~/Content/" + xmlFile.FileName);
                    xmlFile.SaveAs(xmlPath);
                    XDocument xDoc = XDocument.Load(xmlPath);
                    List<Product> productList = xDoc.Descendants("product").Select
                        (product =>
                        new Product {
                            id = Convert.ToInt32(product.Element("id").Value),
                            Name = product.Element("name").Value,
                            Price = Convert.ToDecimal(product.Element("price").Value),
                            Quantity = Convert.ToInt32(product.Element("quantity").Value),

                }).ToList();
                    using (PersistenceEgEntities2 px = new PersistenceEgEntities2()) {

                        foreach (var i in productList) {

                            var v = px.Products.Where(a => a.id.Equals(i.id)).FirstOrDefault();
                            if (v != null)
                            {

                                v.id = i.id;
                                v.Name = i.Name;
                                v.Price = i.Price;
                                v.Quantity = i.Quantity;
                            }
                            else {
                                px.Products.Add(i);
                            }
                        }
                        px.SaveChanges();
                        ViewBag.Result = px.Products.ToList();

                    }

                        return View("Sucess");
            
                }
                catch {
                    ViewBag.Error = "Something Wrong";
                    return View("Index");

                }
            }
            else {

                ViewBag.Error = "Can't Import this xml File";
                return View("Index");
            }
        }
    }
}