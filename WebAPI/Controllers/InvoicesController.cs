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
        public async Task<IActionResult> GetByinvoiceNumber(string invoiceNumber)
        {
            return GetResponse(await Mediator.Send(new GetInvoiceByInvoiceNumberQuery { InvoiceNumber = invoiceNumber }));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateInvoiceCommand createInvoice)
        {
            return GetResponse(await Mediator.Send(createInvoice));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceCommand updateInvoice)
        {
            return GetResponse(await Mediator.Send(updateInvoice));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteInvoiceCommand deleteInvoice)
        {
            return GetResponse(await Mediator.Send(deleteInvoice));
        }
    }
}
