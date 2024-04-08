using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShoopStock.Core.Helper;
using ShoopStock.Core.Massages;
using ShoopStock.Core.Models;
using ShoopStock.Infrastructure.Repositories;
using System.Data.Common;

namespace ShoopStock.Infrastructure.UnitOfWork;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IProductRepository? _productRepository;
    private ICategoryRepositoriy? _categoryRepositoriy;
    private bool _disposed;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IProductRepository ProductRepository =>
           _productRepository ??= new ProductRepository(_dbContext);

    public ICategoryRepositoriy CategoryRepositoriy =>
         _categoryRepositoriy ??= new CategoryRepositoriy(_dbContext);

    public async Task<UnitOfWorkExceptionModel> Commit()
    {
        await using var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await _dbContext.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
            return new UnitOfWorkExceptionModel
            {
                Succeeded = true,
            };
        }
        catch (DbException ex)
        {
            await dbContextTransaction.RollbackAsync();
            return new UnitOfWorkExceptionModel
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
        catch (InvalidOperationException ex)
        {
            await dbContextTransaction.RollbackAsync();
            return new UnitOfWorkExceptionModel
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
        catch (DbUpdateException ex)
        {
            await dbContextTransaction.RollbackAsync();

            var sqlException = ex.InnerException as SqlException;

            if (ex.InnerException != null)
            {
                switch (sqlException!.ErrorCode)
                {
                    case ErrorSqlCode.Unique:
                        {
                            var errorMassage = StringHelper.SplitErrorMassage(sqlException.Message!);
                            var typeName = StringHelper.SplitUpperCase(errorMassage[0]);
                            return new UnitOfWorkExceptionModel
                            {
                                Succeeded = false,
                                Message = string.Format(ErrorMessages.UniqueMessage, typeName, errorMassage[1].ToLower())
                            };
                        }
                    case ErrorSqlCode.EntityById:
                        {
                            var errorMassage = StringHelper.SplitErrorMassage(sqlException.Message!);
                            return new UnitOfWorkExceptionModel
                            {
                                Succeeded = false,
                                Message = string.Format(ErrorMessages.EntityNoteFoundMessage,
                                    StringHelper.SplitUpperCase(errorMassage[0]))
                            };
                        }
                    default:
                        {
                            return new UnitOfWorkExceptionModel
                            {
                                Succeeded = false,
                                Message = ex.InnerException!.Message
                            };
                        }
                }
            }

            return new UnitOfWorkExceptionModel
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
        catch (Exception ex)
        {
            await dbContextTransaction.RollbackAsync();
            return new UnitOfWorkExceptionModel
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _dbContext.Dispose();
        }

        _disposed = true;
    }
}