
namespace CG.Ivory.Data.Repositories;

/// <summary>
/// This class is a default implementation of the <see cref="ITodoRepository"/>
/// interface.
/// </summary>
internal class TodoRepository : ITodoRepository
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the auto-mapper for this repository.
    /// </summary>
    internal protected readonly IMapper _mapper;

    /// <summary>
    /// This field contains the data-context for this repository.
    /// </summary>
    internal protected readonly IvoryDbContext _dbContext;

    /// <summary>
    /// This field contains the logger for this repository.
    /// </summary>
    internal protected readonly ILogger<ITodoRepository> _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="TodoRepository"/>
    /// class.
    /// </summary>
    /// <param name="mapper">The auto-mapper to use for this repository.</param>
    /// <param name="dbContext">The data-context to use for this repository.</param>
    /// <param name="logger">The logger to use for this repository.</param>
    public TodoRepository(
        IMapper mapper,
        IvoryDbContext dbContext,
        ILogger<ITodoRepository> logger
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(mapper, nameof(mapper))
            .ThrowIfNull(dbContext, nameof(dbContext))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        )
    {
        // Search the data-context.
        var result = await _dbContext.Todos.AnyAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Return the results.
        return result;
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<TodoModel> CreateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Convert the model to an entity.
        var entity = _mapper.Map<TodoEntity>(model);

        // Add the entity to the data-context.
        _dbContext.Attach(entity);

        // Save the changes.
        await _dbContext.SaveChangesAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Convert the entity to a model.
        model = _mapper.Map<TodoModel>(entity);

        // Return the results.
        return model;   
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Find a tracked entity (if there is one).
        var entity = await _dbContext.Todos.Where(x =>
            x.Id == model.Id
            ).FirstOrDefaultAsync(
                cancellationToken
                ) .ConfigureAwait(false);

        // Did we fail?
        if (entity is null)
        {
            // Panic!!
            throw new KeyNotFoundException(
                $"The TODO key: {model.Id} was not found!"
                );
        }

        // Remove the entity from the data-context.
        _dbContext.Remove(entity);

        // Save the changes.
        await _dbContext.SaveChangesAsync(
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<List<TodoModel>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        // Search the data-context.
        var models = await _dbContext.Todos.Select(x => 
            _mapper.Map<TodoModel>(x)
            ).AsNoTracking()
            .ToListAsync(
                cancellationToken
                ).ConfigureAwait(false);

        // Return the results.
        return models;
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<TodoModel> UpdateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Find a tracked entity (if there is one).
        var entity = await _dbContext.Todos.Where(x =>
            x.Id == model.Id
            ).FirstOrDefaultAsync(
                cancellationToken
                ).ConfigureAwait(false);

        // Did we fail?
        if (entity is null)
        {
            // Panic!!
            throw new KeyNotFoundException(
                $"The TODO key: {model.Id} was not found!" 
                );
        }

        // Update the entity.
        entity.DueDate = model.DueDate;
        entity.Name = model.Name;
        entity.IsDone = model.IsDone;

        // Save the changes.
        await _dbContext.SaveChangesAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Convert the entity to a model.
        var changedModel = _mapper.Map<TodoModel>(entity);

        // Return the results.
        return changedModel;
    }

    #endregion
}
