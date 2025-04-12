using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Service
{
    public interface INavigationService
    {
        Core.AbstractViewMode CurrentView { get; }
        void NavigateTo<T>() where T : Core.AbstractViewMode;

    }
}
