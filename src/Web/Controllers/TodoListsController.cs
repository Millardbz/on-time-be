using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.TodoLists.Commands.CreateTodoList;
using on_time_be.Application.TodoLists.Commands.DeleteTodoList;
using on_time_be.Application.TodoLists.Commands.UpdateTodoList;
using on_time_be.Application.TodoLists.Queries.GetTodos;
using on_time_be.WebUI.Controllers;

namespace on_time_be.Web.Controllers
{
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TodosVm>> GetTodoLists()
        {
            return Ok(await Mediator.Send(new GetTodosQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTodoList(CreateTodoListCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoList(int id, UpdateTodoListCommand command)
        {
            if (id != command.Id) 
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand(id));
            return NoContent();
        }
    }
}
