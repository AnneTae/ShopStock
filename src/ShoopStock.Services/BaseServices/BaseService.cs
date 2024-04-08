using AutoMapper;
using ShoopStock.Infrastructure;

namespace ShoopStock.Services.BaseServices;

public class BaseService
{
    private readonly IMapper? _mapper;
    private readonly IUnitOfWork? _unitOfWork;

    protected BaseService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    protected BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected IMapper Mapper => _mapper!;
    protected IUnitOfWork UnitOfWork => _unitOfWork!;
}
