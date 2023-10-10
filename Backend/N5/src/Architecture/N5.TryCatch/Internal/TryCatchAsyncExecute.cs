namespace N5.TryCatch.Internal;

/// <summary>
/// Try Async Execute Function
/// </summary>
public class TryCatchAsyncExecute
{
    private Func<Task> _execute;
    private Func<Exception, Task>? _executeCatch;
    private Func<Task>? _executeFinally;

    /// <summary>
    /// Constructor Try Catch Execute Witout Return
    /// </summary>
    /// <param name="execute">Method try catch</param>
    public TryCatchAsyncExecute(Func<Task> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
    }

    /// <summary>
    /// When make a error, you can add code to Catch
    /// </summary>
    /// <param name="executeCatch">Action to execute catch</param>
    /// <returns>Object manage try catch</returns>
    public TryCatchAsyncExecute Catch(Func<Exception, Task> executeCatch)
    {
        ArgumentNullException.ThrowIfNull(executeCatch);
        _executeCatch = executeCatch;
        return this;
    }

    /// <summary>
    /// independently of use try or catch execute this function to end method
    /// </summary>
    /// <param name="executFinally"></param>
    /// <returns>Object manage try catch</returns>
    public TryCatchAsyncExecute Finally(Func<Task> executFinally)
    {
        ArgumentNullException.ThrowIfNull(executFinally);
        _executeFinally = executFinally;
        return this;
    }

    /// <summary>
    /// Execute to try catch
    /// </summary>
    /// <returns>Task to execute</returns>
    public async Task Apply()
    {
        try
        {
            if (_execute is not null) await _execute();
        }
        catch (Exception ex)
        {
            if (_executeCatch is not null) await _executeCatch(ex);
        }
        finally
        {
            if (_executeFinally is not null) await _executeFinally();
        }
    }
}