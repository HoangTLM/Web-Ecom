using HoangTLM.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HoangTLM.Application
{
    public class AppController<T> : ControllerBase where T : class
    {
        protected readonly IRepository<T> _repository;

        protected AppController(IRepository<T> repository)
        {
            _repository = repository;
        }
    }
} 