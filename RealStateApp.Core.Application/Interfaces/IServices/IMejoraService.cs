using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IMejoraService : IGenericServices<MejoraViewModel, SaveMejoraViewModel, Mejora>
    {
    }
}
