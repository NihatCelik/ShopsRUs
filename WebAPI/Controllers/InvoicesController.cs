using Business.Handlers.Invoices.Commands;
using Business.Handlers.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await Mediator.Send(new GetInvoicesQuery()));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await Mediator.Send(new GetInvoiceQuery { Id = id }));
        }

        [HttpGet("getbyinvoicenumber")]
        public async Task<IActionResult> GetByInvoiceNumber(string invoiceNumber)
        {
            return GetResponse(await Mediator.Send(new GetInvoiceByInvoiceNumberQuery { InvoiceNumber = invoiceNumber }));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoiceForACustomer([FromBody] CreateInvoiceCommand createInvoiceCommand)
        {
            return GetResponse(await Mediator.Send(createInvoiceCommand));
        }
    }
}
