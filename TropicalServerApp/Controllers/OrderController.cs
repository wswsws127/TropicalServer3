using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TropicalServerApp.Models;
using TropicalServerApp.Models.OrderModels;
using System.Net;
using System.Data.Entity;

namespace TropicalServerApp.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        TropicalServer_DB_Access_Layer.Db1 dblayer = new TropicalServer_DB_Access_Layer.Db1();
       //create database context:
        public MyOrderModel momContext = new MyOrderModel();

        //bind two tables, get the wanted columns 
        //[Authorize]


        public ActionResult Index(string orderDate, string custID, string custName, string salesMan)
        {
            //MyOrderModel sd = new MyOrderModel(); //
            List<tblOrder> allTblOrder = momContext.tblOrder.ToList();
            List<tblOrderHistory> allTblHistory = momContext.tblOrderHistory.ToList();
            List<tblCustomer> allTblCustomer = momContext.tblCustomer.ToList();
            //join tables 
            var multipletable = from c in allTblOrder
                                join st in allTblCustomer
                                on c.OrderCustomerNumber equals st.CustNumber
                                where c.OrderTrackingNumber != null
                                select new OrderDetail { orderDetails = c, customerDetails = st };
            if (!String.IsNullOrEmpty(orderDate))
            {
                var today = new DateTime(2012, 2, 28, 0, 0, 0);
                //string dateformat = today.ToString("yyyy/M/dd"); //"2012/2/16"
                switch (orderDate)
                {    
                    case "Today":
                        multipletable = multipletable.Where(s => (today.Date - s.orderDetails.OrderDate.Date).TotalDays == 0);
                        break;
                    case "Last 7 Days":
                       // dateformat = date1.AddDays(7).ToString("yyyy/M/dd");
                        multipletable = multipletable.Where(s => (today.Date - s.orderDetails.OrderDate.Date).TotalDays< 7);
                        break;
                    case "Last 1 Month":

                        multipletable = multipletable.Where(s => (today.Month - s.orderDetails.OrderDate.Month) ==1);
                        break;
                    case "Last 6 Month":

                        multipletable = multipletable.Where(s => (today.Month - s.orderDetails.OrderDate.Month) == 6);
                        break;

                    default:
                        multipletable = multipletable.Where(s => s.orderDetails.OrderDate.ToString().Contains(orderDate.ToString()));
                        break;
                }
                //if (orderDate=="") { }
                //else {
                //    multipletable = multipletable.Where(s => s.orderDetails.OrderDate.ToString().Contains(orderDate.ToString())); }

            }
            if (custID!=null)
            {
                multipletable = multipletable.Where(s => s.customerDetails.CustID.ToString().Contains(custID.ToString()));

            }
            if (!String.IsNullOrEmpty(custName))
            {
                multipletable = multipletable.Where(s => s.customerDetails.CustName.ToUpper().Contains(custName.ToUpper()));

            }
            if (!String.IsNullOrEmpty(salesMan))
            {
                multipletable = multipletable.Where(s => s.customerDetails.CustName.ToUpper().Contains(custName.ToUpper()));
                switch (salesMan)
                {
                    case "Joe Vicini":
                        multipletable = multipletable.Where(s => s.orderDetails.OrderRouteNumber== 501);
                        break;
                    case "Sonal Patel":
                        // 506
                        multipletable = multipletable.Where(s => s.orderDetails.OrderRouteNumber == 506);
                        break;
                   

                    default:
                        
                        break;
                }
            }
            return View(multipletable);
        }
        //filtered:

        public ViewResult Search(string searchString)
        {
            //MyOrderModel sd = new MyOrderModel(); //
            List<tblOrder> allTblOrder = momContext.tblOrder.ToList();
            List<tblOrderHistory> allTblHistory = momContext.tblOrderHistory.ToList();
            List<tblCustomer> allTblCustomer = momContext.tblCustomer.ToList();
            //join tables 
            var multipletable = from c in allTblOrder
                                join st in allTblCustomer
                                on c.OrderCustomerNumber equals st.CustNumber
                                where c.OrderTrackingNumber != null 
                                select new OrderDetail { orderDetails = c, customerDetails = st };
            if (!String.IsNullOrEmpty(searchString))
            {
                multipletable = multipletable.Where(s => s.customerDetails.CustName.ToUpper().Contains(searchString.ToUpper()));
                                   
            }
            return View(multipletable);
        }

        // GET: Order/Details/版本2
        public ActionResult DashBoard()
        {
            return View();
        }

        // GET: View Order/Details/5 查看订单详情
        public ActionResult ViewDetails(int orderId)
        {
            MyOrderModel sd = new MyOrderModel(); 
            List<tblOrder> allTblOrder = sd.tblOrder.ToList();
            //List<tblOrderHistory> allTblHistory = sd.tblOrderHistory.ToList();
            //List<tblCustomer> allTblCustomer = sd.tblCustomer.ToList();
            //join tables 
            var multipletable = from c in allTblOrder
                              
                                where c.OrderID == orderId
                                select new OrderDetail { orderDetails = c };

            return View(multipletable);
        }

        //get ALL order data 
        public JsonResult GetTblOrder() {


            List<tblOrder> allOrder = new List<tblOrder>();
            //Here MyOrderModel is DB context, which is created at the time of model creation
            using (Models.OrderModels.MyOrderModel db = new Models.OrderModels.MyOrderModel()) {
                //select data from MyOrderModel
                allOrder = db.tblOrder.ToList();
            }
            return new JsonResult { Data=allOrder,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
        }

  

        //get filter data from jquery
        public JsonResult GetTblOrderWithParameter(string prefix)
        {
            List<tblOrder> allOrder = new List<tblOrder>();
            //Here MyOrderModel is DB context, which is created at the time of model creation
            using (Models.OrderModels.MyOrderModel db = new Models.OrderModels.MyOrderModel())
            {
                allOrder = db.tblOrder.Where(a=>a.OrderTrackingNumber.Contains(prefix)).ToList();
            }
            return new JsonResult { Data = allOrder, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public JsonResult get_data() { 
        //  DataSet ds=dblayer.Show_data();
        //    List<OrderDetail> listOD = new List<OrderDetail>();
        //    foreach (DataRow dr in ds.Tables[0].Rows) {
        //        listOD.Add(new OrderDetail {
        //            OrderTrackingNumber = dr["OrderTrackingNumber"].ToString(),
        //            OrderDate = dr["OrderDate"].ToString(),
        //            CustID = Convert.ToInt32(dr["CustID"]),
        //            CustName = dr["CustName"].ToString(),
        //            CustOfficeAddress1 = dr["CustOfficeAddress1"].ToString(),
        //            CustRouteNumber = Convert.ToInt32(dr["CustRouteNumber"])
        //        });
        //    }
        //    return Json(listOD,JsonRequestBehavior.AllowGet);

        //}

        // GET: Order/Create
        public ActionResult Create()
        {
            //Creation is disabled
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int orderId)
        {
            //var emp = momContext.tblOrder.Find(orderId);
            //MyOrderModel sd = new MyOrderModel();
            List<tblOrder> allTblOrder = momContext.tblOrder.ToList();//return null??
            //List<tblOrderHistory> allTblHistory = sd.tblOrderHistory.ToList();
            //List<tblCustomer> allTblCustomer = sd.tblCustomer.ToList();
            //join tables 
            var multipletable = from c in allTblOrder
                                where c.OrderID == orderId
                                select new OrderDetail { orderDetails = c };

            return View(multipletable);

        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult SaveEdit(TropicalServerApp.Models.OrderDetail model)
        {
            var oldRow = momContext.tblOrder.Find(model.orderDetails.OrderID);

            if(oldRow != null)
            {
                oldRow.OrderRouteNumber = model.orderDetails.OrderRouteNumber;
                oldRow.OrderCustomerNumber = model.orderDetails.OrderCustomerNumber;
                oldRow.OrderCaseNumbers = model.orderDetails.OrderCaseNumbers;
                oldRow.OrderItemNumber = model.orderDetails.OrderItemNumber;
                oldRow.OrderItemQty = model.orderDetails.OrderItemQty;
                momContext.Entry(oldRow).CurrentValues.SetValues(model.orderDetails);
                momContext.SaveChanges();
                return RedirectToAction("Index", "Order");
            }
          
            else { 

            return RedirectToAction("DashBoard","Order");
            }

        }

        // GET: Order/Delete/5
        public ActionResult Delete(int orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var targetRow = momContext.tblOrder.Find(orderId);
            //targetRow is a tblOrder object.
            if (targetRow == null)
            {
                return HttpNotFound();
            }
            //send the targetRow (tblOrder type) to Delete View.
            return View(targetRow);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed()
        {
            var orderId = Convert.ToInt32(Request["OrderID"].ToString());
            var targetRow = momContext.tblOrder.Find(orderId);
            if (targetRow == null)
            {
                return HttpNotFound();
            }
            else
            {
                momContext.tblOrder.Remove(targetRow);
                momContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                momContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
