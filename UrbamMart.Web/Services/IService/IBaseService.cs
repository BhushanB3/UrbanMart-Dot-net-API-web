using Microsoft.AspNetCore.Mvc;
using UrbamMart.Web.Models;

namespace UrbamMart.Web.Services.IService
{
    public interface IBaseService
    {
        Task<T?> SendAsync<T>(RequestDto requestDto);
        //public Task<T> SendAsync<T>(HttpRequestMessage requestMessage);
    }
}
