using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Manager;

namespace src.Controllers {
    public class HomeController : Controller {

        public IActionResult Index () {
            var model = new ContactViewModel ();
            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> Index (ContactViewModel model) {
            if (ModelState.IsValid) {
                var mailManager = new MailManager {
                    Subject = "[ITunEsTooL]お問い合わせがありました。",
                    Body = string.Format ("{0}\n{1}\n{2}", model.Name, model.Email, model.Message)
                };
                await mailManager.SendEmailAsync ();
                model = new ContactViewModel ();
                model.Complite = "お問い合わせありがとうございました。ご回答にはしばらくお待ち下さい。";
            }

            return View (model);
        }
    }

    public class ContactViewModel {

        [Required (ErrorMessage = "名前は必須です。")]
        public string Name { get; set; }

        [Required (ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress (ErrorMessage = "Emailを入力してください。")]
        public string Email { get; set; }

        [Required (ErrorMessage = "お問い合わせ内容は必須です。")]
        public string Message { get; set; }
        public string Complite { get; set; }
    }
}