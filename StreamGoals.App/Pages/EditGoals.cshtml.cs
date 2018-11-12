using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;

namespace StreamGoals.App.Pages
{
    public class EditGoalsModel : BlazorComponent
    {
        public void OnGet()
        {
        }
        public Task GetGoals()
        {
            //ToDo get all goals 
            return Task.CompletedTask;
        }
        public Task EditGoal(string name)
        {
            return Task.CompletedTask;
        }
    }
}