using System.Runtime.CompilerServices;

namespace Todo_BlazorWebassembly_Eksempel.Models
{
    public class CascadingReloadCallback
    {

        private Action _Action;

        public CascadingReloadCallback(Action action)
        {
            _Action = action;
        }

        public Task InvokeAsync()
        {
            _Action.Invoke();
            return Task.CompletedTask;
        }

        public Task Invoke()
        {
            _Action.Invoke();
            return Task.CompletedTask;
        }
    }
}
