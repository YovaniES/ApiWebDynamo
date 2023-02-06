using ApiWebDynamo.Context;
using ApiWebDynamo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;

namespace ApiWebDynamo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Logeado([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username && x.Password == userObj.Password);
        
            if (user == null)
                return NotFound(new { Message="Usuario no Encontrado!"});

            return Ok(new { Message = "Login exitoso"});
        }


        [HttpPost("registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            await _dataContext.Users.AddAsync(userObj);
            await _dataContext.SaveChangesAsync();

            return Ok(new { Message = "Usuario Registrado"});
        }

        [HttpPost("mail")]
        public IActionResult SendMail(string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("lucerito.10e@gmail.com"));
            email.To.Add(MailboxAddress.Parse("yovany.21se@gmail.com"));

            email.Subject = "Periodos Planificados";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smt.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("lucerito.10e@gmail.com", "yosaes.21y");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
     }
 }


