using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TropicalServerApp.Models;

namespace TropicalServerApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies["tblTropicalUser"];
            if (cookie != null)
            {
                var userID = cookie["LoginID"].ToString();
                ViewBag.LoginID = userID;
            }
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Authorize(TropicalServerApp.Models.tblTropicalUser userModel)
        {
            HttpCookie cookie = new HttpCookie("tblTropicalUser");
          

            using (Models.LoginDataModel db =new Models.LoginDataModel()) {
                var userDetails = db.tblTropicalUser.Where(x => x.LoginID == userModel.LoginID &&
                  x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                { //login failed
                    userModel.LoginErrorMessage = "Wrong Login ID or Password.";
                    return View("Login", userModel);
                }
                else 
                { //login success
                    System.Web.Security.FormsAuthentication.SetAuthCookie(userModel.LoginID, userModel.RememberMe);
                    
                    Session["loginID"] = userDetails.LoginID;
                    Session["userName"] = userDetails.FirstName;
                    if (userModel.RememberMe)
                    {
                        
                        //cookie.Values.Add("LoginID", userDetails.LoginID);
                        cookie["loginID"] = userModel.LoginID;
                        cookie.Expires = DateTime.Now.AddDays(15);
                        HttpContext.Response.Cookies.Add(cookie);
                        
                    }
                    return RedirectToAction("Index", "Order"); //action name, controller name
                }
            }
          
        }

        public ActionResult LogOut() {
            //int loginID = (int) Session["loginID"];
            //Session.Abandon();
            return RedirectToAction("Login","Login"); //action name, controller name
        }

        [HttpGet]
        public ActionResult ForgotLoginID()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotLoginID(string EmailID)
        {//verify Email ID
            string message = "";
           // bool status = false;
            using (Models.LoginDataModel db = new Models.LoginDataModel())
            {
                var account = db.tblTropicalUser.Where(a => a.Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //send Email for telling the LoginID
                    //string resetCode = Guid.NewGuid().ToString();
                    SendForgotIDEmail(account.Email, "ForgotID",account.LoginID);
                    //account.ResetPasswordCode = resetCode;
                    db.Configuration.ValidateOnSaveEnabled = false;//remove reset pw does not match issue
                    db.SaveChanges();
                    message = "Your LoginID has been sent to your email. Your Login ID is: "+ account.LoginID;
                }
                else
                {
                    message = "Account not found";
                }
            }
            //Generate reset password link

            ViewBag.Message = message;
            return View();
        }

        [NonAction]
        public void SendForgotIDEmail(string emailID, string emailFor = "ForgotID", string userName="")
        {
            var verifyUrl = "/Login/" + emailFor;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("slithice2012@gmail.com", "Tropical Cheese");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Sydy0436";

            string subject = "";
            string body = "";

            if (emailFor == "ForgotID")
            {
                subject = "Here is your Login ID:";
                body = "Hi, <br/>Your Login ID is:" +
                    "<br/>"+userName;
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [NonAction]
        public void SendForgotPWEmail(string emailID, string activationCode, string emailFor = "ResetPassword")
        {
            var verifyUrl = "/Login/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("slithice2012@gmail.com", "Tropical Cheese");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Sydy0436";

            string subject = "";
            string body = "";
          
            if (emailFor == "ResetPassword") { 
            subject = "Reset Password";
            body = "Hi, <br/>We got request for reset your account password. Please click on the below link to ewset your password." +
                "<br/><br/><a herf=" + link + ">Reset Password link</a>";
        }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //verify Email ID
            string message = "";
            bool status = false;
            using (Models.LoginDataModel db = new Models.LoginDataModel()) {
                var account = db.tblTropicalUser.Where(a => a.Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //send Email for reset the password
                    string resetCode = Guid.NewGuid().ToString();
                    SendForgotPWEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    db.Configuration.ValidateOnSaveEnabled = false;//remove reset pw does not match issue
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else {
                    message = "Account not found";
                }
            }
            //Generate reset password link

            ViewBag.Message = message;
                return View();
        }

        public ActionResult ResetPassword(string id) {
            //verify reset pw link
            //find account associated with this link
            //redirect to reset pw page
            using (Models.LoginDataModel db = new Models.LoginDataModel()) {
                var user = db.tblTropicalUser.Where(a=>a.ResetPasswordCode==id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassWord(ResetPasswordModel model) {
            var message = "";
            if (ModelState.IsValid)
            {
                using (Models.LoginDataModel db = new Models.LoginDataModel()) {
                    var user = db.tblTropicalUser.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user!=null) {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    }
                }
            }
            else {
                message = "Something invalid";
            }

            ViewBag.Message = message;
            return View(model);
        }
       
    }
}