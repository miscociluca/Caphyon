using AutoMapper;
using Caphyon.Business.Data.Interfaces;
using Caphyon.Business.Entities;
using Caphyon.Business.Services.System;
using Caphyon.Business.Services.System.Interfaces;
using Caphyon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Caphyon.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        protected IToDoService _ToDoService = new ToDoService(AppContext.Create());
        // GET 
        [Route("GetTasks")]
        [HttpGet]
        public IEnumerable<ToDoViewModel> GetTasks()
        {
            var datas = new List<ToDoViewModel>();
            var res = _ToDoService.GetAll();
            foreach (var item in res)
            {
                datas.Add(Mapper.Map<ToDoViewModel>(item));
            }
            return datas;
        }

        [Route("DeleteAll")]
        [HttpPost]
        public HttpResponseMessage DeleteAll()
        {
            bool result = false;
            try
            {
                result = _ToDoService.DeleteAll();
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            if (result)
            {
                var req = Request.CreateResponse(HttpStatusCode.OK);
                return req;
            }
            else
            {
                var req = Request.CreateResponse(HttpStatusCode.BadRequest);
                return req;
            }

        }
        [Route("CompleteAll")]
        [HttpPost]
        public HttpResponseMessage CompleteAll()
        {
            bool result = false;
            try
            {
                result = _ToDoService.CompleteAll();
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            if (result)
            {
                var req = Request.CreateResponse(HttpStatusCode.OK);
                return req;
            }
            else
            {
                var req = Request.CreateResponse(HttpStatusCode.BadRequest);
                return req;
            }

        }

        [Route("AddTask")]
        [HttpPost]
        public HttpResponseMessage AddTask([FromBody]ToDoViewModel value)
        {
            int newTaskId = -1;
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                IToDo AddedModel = _ToDoService.Add(Mapper.Map<IToDo>(value));
                var data = Mapper.Map<ToDoViewModel>(AddedModel);
                newTaskId = data.Id;
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            var req = Request.CreateResponse(HttpStatusCode.Created, newTaskId);
            return req;
        }

        [Route("CompleteTask/{id:int}")]
        [HttpPatch]
        public HttpResponseMessage CompleteTask(int id)
        {
            bool result = false;
            try
            {
                result = _ToDoService.CompleteTask(id);
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            if (result)
            {
                var req = Request.CreateResponse(HttpStatusCode.OK, id);
                return req;
            }
            else
            {
                var req = Request.CreateResponse(HttpStatusCode.BadRequest, id);
                return req;
            }

        }

        [Route("IncompleteTask/{id:int}")]
        [HttpPatch]
        public HttpResponseMessage IncompleteTask(int id)
        {
            bool result = false;
            try
            {
                result = _ToDoService.InCompleteTask(id);
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            if (result)
            {
                var req = Request.CreateResponse(HttpStatusCode.OK, id);
                return req;
            }
            else
            {
                var req = Request.CreateResponse(HttpStatusCode.BadRequest, id);
                return req;
            }

        }


        [Route("UpdateTask/{id:int}")]
        [HttpPut]
        public HttpResponseMessage UpdateTask(int id, [FromBody]ToDoViewModel value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                IToDo UpdatedModel = _ToDoService.Update(Mapper.Map<IToDo>(value));
                if (UpdatedModel == null)
                {
                    Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            var req = Request.CreateResponse(HttpStatusCode.OK, id);
            return req;
        }

        [Route("DeleteTask/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            bool result = false;
            result = _ToDoService.Delete(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            } 
        }
    }
}
