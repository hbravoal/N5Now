namespace N5.Domain.Interfaces.Application;

/// <summary>
/// Base Application implemientaion to Application layer
/// </summary>
/// <typeparam name="T">Request Message</typeparam>
/// <typeparam name="Q">Response Message</typeparam>
public interface IApplication<T, Q>
    where T : class
    where Q : class
{
    /// <summary>
    /// Handler execute aplication
    /// </summary>
    /// <param name="value">Request messaage</param>
    /// <returns>Response Message</returns>
    Task<Q> Handler(T value);
}