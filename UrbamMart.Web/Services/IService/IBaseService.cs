using Microsoft.AspNetCore.Mvc;
using UrbamMart.Web.Models;

namespace UrbamMart.Web.Services.IService
{
    public interface IBaseService
    {
        Task<T?> SendAsync<T>(RequestDto requestDto, bool withBearer = true);
    }
}
