﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login_registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace login_registration.Controllers
{
    public class HomeController : Controller
    {

        private UserContext _context {get; set;}

        public HomeController(UserContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("reg")]
        public IActionResult reg(User user, string psconf)
        {
            if(ModelState.IsValid && psconf == user.Password){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                HttpContext.Session.SetInt32("User_Id", user.UserId);
                user.Password = Hasher.HashPassword(user, user.Password);
                var account = new Account();
                user.account = account;
                _context.users.Add(user);
                _context.SaveChanges();
                return View("About", user);
            }else{
                return RedirectToAction("Index", user);
            }
        }

        [HttpPost("login")]
        public IActionResult login(string Email, string PasswordToCheck)
        {
            var Hasher = new PasswordHasher<User>();
            var user = _context.users.Include(x => x.account).ThenInclude(x => x.transactions).Where(x => x.Email == Email).FirstOrDefault();
            if(user!=null && PasswordToCheck != null)
            {
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PasswordToCheck))
                {
                    HttpContext.Session.SetInt32("User_Id", user.UserId);
                    user.account.transactions = user.account.transactions.OrderByDescending(x => x.Id).ToList();
                    return View("About", user);
                }
                else
                {
                    return RedirectToAction("Index", user);
                }
            }
            return View("Index");
        }



        [HttpPost("action")]
        public IActionResult act(string action, string amount)
        {
            System.Console.WriteLine(action);
            var user = _context.users.Include(x => x.account).ThenInclude(x => x.transactions).Where(x => x.UserId == HttpContext.Session.GetInt32("User_Id")).FirstOrDefault();
            var amt = Convert.ToInt32(amount);
            var trans = new Transaction();
            trans.CreatedAt = DateTime.Now;
            trans.Difference = amt;
            trans.TransactionType = "Desposit";
            trans.StringDiff = amt.ToString();
            if(action == "WithDraw" && (user.account.Balance - amt) > 0)
            {
                trans.TransactionType = "WithDraw";
                trans.StringDiff = (amt*-1).ToString();
                user.account.Balance -= amt;
            }
            else
            {
                user.account.Balance+=amt;
            }
            
            user.account.transactions.Add(trans);
            _context.SaveChanges();
            user.account.transactions = user.account.transactions.OrderByDescending(x => x.Id).ToList();
            return View("About", user);
        }

        public IActionResult About()
        {
            var user = _context.users.Where(x => x.UserId == HttpContext.Session.GetInt32("User_Id")).FirstOrDefault();
            user.account.transactions = user.account.transactions.OrderByDescending(x => x.Id).ToList();
            return View(user);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
