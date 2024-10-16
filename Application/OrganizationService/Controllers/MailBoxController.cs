using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;
using Datalayer.Domain;

namespace OrganizationService.Controllers
{
    [Route("api/mailbox")]
    [ApiController]
    [Authorize]
    public class MailBoxController : ControllerBase
    {
        private readonly IService<MailBox, DefaultSearch<MailBoxSearchResult>> _mailboxService;
        public MailBoxController(IService<MailBox, DefaultSearch<MailBoxSearchResult>> mailboxService)
        {
            _mailboxService = mailboxService;
        }


        [HttpGet("{mailboxId}")]
        [Authorize(MailboxPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid mailboxId)
        {
            return Ok(await _mailboxService.Get(MailBoxProjection.MailBoxInformation, mailboxId));
        }

        [HttpGet()]
        [Authorize(MailboxPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mailboxService.Get(MailBoxProjection.MailBoxInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(MailboxPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<MailBoxSearchResult> search)
        {
            return Ok(await _mailboxService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(MailboxPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(MailBoxInfo newMailBox)
        {
            if (!ModelState.IsValid) return BadRequest();

            MailBox mailbox = new MailBox()
            {
                Server = newMailBox.Server,
                FromAddress = newMailBox.FromAddress,
                AdminEmail = newMailBox.AdminEmail,
                ServerUserName = newMailBox.ServerUserName,
                ServerPassword = newMailBox.ServerPassword,
                ConnectionSecurity = newMailBox.ConnectionSecurity,
                Clients = newMailBox.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = newMailBox.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _mailboxService.Add(mailbox, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(MailboxPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(MailBoxInfo updateMailBox)
        {
            if (!ModelState.IsValid && updateMailBox.Id == Guid.Empty) return BadRequest();

            MailBox mailbox = new MailBox()
            {
                Id = updateMailBox.Id,
                Server = updateMailBox.Server,
                FromAddress = updateMailBox.FromAddress,
                AdminEmail = updateMailBox.AdminEmail,
                ServerUserName = updateMailBox.ServerUserName,
                ServerPassword = updateMailBox.ServerPassword,
                ConnectionSecurity = updateMailBox.ConnectionSecurity,
                Clients = updateMailBox.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = updateMailBox.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _mailboxService.Update(mailbox, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{mailboxId}")]
        [Authorize(MailboxPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid mailboxId)
        {
            if (mailboxId == Guid.Empty) return BadRequest();
            return Ok(await _mailboxService.Delete(new MailBox { Id = mailboxId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
