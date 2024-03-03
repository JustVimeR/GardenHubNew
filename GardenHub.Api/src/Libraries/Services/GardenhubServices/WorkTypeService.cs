using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class WorkTypeService : Service<WorkType>, IWorkTypeService
{
    public WorkTypeService(IWorkTypeRepository repository) : base(repository)
    {
    }

    public override Task<List<WorkType>> GetAllAsync()
    {
        return base.GetAllAsync();
    }

    public Task<List<WorkType>> GetDerivedWorkTypesById(List<long> workTypesIds)
    {
        return _repository.GetWhere(x => workTypesIds.Contains(x.Id), ignorePrepareDbSet: true).ToListAsync();
    }

    public override Task<WorkType> PostAsync(WorkType addWorkType)
    {
        if (addWorkType.ParentWorkTypeId != default && !addWorkType.DerivedWorkTypes.IsNullOrEmpty())
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, ErrorMessages.DerivedWorkTypeCantBeParent);
        }

        return base.PostAsync(addWorkType);
    }

    public override Task<WorkType> PutAsync(WorkType updateWorkType)
    {
        if (updateWorkType.ParentWorkTypeId != default && !updateWorkType.DerivedWorkTypes.IsNullOrEmpty())
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, ErrorMessages.DerivedWorkTypeCantBeParent);
        }

        return base.PutAsync(updateWorkType);
    }
}