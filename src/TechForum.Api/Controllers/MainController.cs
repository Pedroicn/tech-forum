using System.Net;
using Microsoft.AspNetCore.Mvc;
using TechForum.Business.Interfaces;
namespace TechForum.Api.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TechForum.Business.Notifications;

[ApiController]
public abstract class MainController : ControllerBase
{
  private readonly INotifier _notifier;

  public MainController(INotifier notifier)
  {
    _notifier = notifier;
  }

  protected bool IsValidOperarion()
  {
    return !_notifier.HasNotification();
  }
  
  protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
  {
    if (IsValidOperarion())
    {
      return new ObjectResult(result)
      {
        StatusCode = Convert.ToInt32(statusCode),
      };
    }

    return BadRequest(new
    {
      errors = _notifier.GetNotifications().Select(notifier => notifier.Message)
    });
  }

  protected ActionResult CustomResponse(ModelStateDictionary modelState)
  {
    if (!modelState.IsValid) NotifyErrorInvalidModel(modelState);
    return CustomResponse();
  }

  protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
  {
    var errors = modelState.Values.SelectMany(error => error.Errors);
    foreach (var error in errors)
    {
      var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
      NotificarErro(errorMsg);
    }
  }

  protected void NotificarErro(string message)
  {
    _notifier.Handle(new Notification(message));
  }
}
