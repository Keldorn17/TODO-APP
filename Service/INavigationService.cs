using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Core;

namespace TODO.Service
{
    public interface INavigationService
    {
        AbstractViewModel CurrentView { get; }
        void NavigateTo<T>() where T : AbstractViewModel;

    }
}
