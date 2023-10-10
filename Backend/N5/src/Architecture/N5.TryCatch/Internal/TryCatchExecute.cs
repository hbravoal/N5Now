namespace N5.TryCatch.Internal;

/// <summary>
/// Try Execute Function
/// </summary>
public class TryCatchExecute
{
    private Action _execute;
    private Action<Exception> _executeCatch;
    private Action _executeFinally;

    /// <summary>
    /// Constructor Try Catch Execute Witout Return
    /// </summary>
    /// <param name="execute">Method try catch</param>
    public TryCatchExecute(Action execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
    }

    /// <summary>
    /// When make a error, you can add code to Catch
    /// </summary>
    /// <param name="executeCatch">Action to execute catch</param>
    /// <returns>Object manage try catch</returns>
    public TryCatchExecute Catch(Action<Exception> executeCatch)
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
    public TryCatchExecute Finally(Action executFinally)
    {
        ArgumentNullException.ThrowIfNull(executFinally);
        _executeFinally = executFinally;
        return this;
    }

    /// <summary>
    /// Execute to try catch
    /// </summary>
    /// <returns>Task to execute</returns>
    public Task Apply()
        => Task.Factory.StartNew(() =>
        {
            try
            {
                if (_execute is not null) _execute();
            }
            catch (Exception ex)
            {
                if (_executeCatch is not null) _executeCatch(ex);
            }
            finally
            {
                if (_executeFinally is not null) _executeFinally();
            }
        });
}