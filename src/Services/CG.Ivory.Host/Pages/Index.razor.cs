
namespace CG.Ivory.Host.Pages;

/// <summary>
/// This class is the code-behind for the <see cref="Index"/> page.
/// </summary>
public partial class Index
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the TODO tasks for the page.
    /// </summary>
    internal protected List<TodoModel> _todos = new();

    /// <summary>
    /// This field contains a temporary backup of a TODO task.
    /// </summary>
    private TodoModel? _beforeEdit;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the TODO manager for the page.
    /// </summary>
    [Inject]
    internal protected ITodoManager TodoManager { get; set; } = null!;

    /// <summary>
    /// This property contains the snackbar service for this page.
    /// </summary>
    [Inject]
    internal protected ISnackbar SnackbarService { get; set; } = null!;

    /// <summary>
    /// This property contains the DAL options for this page.
    /// </summary>
    [Inject]
    internal protected IOptions<DataAcessLayerOptions> DalOptions { get; set; } = null!;    

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method initializes the page.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected override async Task OnInitializedAsync()
    {
        // Get the list of TODO tasks.
        _todos = await TodoManager.FindAllAsync();

        // Give the base class a chance.
        await base.OnInitializedAsync();
    }

    // *******************************************************************

    /// <summary>
    /// This method creates a new TODO task.
    /// </summary>
    /// <returns>A task to perform the operation.</returns>
    protected async Task OnCreateTaskAsync()
    {
        try
        {
            // Create the new task.
            var newTask = await TodoManager.CreateAsync(
                new TodoModel()
                {
                    Name = "A new TODO task"
                });

            // Add to the list of tasks.
            _todos.Insert(0, newTask);

            // Tell the world what happened.
            SnackbarService.Add(
                $"Changes were saved",
                Severity.Success,
                options => options.CloseAfterNavigation = true
                );
        }
        catch (Exception ex)
        {
            // Tell the world what happened.
            SnackbarService.Add(
                $"<b>Something broke!</b> " +
                $"<ul><li>{ex.GetBaseException().Message}</li></ul>",
                Severity.Error,
                options => options.CloseAfterNavigation = true
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method is called when changes to a task are committed.
    /// </summary>
    /// <param name="element">The task to use for the operation.</param>
    private void OnCommitTask(object element)
    {
        try
        {
            // Save the changes.
            _ = TodoManager.UpdateAsync(
                (TodoModel)element
                ).Result;

            // We don't need the backup any more.
            _beforeEdit = null;

            // Tell the world what happened.
            SnackbarService.Add(
                $"Changes were saved",
                Severity.Success,
                options => options.CloseAfterNavigation = true
                );
        }
        catch (Exception ex)
        {
            // Tell the world what happened.
            SnackbarService.Add(
                $"<b>Something broke!</b> " +
                $"<ul><li>{ex.GetBaseException().Message}</li></ul>",
                Severity.Error,
                options => options.CloseAfterNavigation = true
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method is before a task is edited.
    /// </summary>
    /// <param name="element">The task to use for the operation.</param>
    private void OnBackupTask(object element)
    {
        // Make a backup copy of the task.
        _beforeEdit = (TodoModel)element.QuickClone();
    }

    // *******************************************************************

    /// <summary>
    /// This method is called when a task edit is rolled back.
    /// </summary>
    /// <param name="element">The task to use for the operation.</param>
    private void OnRollbackTask(object element)
    {
        // Rollback the changes to the task.
        _beforeEdit?.QuickCopyTo(element);

        // We don't need the backup any more.
        _beforeEdit = null;
    }

    // *******************************************************************

    /// <summary>
    /// This method sets the TODO task to done.
    /// </summary>
    /// <param name="element">The task to use for the operation.</param>
    private void OnSetIsDone(TodoModel element)
    {
        try
        {
            // Update the task.
            element.IsDone = true;

            // Save the changes.
            _ = TodoManager.UpdateAsync(
                element
                ).Result;

            // Tell the world what happened.
            SnackbarService.Add(
                $"Changes were saved",
                Severity.Success,
                options => options.CloseAfterNavigation = true
                );
        }
        catch (Exception ex)
        {
            // Tell the world what happened.
            SnackbarService.Add(
                $"<b>Something broke!</b> " +
                $"<ul><li>{ex.GetBaseException().Message}</li></ul>",
                Severity.Error,
                options => options.CloseAfterNavigation = true
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method sets the TODO task to not done.
    /// </summary>
    /// <param name="element">The task to use for the operation.</param>
    private void OnSetIsNotDone(TodoModel element)
    {
        try
        {
            // Update the task.
            element.IsDone = false;

            // Save the changes.
            _ = TodoManager.UpdateAsync(
                element
                ).Result;

            // Tell the world what happened.
            SnackbarService.Add(
                $"Changes were saved",
                Severity.Success,
                options => options.CloseAfterNavigation = true
                );
        }
        catch (Exception ex)
        {
            // Tell the world what happened.
            SnackbarService.Add(
                $"<b>Something broke!</b> " +
                $"<ul><li>{ex.GetBaseException().Message}</li></ul>",
                Severity.Error,
                options => options.CloseAfterNavigation = true
                );
        }
    }

    #endregion
}
